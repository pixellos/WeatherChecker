using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;

namespace WeatherChecker.Models
{
    public class WeatherData : IWeatherData
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

        private float _farenheitTemperature;
        public float FarenheitTemperature
        {
            get
            {
                return _farenheitTemperature;
            }
        }

        private int _humidity;
        public int Humidity
        {
            get
            {
                return _humidity;
            }
        }

        private string _mainInformation;
        public string MainInformation
        {
            get
            {
                return _mainInformation;
            }
        }

        private float _maxFarenheitTemperature;
        public float MaxFarenheitTemperature
        {
            get
            {
                return _maxFarenheitTemperature;
            }
        }

        private float _minFarenheitTemperature;
        public float MinFarenheitTemperature
        {
            get
            {
                return _minFarenheitTemperature;
            }
        }

        private int _windDegree;
        public int WindDegree
        {
            get
            {
                return _windDegree;
            }
        }

        private float _windSpeed;
        public float WindSpeed
        {
            get
            {
                return _windSpeed;   
            }
        }
    }
}
