using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    


    public interface IDateWeatherData
    {
        List<DateTime> DateTimes { get;  }

        List<IWeatherData> WeatherDatas { get;  }

        void Add(DateTime dateTime, IWeatherData weatherData);
        

    }
}
