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
        public static IWeatherData ERRORMODEL = new WeatherData("NaN",0,0,"NaN",0,0);
        public WeatherData(string description,float farenheitTemperature,int humidity,string mainInformation,
             float windDegree, float windSpeed)
        {
            _description = description;
            _farenheitTemperature = farenheitTemperature;
            _humidity = humidity;
            _mainInformation = mainInformation;
  
            _windDegree = windDegree;
            _windSpeed = windSpeed;

        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

        private float _farenheitTemperature;
        public float KelvinTemperature
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


        private float _windDegree;
        public float WindDegree
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
