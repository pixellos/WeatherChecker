using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    public interface IWeatherConnection
    {
       
        string City { get; set; }
        string CityCode { get; set; }
        string Language { get; set; }

        IWeatherData GetWeatherData();
        IDateWeatherData GetDateWeatherData();
    }
}
