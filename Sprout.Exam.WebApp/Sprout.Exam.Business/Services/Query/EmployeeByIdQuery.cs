using Sprout.Exam.Business.Domain.Query;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Exam.Business.Services.Query
{
    public class EmployeeByIdQuery : IEmployeeByIdQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeByIdQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeEntity> ExecuteAsync(int id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Employees.GetById(id);
        }
    }
}
