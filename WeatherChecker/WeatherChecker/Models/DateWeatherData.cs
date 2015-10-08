using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;

namespace WeatherChecker.Models
{
    class DateWeatherData : WeatherData , IDateWeatherData 
    {
        public DateWeatherData(string description, float farenheitTemperature, int humidity, string mainInformation,
            float maxFarenheitTemperature, float minFarenheitTemperature, float windDegree, float windSpeed) : base(description,farenheitTemperature,humidity,mainInformation,maxFarenheitTemperature,minFarenheitTemperature,windDegree,windSpeed)
        {

        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get
            {
                return _endDateTime;
            }

          
        }

        private DateTime _startDate;
        public DateTime StartDateTime
        {
            get
            {
                return _startDate;
            }

           
        }
    }
}
