using Microsoft.AspNetCore.Mvc;

namespace back_accounts.Extensions
{
    public static class SerivceExtensions
    {
        public static void AddApiVersionExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1,0);

                config.AssumeDefaultVersionWhenUnspecified = true;

                config.ReportApiVersions = true;
                
            });
        }
    }
}
