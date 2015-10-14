using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    public interface IWeatherData
    {
        string MainInformation { get; }
        string Description { get; }
        float KelvinTemperature { get; }
       

        int Humidity { get; }
        float WindSpeed { get; }
        float WindDegree { get; }
    }
}
