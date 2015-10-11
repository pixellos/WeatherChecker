using System;
using System.Collections.Generic;
using WeatherChecker.Abstracts;

namespace WeatherChecker.Models
{
    internal class DateWeatherData : IDateWeatherData
    {
        public Dictionary<DateTime, IWeatherData> WeatherDataByDateTime { get; set; }
    }
}