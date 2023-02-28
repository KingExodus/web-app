using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Exam.Business.Domain
{
    public interface IAddEmployeeCommand
    {
        Task<CommandResult<int>> ExecuteAsync(CreateEmployeeDto input, ClaimsPrincipal principal, CancellationToken cancellationToken);
    }
}
