namespace CountryApi.Services
{
    using CountryApi.Dto;
    using CountryApi.Interfaces;
    using CountryDataAccess.Data;
    using Microsoft.EntityFrameworkCore;

    public class ProvinceDataService : IProvinceDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProvinceDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProvinceDto>> GetProvinces(long id)
        {
            var countries = await _dbContext.Province
                .Where(x => x.CountryId == id)
                .Select(x => new ProvinceDto(x.Name))
                .ToListAsync();

            return countries;
        }
    }
}
