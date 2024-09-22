namespace DotNetMinimalApiCrudWithoutDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<WeatherForecast> WeatherList = new List<WeatherForecast>()
            {
                new WeatherForecast
                {
                    Id = 1,
                    Date = new(2021,1,31),
                    TemperatureC = 32,
                    Summary = "Summary 1"
                },
                new WeatherForecast
                {
                    Id = 2,
                    Date = new(2021,1,31),
                    TemperatureC = 32,
                    Summary = "Summary 1"
                },
                new WeatherForecast
                {
                    Id = 3,
                    Date = new(2021,1,31),
                    TemperatureC = 32,
                    Summary = "Summary 1"
                }
            };
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/weatherforecast", () =>
            {
                return WeatherList;
            })
            .WithName("GetAllWeatherForecast")
            .WithOpenApi();

            app.MapGet("/weatherforecast/{id}", (int id) =>
            {
                WeatherForecast result = WeatherList.FirstOrDefault(w => w.Id == id);
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            }).WithName("GetWeatherForecast").WithOpenApi();

            app.MapPost("/weatherforecast", (WeatherForecast forecast) =>
            {
                forecast.Id = WeatherList.Last().Id + 1;
                WeatherList.Add(forecast);
                return Results.Created($"/weatherforecast/{forecast.Id}", forecast);
            }).WithName("CreateWeatherForecast").WithOpenApi();

            app.MapPut("/weatherforecast", (int id, WeatherForecast updatedForecast) =>
            {
                var forecast = WeatherList.FirstOrDefault(f => f.Id == id);
                if (forecast == null)
                {
                    return Results.NotFound();
                }
                int index = WeatherList.IndexOf(forecast);
                WeatherList[index].Date = updatedForecast.Date;
                WeatherList[index].Summary = updatedForecast.Summary;
                WeatherList[index].TemperatureC = updatedForecast.TemperatureC;
                return Results.Created($"/weatherforecast/{forecast.Id}", forecast);
            }).WithName("UpdateWeatherForecast").WithOpenApi();

            app.MapDelete("/weatherforecast/{id}", (int id) =>
            {
                WeatherForecast result = WeatherList.FirstOrDefault(w => w.Id == id);
                if (result != null)
                {
                    WeatherList.Remove(result);
                    return Results.Ok(result);
                }
                return Results.NotFound();
            }).WithName("DeleteWeatherForecast").WithOpenApi();

            app.Run();
        }
    }
}
