using Moq;
using Shouldly;
using Sprout.Exam.Business.Services.Query;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTest.Query
{
    public class EmployeeQueryTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private EmployeeQuery CreateCommand()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            return new EmployeeQuery(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task EmployeeQuery_ShouldReturn_Employees()
        {
            var command = CreateCommand();
            var employeeList = CreateEmployeeList();

            _unitOfWorkMock.Setup(x => x.Employees.GetAllAsync())
                   .ReturnsAsync(employeeList);

            var result = await command.ExecuteAsync(It.IsAny<ClaimsPrincipal>(), CancellationToken.None);

            result.ShouldNotBeNull();

            _unitOfWorkMock.Verify(x => x.Employees.GetAllAsync(), Times.Once);
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
