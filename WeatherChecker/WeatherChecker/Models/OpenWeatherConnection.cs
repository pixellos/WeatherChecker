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
        private const string BaseAdress = "http://api.openweathermap.org/";
        private const string ApiNowWeather = "data/2.5/weather";
        private const string ApiForecast3Hours = "data/2.5/forecast";
        private const string ByCityAdress = "?q=";
        private const string ByCityId = "?id=";
        private const string GetXml = "&mode=xml";
        private const string Appid = "&APPID=29a20f74935ebf1b87a71157e2d58600";
        private const string TakeNumbersOnlyAndDot = "1234567890.";
        private const string TakeLetters = "\"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm ";
        private const string OpenWeatherMapApiJsonSeparator = "\":";

        private string _currentConnectionParametrs;
        private string _city;
        public string City
        {
            set
            {
                _city = value;
                _currentConnectionParametrs = ByCityAdress + _city + Language;
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
                _currentConnectionParametrs = ByCityId + _cityCode + Language;
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
        
        private static string  GetDataFromUri(string connectionString)
        {
            using (var httpConnection = new WebClient())
            {
               return httpConnection.DownloadString(connectionString + Appid);
            }
        }

        public IDateWeatherData GetDateWeatherData(string source, string OpenWeatherMapApiSeparator)
        {
            throw new NotImplementedException();
            string temporaryData;
            try
            {
                temporaryData = GetDataFromUri(BaseAdress + ApiForecast3Hours + _currentConnectionParametrs);
            }
            catch (Exception)
            {
                Console.WriteLine("There is no last working connectionString or you dont have internet!");
                return new DateWeatherData();
            }
            ParseToIWeatherData(temporaryData, OpenWeatherMapApiJsonSeparator);
        }


        private static IWeatherData ParseToIWeatherData(string source,string OpenWeatherMapApiSeparator )
        {
            if (source == null || source == "{\"cod\":\"404\",\"message\":\"Error: Not found city\"}\n")
            {
                return WeatherData.ERRORMODEL;
            }
            float temp = float.Parse(source.ParseAPIData("main", "temp", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float maxTemp = float.Parse(source.ParseAPIData("main", "temp_max", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float minTemp = float.Parse(source.ParseAPIData("main", "temp_min", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            int humidity = int.Parse(source.ParseAPIData("main", "humidity", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float windSpeed = float.Parse(source.ParseAPIData("wind", "speed", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            float windDegree = float.Parse(source.ParseAPIData("wind", "deg", OpenWeatherMapApiSeparator, TakeNumbersOnlyAndDot), CultureInfo.InvariantCulture);
            string description = source.ParseAPIData("weather", "description", OpenWeatherMapApiSeparator, TakeLetters);
            string mainWeather = source.ParseAPIData("weather", "main", OpenWeatherMapApiSeparator, TakeLetters);
            return new WeatherData(description, temp, humidity, mainWeather, maxTemp, minTemp, windDegree, windSpeed);
        }

        private IWeatherData ParseJSONToIWeatherData(string source)
        {
            return ParseToIWeatherData(source, OpenWeatherMapApiJsonSeparator);
        }

        public IWeatherData GetWeatherData()
        {
            string temporaryDataString;
            IWeatherData temporarData;
            try
            {
               temporaryDataString = GetDataFromUri(BaseAdress + ApiNowWeather + _currentConnectionParametrs);
                temporarData = ParseJSONToIWeatherData(temporaryDataString);

            }
            catch (Exception)
            {
                Console.WriteLine("There is no last working connectionString or you dont have internet!");
                return WeatherData.ERRORMODEL;
            }
            return temporarData;
        }

        public IDateWeatherData GetDateWeatherData()
        {
            throw new NotImplementedException();
        }
    }
}
