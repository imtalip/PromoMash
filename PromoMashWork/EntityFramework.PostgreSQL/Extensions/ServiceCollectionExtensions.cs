namespace EntityFramework.PostgreSQL.Extensions
{
    using System.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CountryDataAccess.Data;
    using IdentityServerData.Data;
    using Microsoft.AspNetCore.Identity;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCountryDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            string countryPostgres = configuration.GetConnectionString("CountryPostgres") ??
                throw new ConfigurationErrorsException(
                    "ConnectionStrings.CountryPostgres is a required config!");

            services.AddDbContextPool<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(countryPostgres, opt =>
                    {
                        opt.EnableRetryOnFailure();
                        opt.MigrationsAssembly("EntityFramework.PostgreSQL");
                    });
                    options.EnableSensitiveDataLogging();
                });

            return services;
        }

        public static IServiceCollection AddIS4DbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            string is4Postgres = configuration.GetConnectionString("IS4Postgres") ??
                throw new ConfigurationErrorsException(
                    "ConnectionStrings.IS4Postgres is a required config!");

            services.AddDbContextPool<AspNetIdentityDbContext>(
                options =>
                {
                    options.UseNpgsql(is4Postgres, opt =>
                    {
                        opt.EnableRetryOnFailure();
                        opt.MigrationsAssembly("EntityFramework.PostgreSQL");
                    });
                    options.EnableSensitiveDataLogging();
                });

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(is4Postgres, opt => opt.MigrationsAssembly("EntityFramework.PostgreSQL"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(is4Postgres, opt => opt.MigrationsAssembly("EntityFramework.PostgreSQL"));
                })
                .AddDeveloperSigningCredential();

            return services;
        }


        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            string is4Postgres = configuration.GetConnectionString("IS4Postgres") ??
                throw new ConfigurationErrorsException(
                    "ConnectionStrings.IS4Postgres is a required config!");

            services.AddDbContextPool<AspNetIdentityDbContext>(
                options =>
                {
                    options.UseNpgsql(is4Postgres, opt =>
                    {
                        opt.EnableRetryOnFailure();
                        opt.MigrationsAssembly("EntityFramework.PostgreSQL");
                    });
                    options.EnableSensitiveDataLogging();
                });

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(is4Postgres, opt => opt.MigrationsAssembly("EntityFramework.PostgreSQL"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(is4Postgres, opt => opt.MigrationsAssembly("EntityFramework.PostgreSQL"));
                })
                .AddDeveloperSigningCredential();

            return services;
        }

    }
}
