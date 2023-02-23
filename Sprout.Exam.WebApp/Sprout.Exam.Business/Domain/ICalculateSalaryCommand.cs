using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Sprout.Exam.Business.DataTransferObjects;

namespace Sprout.Exam.Business.Domain
{
    public interface ICalculateSalaryCommand
    {
        Task<EmployeePayrollDto> ExecuteAsync(EmployeeSalaryDto input, ClaimsPrincipal principal, CancellationToken cancellationToken);
    }
}
