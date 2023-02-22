using Sprout.Exam.Models;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Persistence
{
    public interface IUnitOfWork
    {
        IRepository<EmployeeEntity> Employees { get; }
        IRepository<EmployeeTypeEntity> EmployeeTypes { get; }

        Task SaveChangesAsync();
    }
}
