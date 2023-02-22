using Sprout.Exam.Business.Domain;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Persistence;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Sprout.Exam.Business.DataTransferObjects;

namespace Sprout.Exam.Business.Services
{
    public class CalculateSalaryCommand : ICalculateSalaryCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculateSalaryCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeePayrollDto> ExecuteAsync(EmployeeSalaryDto input, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));

            var entity = await _unitOfWork.Employees.GetById(input.Id);
            if (entity == null)
            {
                return null;
            }

            var employee = new EmployeePayrollDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Birthdate = entity.Birthdate,
                Tin = entity.TIN,
                TypeId = entity.EmployeeTypeId,
            };

            if (employee.TypeId == (int)EmployeeType.Regular)
            {
                // TODO
            }

            if (employee.TypeId == (int)EmployeeType.Contractual)
            {
                // TODO
            }

            return employee;
        }
    }
}
