using System.Linq;

#pragma warning disable CS9113 // Parameter is unread.

namespace Readly.Api.WeatherForecast;

public class List(IMediator _mediator) : EndpointWithoutRequest<WeatherForecastListResponse>
{
    public override void Configure()
    {
        Get("/weatherforecast");
        AllowAnonymous();
    }


    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastRecord
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToList();

        await Task.CompletedTask;

        Response = new WeatherForecastListResponse() { WeatherForecasts = forecasts };
    }
}
