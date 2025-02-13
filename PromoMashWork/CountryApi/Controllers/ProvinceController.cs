namespace CountryApi.Controllers
{
    using CountryApi.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [Display(Name = "Контроллер по городам")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceDataService _provinceDataService;

        public ProvinceController(IProvinceDataService provinceDataService)
        {
            _provinceDataService = provinceDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(long idCountry)
        {
            var provinces = await _provinceDataService.GetProvinces(idCountry);
            return Ok(provinces);
        }
    }
}
