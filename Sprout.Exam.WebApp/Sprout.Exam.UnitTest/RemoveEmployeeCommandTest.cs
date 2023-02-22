using Moq;
using Shouldly;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class RemoveEmployeeCommandTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private RemoveEmployeeCommand CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            return new RemoveEmployeeCommand(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task RemoveEmployeeCommand_EmployeeNotExists_ShouldReturnNull()
        {
            var command = CreateCommand();

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync((EmployeeEntity)null);

            var result = await command.ExecuteAsync(1, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldBeNull();

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RemoveEmployeeCommand_ShouldRemoveEmployee_IfEmployeeExist()
        {
            var command = CreateCommand();
            var employee = CreateEmployee();

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                .ReturnsAsync(employee);

            _unitOfWorkMock.Setup(x => x.Employees.Update(employee));
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await command.ExecuteAsync(employee.Id, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldNotBeNull();
            result.IsDeleted.ShouldBeTrue();

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.Employees.Update(It.IsAny<EmployeeEntity>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        private EmployeeEntity CreateEmployee()
        {
            return new EmployeeEntity
            {
                Id = 1,
                FullName = "FullName",
                Birthdate = DateTime.Now.Date,
                TIN = "1234",
                EmployeeTypeId = 1
            };
        }
    }
}
