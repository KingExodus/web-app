using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Sprout.Exam.Models;

namespace Sprout.Exam.Business.Domain
{
    public interface IRemoveEmployeeCommand
    {
        Task<CommandResult<EmployeeEntity>> ExecuteAsync(int id, ClaimsPrincipal principal, CancellationToken cancellationToken);
    }
}
