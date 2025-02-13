namespace CountryApi.Interfaces
{
    using CountryApi.Dto;

    public interface ICountryDataService
    {
        public Task<List<CountryDto>> GetCountries();
    }
}
