using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Domain.Settings;
using TestMatchProfile.Infrastructure.Shared.Services;

namespace TestMatchProfile.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<IMockService, MockService>();
        }
    }
}