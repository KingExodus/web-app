using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Employees = new Repository<EmployeeEntity>(dbContext);
            EmployeeTypes = new Repository<EmployeeTypeEntity>(dbContext);
        }

        public IRepository<EmployeeEntity> Employees { get; }
        public IRepository<EmployeeTypeEntity> EmployeeTypes { get; }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
