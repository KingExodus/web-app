using Microsoft.AspNetCore.Builder;

namespace Sprout.Exam.Business.Extensions
{
    public static class ExceptionHandlingExtension
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandling>();
        }
    }
}
