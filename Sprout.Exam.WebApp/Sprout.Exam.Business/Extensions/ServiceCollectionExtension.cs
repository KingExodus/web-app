using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.Domain.Query;
using Sprout.Exam.Business.Domain;
using Sprout.Exam.Business.Services.Query;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.DataAccess;

namespace Sprout.Exam.Business.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDependencyServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAddEmployeeCommand, AddEmployeeCommand>();
            services.AddScoped<IUpdateEmployeeCommand, UpdateEmployeeCommand>();
            services.AddScoped<IEmployeeQuery, EmployeeQuery>();
            services.AddScoped<IEmployeeByIdQuery, EmployeeByIdQuery>();
            services.AddScoped<IRemoveEmployeeCommand, RemoveEmployeeCommand>();
            services.AddScoped<ICalculateSalaryCommand, CalculateSalaryCommand>();
        }
    }
}
