using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    public interface IDateWeatherData : IWeatherData
    {
        DateTime StartDateTime { get;}
        DateTime EndDateTime { get; }
    }
}
