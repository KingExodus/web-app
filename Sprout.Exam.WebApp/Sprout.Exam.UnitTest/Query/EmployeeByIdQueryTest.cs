using Moq;
using Sprout.Exam.Business.Services.Query;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Shouldly;

namespace Sprout.Exam.UnitTest.Query
{
    public class EmployeeByIdQueryTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private EmployeeByIdQuery CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            return new EmployeeByIdQuery(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task EmployeeByIdQuery_ShouldReturn_Employee()
        {
            var command = CreateCommand();
            var employeeList = CreateEmployeeList();

            _unitOfWorkMock.Setup(x => x.Employees.GetById(It.IsAny<int>()))
                   .ReturnsAsync(employeeList[1]);

            var result = await command.ExecuteAsync(employeeList[1].Id, It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.FullName.ShouldBe(employeeList[1].FullName);
            result.Birthdate.ShouldBe(employeeList[1].Birthdate);
            result.TIN.ShouldBe(employeeList[1].TIN);
            result.EmployeeTypeId.ShouldBe(employeeList[1].EmployeeTypeId);

            _unitOfWorkMock.Verify(x => x.Employees.GetById(It.IsAny<int>()), Times.Once);
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
                     Id = 2,
                     FullName = "Jane Doe",
                     Birthdate = new DateTime(1995, 1, 1),
                     TIN = "123456789",
                     EmployeeTypeId = 2,
                 },
             };
        }
    }
}
