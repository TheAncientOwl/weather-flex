﻿using WeatherFlex.Database;
using WeatherFlex.Model;
using WeatherFlex.Model.Weather;
using WeatherFlex.Services;

namespace WeatherFlex.ViewModels
{
    public class WeatherViewModel
    {
        static readonly int TEMPERATURES_COUNT_LIMIT = 25;

        readonly WeatherService weatherService;
        readonly GeolocationService geolocationService;

        public WeatherAPI WeatherAPI { get; private set; }

        public WeatherViewModel(WeatherService weatherService, GeolocationService geolocationService)
        {
            this.weatherService = weatherService;
            this.geolocationService = geolocationService;
        }

        public async Task GetUserWeatherAsync()
        {
            Location userLocation = await geolocationService.GetLocationAsync();

            await GetWeatherAsync(userLocation.Latitude, userLocation.Longitude);
        }

        public async Task GetWeatherAsync(double latitude, double longitude)
        {
            WeatherAPI = await weatherService.FetchWeather(latitude, longitude);

            WeatherAPI.LocationProperties = await geolocationService.GetLocationPropertiesAsync(latitude, longitude);
        }

        public List<Temperature> GetHourlyTemperature(Settings userSettings)
        {
            DateTime now = DateTime.Now;

            List<Temperature> temperatures = new();
            TemperatureForecast temperatureForecast = WeatherAPI.TemperatureForecast;

            for (int i = 0, temperaturesCountLimit = TEMPERATURES_COUNT_LIMIT;
                i < temperatureForecast.Temperature.Count && temperaturesCountLimit > 0;
                i++)
            {
                DateTime time = temperatureForecast.GetDateTimeAt(i);

                if (time.Day > now.Day || (time.Day == now.Day && time.Hour >= now.Hour))
                {
                    temperaturesCountLimit--;
                    temperatures.Add(new Temperature()
                    {
                        Time = temperatureForecast.Time[i],
                        Value = userSettings.PreffersCelsius ? temperatureForecast.Temperature[i] : ToFahrenheit(temperatureForecast.Temperature[i]),
                        Units = userSettings.PreffersCelsius ? WeatherAPI.WeatherUnits.Units : "F",
                        PrecipitationProbability = temperatureForecast.PrecipitationProbability[i],
                        WeatherCode = WeatherCode.FromCode(temperatureForecast.Weathercode[i])
                    });
                }
            }

            temperatures[0].IsNow = true;

            return temperatures;
        }

        static double ToFahrenheit(double temperatureC) => temperatureC * 1.8 + 32;
    }
}
