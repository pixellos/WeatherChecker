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

        float FarenheitTemperature { get; }
        float MaxFarenheitTemperature { get; }
        float MinFarenheitTemperature { get; }

        int Humidity { get; }
        float WindSpeed { get; }
        int WindDegree { get; }
    }
}
