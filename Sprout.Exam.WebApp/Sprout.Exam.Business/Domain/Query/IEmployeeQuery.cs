using Sprout.Exam.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Exam.Business.Domain.Query
{
    public interface IEmployeeQuery
    {
        Task<IEnumerable<EmployeeEntity>> ExecuteAsync(ClaimsPrincipal principal, CancellationToken cancellationToken);
    }
}
