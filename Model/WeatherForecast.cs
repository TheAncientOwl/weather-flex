﻿using Newtonsoft.Json;

namespace WeatherFlex.Model
{
    public class WeatherForecast
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationTimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("daily_units")]
        public DailyUnits DailyWeatherUnit { get; set; }

        [JsonProperty("daily")]
        public WeatherApiDaily Daily { get; set; }

    }

    public class DailyUnits
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("weathercode")]
        public string Weathercode { get; set; }

        [JsonProperty("temperature_2m_max")]

        public string Temperature_2m_max { get; set; }

        [JsonProperty("temperature_2m_min")]
        public string Temperature_2m_min { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("precipitation_probability_max")]
        public string Precipitation_probability_max { get; set; }
    }

    public class WeatherApiDaily
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }
        
        [JsonProperty("weathercode")]
        public List<int> WeatherCode { get; set; }

        [JsonProperty("temperature_2m_max")]

        public List<double> Temperature2mMaxList { get; set; }

        [JsonProperty("temperature_2m_min")]

        public List<double> Temperature2mMinList { get; set; }

        [JsonProperty("sunrise")]
        public List<string> Sunrise { get; set; }

        [JsonProperty("sunset")]
        public List<string> Sunset { get; set; }

        [JsonProperty("precipitation_probability_max")]
        
        public List<int> PrecipitationProbabilityMaxList { get; set; }
    }
}

