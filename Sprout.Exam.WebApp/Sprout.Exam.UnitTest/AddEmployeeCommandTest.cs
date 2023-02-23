using Moq;
using Shouldly;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class AddEmployeeCommandTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private AddEmployeeCommand CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            return new AddEmployeeCommand(_unitOfWorkMock.Object);
        }

        [Fact]
        public void AddEmployeeCommand_ShouldReturn_ThrowsException()
        {
            var command = CreateCommand();

            Should.Throw<ArgumentNullException>(() => command.ExecuteAsync(null, It.IsAny<ClaimsPrincipal>(), CancellationToken.None));
        }

        [Fact]
        public async Task AddEmployeeCommand_EmployeeExists_ShouldReturnNull()
        {
            var employee = CreateEmployee();
            var command = CreateCommand();

            _unitOfWorkMock.Setup(x => x.Employees.GetByQuery(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
                   .ReturnsAsync(employee);

            var result = await command.ExecuteAsync(Mock.Of<CreateEmployeeDto>(), It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldBeNull();

            _unitOfWorkMock.Verify(x => x.Employees.GetByQuery(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddEmployeeCommand_ShouldAddEmployee_IfEmployeeNotExist()
        {
            var command = CreateCommand();

            _unitOfWorkMock.Setup(x => x.Employees.GetByQuery(x => x.FullName == It.IsAny<string>()))
                .ReturnsAsync((EmployeeEntity)null);

            var employee = CreateEmployee();
            _unitOfWorkMock.Setup(x => x.Employees.Add(employee));
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            var employeeDto = new CreateEmployeeDto
            {
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

            _unitOfWorkMock.Verify(x => x.Employees.GetByQuery(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.Employees.Add(It.IsAny<EmployeeEntity>()), Times.Once);
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
