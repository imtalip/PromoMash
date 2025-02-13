namespace CountryApi.Extensions
{
    using CountryApi.Interfaces;
    using CountryApi.Services;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICountryDataService, CountryDataService>();
            services.AddScoped<IProvinceDataService, ProvinceDataService>();

            return services;
        }
    }
}
