using Sprout.Exam.Business.Domain.Query;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Sprout.Exam.Business.Services.Query
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeEntity>> ExecuteAsync(ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            return employees.Where(x => !x.IsDeleted);
        }
    }
}
