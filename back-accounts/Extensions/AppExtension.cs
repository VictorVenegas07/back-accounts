using back_accounts.Middlewares;

namespace back_accounts.Extensions
{
    public static class AppExtension
    {
        public static void useErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
