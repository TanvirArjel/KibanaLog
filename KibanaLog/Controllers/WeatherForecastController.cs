using Microsoft.AspNetCore.Mvc;

namespace KibanaLog.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        try
        {
            var random = new Random();
            var randomVal = random.Next(0, 5);
            if (randomVal > 2)
            {
                throw new InvalidOperationException("There is something wrong with random val.");
            }
            
            _logger.LogInformation("Request started.");
            WeatherForecast[] weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            _logger.LogInformation("Request ended.");
            return Ok(weatherForecasts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something serious happened.");
            return new StatusCodeResult(500);
        }
    }
}