using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    public interface IWeatherConnection
    {
        double Longitude { get; }
        double Latitude { get; }
        string City { get; }
        string CountryCode { get; }

        IWeatherData GetWeatherData();
        bool SetNewWeatherTarget(double longitude, double latitude);
        bool SetNewWeatherTarget(string city, string countryCode);
        bool SetNewWeatherTarget(string city);
    }
}
