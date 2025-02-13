using CountryApi.Dto;

namespace CountryApi.Interfaces
{
    public interface IProvinceDataService
    {
        public Task<List<ProvinceDto>> GetProvinces(long countryId);
    }
}
