using System;
using System.Collections.Generic;
using WeatherChecker.Abstracts;

namespace WeatherChecker.Models
{
    internal class DateWeatherData : IDateWeatherData
    {
        public DateWeatherData()
        {
           DateTimes = new List<DateTime>();
            WeatherDatas = new List<IWeatherData>();
        }
        public List<DateTime> DateTimes { get; set; }
        public List<IWeatherData> WeatherDatas { get; set; }

        public void Add(DateTime dateTime, IWeatherData weatherData)
        {
            DateTimes.Add(dateTime);
            WeatherDatas.Add(weatherData);
        }

        public int Count()
        {
           return DateTimes.Count;
        }
    }
}