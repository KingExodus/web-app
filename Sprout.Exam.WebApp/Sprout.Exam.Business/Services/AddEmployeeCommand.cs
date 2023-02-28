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
    public class AddEmployeeCommand : IAddEmployeeCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<int>> ExecuteAsync(CreateEmployeeDto input, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));

            var employee = await _unitOfWork.Employees.GetByQuery(x => x.FullName == input.FullName);
            if (employee != null)
            {
                return new CommandResult<int>("Employee is already exist");
            }

            employee = new EmployeeEntity
            {
                FullName = input.FullName,
                Birthdate = DateTime.Parse(input.Birthdate),
                TIN = input.Tin,
                EmployeeTypeId = input.TypeId
            };

            _unitOfWork.Employees.Add(employee);
            await _unitOfWork.SaveChangesAsync();

            return new CommandResult<int>(employee.Id);
        }
    }
}
