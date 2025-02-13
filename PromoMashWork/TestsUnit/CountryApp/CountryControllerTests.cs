namespace TestsUnit.CountryApp
{
    using CountryApi.Controllers;
    using CountryApi.Dto;
    using CountryApi.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class CountryControllerTests
    {
        [Fact]
        public async Task IndexViewDataMessage()
        {
            var mock = new Mock<ICountryDataService>();
            var testCountry = GetTestCountry();

            mock.Setup(repo => repo.GetCountries()).Returns(testCountry);

            CountryController controller = new CountryController(mock.Object);

            var result = (OkObjectResult)await controller.GetCountries();

            Assert.Equal(200, result.StatusCode);
            Assert.Same(testCountry.Result, result.Value);
        }

        private Task<List<CountryDto>> GetTestCountry()
        {
            var users = new List<CountryDto>
            {
                new CountryDto (1, "test"),
            };
            return Task.FromResult(users);
        }
    }
}
