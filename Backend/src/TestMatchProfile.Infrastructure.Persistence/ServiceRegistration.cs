using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using DbUp;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Infrastructure.Persistence.Contexts;
using TestMatchProfile.Infrastructure.Persistence.Repositories;
using TestMatchProfile.Infrastructure.Persistence.Repository;

namespace TestMatchProfile.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            #region Repositories

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IAuthorRepositoryAsync, AuthorRepositoryAsync>();
            services.AddTransient<ILegalEntityRepositoryAsync, LegalEntityRepositoryAsync>();
            services.AddTransient<ILegalContractRepositoryAsync, LegalContractRepositoryAsync>();
            services.AddTransient<IPositionRepositoryAsync, PositionRepositoryAsync>();
           
            #endregion Repositories
        }

        public static void MigrateDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
                        
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem(Path.Combine("ScriptsDB"))
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();
        }
    }
}