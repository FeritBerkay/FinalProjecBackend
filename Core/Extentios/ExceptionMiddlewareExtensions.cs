using Microsoft.AspNetCore.Builder;
using static Core.Extentios.Class1;

namespace Core.Extentios
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
