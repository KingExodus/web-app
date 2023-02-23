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
            Employee = Set<EmployeeEntity>();
            EmployeeType = Set<EmployeeTypeEntity>();
        }

        public DbSet<EmployeeEntity> Employee { get; }
        public DbSet<EmployeeTypeEntity> EmployeeType { get; }
    }
}
