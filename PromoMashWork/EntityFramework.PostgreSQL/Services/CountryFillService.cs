namespace EntityFramework.PostgreSQL.Services
{
    using CountryDataAccess.Data;
    using CountryDataAccess.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Configuration;

    /// <summary>
    /// Заполнением псевдо данными для стран и городов.
    /// </summary>
    public class CountryFillService
    {
        public static void FillData(IConfiguration configuration)
        {
            string countryPostgres = configuration.GetConnectionString("CountryPostgres") ??
                throw new ConfigurationErrorsException(
                    "ConnectionStrings.CountryPostgres is a required config!");

            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContextPool<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(countryPostgres);
                });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
            ctx.Database.Migrate();
            EnsureCountries(ctx);
        }

        private static void EnsureCountries(ApplicationDbContext context)
        {
            if (!context.Country.Any())
            {
                foreach (var groupProvinces in FakeData.Provinces.GroupBy(x => x.Country.Name))
                {
                    var country = groupProvinces.First().Country;

                    context.Country.Add(country);

                    context.SaveChanges();

                    foreach (var province in groupProvinces)
                    {
                        province.Country = country;
                        province.CountryId = country.Id;

                        context.Province.Add(province);

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
