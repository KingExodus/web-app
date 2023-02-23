using Moq;
using Shouldly;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class UpdateEmployeeCommandTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private UpdateEmployeeCommand CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            return new UpdateEmployeeCommand(_unitOfWorkMock.Object);
        }

        [Fact]
        public void UpdateEmployeeCommand_ShouldReturn_ThrowsException()
        {
            var command = CreateCommand();

            Should.Throw<ArgumentNullException>(() => command.ExecuteAsync(null, It.IsAny<ClaimsPrincipal>(), CancellationToken.None));
        }

        [Fact]
        public async Task UpdateEmployeeCommand_EmployeeNotExists_ShouldReturnNull()
        {
            var command = CreateCommand();
            
            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync((EmployeeEntity)null);

            var result = await command.ExecuteAsync(Mock.Of<EditEmployeeDto>(), It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldBeNull();

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateEmployeeCommand_ShouldUpdateEmployee_IfEmployeeExist()
        {
            var command = CreateCommand();
            var employee = CreateEmployee();

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                .ReturnsAsync(employee);

            _unitOfWorkMock.Setup(x => x.Employees.Update(employee));
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            var employeeDto = new EditEmployeeDto
            {
                Id = 1,
                FullName = "FullName",
                Birthdate = DateTime.Now.Date,
                Tin = "1234",
                TypeId = 1
            };
            var result = await command.ExecuteAsync(employeeDto, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(employee.FullName);
            result.Birthdate.ShouldBe(employee.Birthdate);
            result.TIN.ShouldBe(employee.TIN);
            result.EmployeeTypeId.ShouldBe(employee.EmployeeTypeId);

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
