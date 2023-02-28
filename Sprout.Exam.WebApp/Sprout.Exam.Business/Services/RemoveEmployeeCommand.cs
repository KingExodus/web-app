using Sprout.Exam.DataAccess.Persistence;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Sprout.Exam.Business.Domain;
using Sprout.Exam.Models;

namespace Sprout.Exam.Business.Services
{
    public class RemoveEmployeeCommand : IRemoveEmployeeCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEmployeeCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<EmployeeEntity>> ExecuteAsync(int id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return null;
            }

            employee.IsDeleted = true;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return new CommandResult<EmployeeEntity>(employee);
        }
    }
}
