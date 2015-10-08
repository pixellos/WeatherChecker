using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.Net;
using System.Globalization;

namespace WeatherChecker.Models
{
    public class OpenWeatherConnection : IWeatherConnection
    {
        #region Constants and properties
        public string _BaseAdress = "http://api.openweathermap.org/";
        public string _APINowWeather = "data/2.5/weather";
        public string _APIForecast3Hours = "data/2.5/forecast";
        public string _ByCityAdress = "?q=";
        public string _ByCityID = "?id=";
        public string _GetXML = "&mode=xml";
        public string APPID = "&APPID=29a20f74935ebf1b87a71157e2d58600";


        static public string _TakeNumbersOnlyAndDot = "1234567890.";
        static public string _TakeLetters = "\"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm ";
        public string _OpenWeatherMapApiJSONSeparator = "\":";

        /**        private const string _BaseAdress = "http://api.openweathermap.org/";
        private const string _ByCityAdress = "data/2.5/weather?q=";
        private const string _ByGeoCoordinates = "data/2.5/weather?lat=";
        private const string _ByGeoSeparator = "&lon=";
        private const string _ByCodeSeparator = ",";
        private const string _TakeNumbersOnlyAndDot = "1234567890.";
        private const string _TakeLetters = "\"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm ";
        private const string _OpenWeatherMapApiJSONSeparator = "\":";*/

        private string currentConnectionParametrs;
        private string _city;
        public string City
        {
            set
            {
                _city = value;
                currentConnectionParametrs = _ByCityAdress + _city + Language;
            }
            get
            {
                return _city;
            }
        }

        private string _cityCode;
        public string CityCode
        {
            set
            {
                _cityCode = value;
                currentConnectionParametrs = _ByCityID + _cityCode + Language;
            }
            get
            {
                return _cityCode;
            }
        }

		private string _LastRawData;
        public string RawData
        {
            get
            {
                return _LastRawData;
            }
        }

        public static string TakeNumbersOnlyAndDot
        {
            get
            {
                return _TakeNumbersOnlyAndDot;
            }
        }

        public string Language
        {
            get
            {
                return "&lang=pl";
            }

            set
            {
               
            }
        }

        #endregion
        private string _lastConnectionString;
        private string GetDataFromURI()
        {
            return GetDataFromURI(_lastConnectionString);
        }
        private string  GetDataFromURI(string connectionString)
        {
            
            using (var httpConnection = new WebClient())
            {
               return _LastRawData = httpConnection.DownloadString(connectionString + APPID);
            }
        }

        public IDateWeatherData GetDateWeatherData()
        {
            throw new NotImplementedException();
        }



        private IWeatherData ParseJSONToData()
        {
             float temp = float.Parse(RawData.ParseAPIData("main", "temp", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot),CultureInfo.InvariantCulture);
            float maxTemp = float.Parse(RawData.ParseAPIData("main", "temp_max", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot),CultureInfo.InvariantCulture);
            float minTemp = float.Parse(RawData.ParseAPIData("main", "temp_min", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            int humidity = int.Parse(RawData.ParseAPIData("main", "humidity", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float windSpeed = float.Parse(RawData.ParseAPIData("wind", "speed", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float windDegree = float.Parse(RawData.ParseAPIData("wind", "deg", _OpenWeatherMapApiJSONSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            string description = RawData.ParseAPIData("weather", "description", _OpenWeatherMapApiJSONSeparator, _TakeLetters);
            string mainWeather = RawData.ParseAPIData("weather", "main", _OpenWeatherMapApiJSONSeparator, _TakeLetters);

            return new WeatherData(description,temp,humidity,mainWeather,maxTemp,minTemp,windDegree,windSpeed) ;
        }

        public IWeatherData GetWeatherData()
        {
           
            try
            {
                GetDataFromURI(_BaseAdress + _APINowWeather + currentConnectionParametrs);
            }
            catch (Exception)
            {
                Console.WriteLine("There is no last working connectionString or you dont have internet!");
                return new WeatherData("NaN",0,0,"NaN",0,0,0,0);
            }
            return ParseJSONToData();
        }

      
    }
}
