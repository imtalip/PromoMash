namespace EntityFramework.PostgreSQL.Services
{
    using IdentityModel;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Storage;
    using IdentityServerData.Data;
    using IdentityServerData.FakeData;
    using IdentityServerData.Mappers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Configuration;
    using System.Security.Claims;

    /// <summary>
    /// Заполнение псевдо данными для  IdentityServer.
    /// </summary>
    public class IdentityServerFillService
    {
        public static void FillData(IConfiguration configuration)
        {
            string is4Postgres = configuration.GetConnectionString("IS4Postgres") ??
                throw new ConfigurationErrorsException(
                    "ConnectionStrings.IS4Postgres is a required config!");

            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseNpgsql(is4Postgres)
            );

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseNpgsql(
                            is4Postgres,
                            sql => sql.MigrationsAssembly("EntityFramework.PostgreSQL")
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseNpgsql(
                            is4Postgres,
                            sql => sql.MigrationsAssembly("EntityFramework.PostgreSQL")
                        );
                }
            );

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var angella = userMgr.FindByNameAsync("user1").Result;
            if (angella == null)
            {
                angella = new IdentityUser
                {
                    UserName = "user1",
                    Email = "user1@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(angella, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        angella,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "user1"),
                            new Claim(JwtClaimTypes.GivenName, "user1"),
                            new Claim(JwtClaimTypes.FamilyName, "user1"),
                            new Claim("location", "somewhere")
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in FakeData.Clients.ToList())
                {
                    context.Clients.Add(client.OurToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in FakeData.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.OurToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in FakeData.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.OurToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}
