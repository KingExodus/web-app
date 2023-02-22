using Moq;
using Shouldly;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;
using Sprout.Exam.Business.Domain.Factory;
using System.Collections.Generic;
using Sprout.Exam.Business.Services.Factory;
using System.Linq;

namespace Sprout.Exam.UnitTest
{
    public class CalculateSalaryCommandTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IEmploymentTypeFactory> _employmentTypeFactoryMock;

        private CalculateSalaryCommand CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _employmentTypeFactoryMock = new Mock<IEmploymentTypeFactory>();

            return new CalculateSalaryCommand(_unitOfWorkMock.Object,
                _employmentTypeFactoryMock.Object);
        }

        [Fact]
        public void CalculateSalaryCommand_ShouldReturn_ThrowsException()
        {
            var command = CreateCommand();

            Should.Throw<ArgumentNullException>(() => command.ExecuteAsync(null, It.IsAny<ClaimsPrincipal>(), CancellationToken.None));
        }

        [Fact]
        public async Task CalculateSalaryCommand_EmployeeNotExists_ShouldReturnNull()
        {
            var command = CreateCommand();

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync((EmployeeEntity)null);

            var result = await command.ExecuteAsync(Mock.Of<EmployeeSalaryDto>(), It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldBeNull();

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CalculateSalaryCommand_RegularEmployee_ShouldReturn_ComputedSalary()
        {
            var command = CreateCommand();
            var employee = CreateEmployeeList();
            var netPay = 15781.82;

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync(employee[0]);

            _employmentTypeFactoryMock.Setup(x => x.ComputeSalary(It.IsAny<decimal>()))
                .Returns((decimal)netPay);

            var regularEmployee = new EmployeeSalaryDto
            {
                Id = 1,
                AbsentDays = 2,
            };

            var result = await command.ExecuteAsync(regularEmployee, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(employee[0].FullName);
            result.Birthdate.ShouldBe(employee[0].Birthdate);
            result.Tin.ShouldBe(employee[0].TIN);
            result.TypeId.ShouldBe(employee[0].EmployeeTypeId);
            result.SalaryNetPay.ShouldBeEquivalentTo((decimal)netPay);

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CalculateSalaryCommand_ContractualEmployee_ShouldReturn_ComputedSalary()
        {
            var command = CreateCommand();
            var employee = CreateEmployeeList();
            var netPay = 7250;

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync(employee[1]);

            _employmentTypeFactoryMock.Setup(x => x.ComputeSalary(It.IsAny<decimal>()))
                .Returns((decimal)netPay);

            var contractualEmployee = new EmployeeSalaryDto
            {
                Id = 2,
                WorkedDays = (decimal)14.5,
            };

            var result = await command.ExecuteAsync(contractualEmployee, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(employee[1].FullName);
            result.Birthdate.ShouldBe(employee[1].Birthdate);
            result.Tin.ShouldBe(employee[1].TIN);
            result.TypeId.ShouldBe(employee[1].EmployeeTypeId);
            result.SalaryNetPay.ShouldBeEquivalentTo((decimal)netPay);

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        private List<EmployeeEntity> CreateEmployeeList()
        {
            return new List<EmployeeEntity>
             {
                 new EmployeeEntity
                 {
                     Id = 1,
                     FullName = "John Smith",
                     Birthdate = new DateTime(1990, 1, 1),
                     TIN = "123456789",
                     EmployeeTypeId = 1,
                 },
                 new EmployeeEntity
                 {
                     Id = 1,
                     FullName = "Jane Doe",
                     Birthdate = new DateTime(1995, 1, 1),
                     TIN = "123456789",
                     EmployeeTypeId = 2,
                 },
             };
        }
    }
}
