using EventPlanner.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace EventPlanner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        #region Properties

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger = logger;

        #endregion Properties

        [HttpGet]
        public List<WeatherForecast> GetWeatherForecast(int count)
        {
            List<WeatherForecast> response = new();

            for (int i = 1; i <= count; i++)
            {
                response.Add(new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                });
            }

            return response;
        }
    }
}
