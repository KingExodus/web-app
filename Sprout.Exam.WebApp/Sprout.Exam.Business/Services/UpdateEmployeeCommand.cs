using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Domain;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Exam.Business.Services
{
    public class UpdateEmployeeCommand : IUpdateEmployeeCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<EmployeeEntity>> ExecuteAsync(EditEmployeeDto input, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));

            var employee = await _unitOfWork.Employees.GetById(input.Id);
            if (employee == null)
            {
                return null;
            }

            employee.FullName = input.FullName;
            employee.Birthdate = DateTime.Parse(input.Birthdate);
            employee.TIN = input.Tin;
            employee.EmployeeTypeId = input.TypeId;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return new CommandResult<EmployeeEntity>(employee);
        }
    }
}
