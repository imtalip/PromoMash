namespace CountryApi.Services
{
    using CountryApi.Dto;
    using CountryApi.Interfaces;
    using CountryDataAccess.Data;
    using Microsoft.EntityFrameworkCore;

    public class CountryDataService : ICountryDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CountryDto>> GetCountries()
        {
            var countries = await _dbContext.Country
                .Select(x => new CountryDto(x.Id, x.Name))
                .ToListAsync();

            return countries;
        }
    }
}
