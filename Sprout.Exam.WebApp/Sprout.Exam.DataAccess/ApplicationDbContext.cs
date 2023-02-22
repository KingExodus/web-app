using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            Employees = Set<EmployeeEntity>();
            EmployeeTypes = Set<EmployeeTypeEntity>();
        }

        public DbSet<EmployeeEntity> Employees { get; }
        public DbSet<EmployeeTypeEntity> EmployeeTypes { get; }
    }
}
