namespace CountryApi.Controllers
{
    using CountryApi.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Display(Name = "Контроллер по странам")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryDataService _countryDataService;

        public CountryController(ICountryDataService countryDataService)
        {
            _countryDataService = countryDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryDataService.GetCountries();
            return Ok(countries);
        }
    }
}
