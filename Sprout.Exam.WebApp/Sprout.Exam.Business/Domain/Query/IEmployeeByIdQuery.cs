using Sprout.Exam.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Exam.Business.Domain.Query
{
    public interface IEmployeeByIdQuery
    {
        Task<EmployeeEntity> ExecuteAsync(int id, ClaimsPrincipal principal, CancellationToken cancellationToken);
    }
}
