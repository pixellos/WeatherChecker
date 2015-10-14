using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.Net;
using System.Globalization;
using System.Security.AccessControl;
using System.Xml;
using System.Text;
using System.Windows;
using System.Windows.Documents;
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
        private const string TakeLetters = "\"QWERTYUIOPASDFGHJKLZXCVBNMĘÓĄŚŁŻŹĆŃqwertyuiopasdfghjklzxcvbnmąężźćółńśę ";
        private const string OpenWeatherMapApiJsonSeparator = "\":";

        private string _currentConnectionParametrs;

        public OpenWeatherConnection()
        {
            City = City;

        }
        //private string _city;
        public string City
        {
            set
            {
                Properties.Settings.Default.City = value;
                Properties.Settings.Default.Save();
                _currentConnectionParametrs = ByCityAdress + City + Language+Appid;
            }
            get { return Properties.Settings.Default.City; }
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
               return httpConnection.DownloadString(connectionString);
            }
        }

        public IDateWeatherData GetDateWeatherData()
        {
            try
            {
                return parseToDateWeatherData(BaseAdress + ApiForecast3Hours + _currentConnectionParametrs + GetXml);
            }
            catch (Exception exception)
            {
                return null;
                Console.WriteLine("Invaild input");
            }
           
        }

        private IDateWeatherData parseToDateWeatherData(string connectionString)
        {
            using (XmlReader xmlReader = XmlReader.Create(connectionString + "&units=standard")) // sample api call = http://api.openweathermap.org/data/2.5/forecast?q=Krosno&lang=pl&APPID=29a20f74935ebf1b87a71157e2d58600&mode=xml&units=standard
            {
                xmlReader.ReadToFollowing("forecast");

                DateWeatherData dateWeatherData = new DateWeatherData();
                for (int i = 0; i < 39; i++)
                {
                    DateTime forecastDateTime = DateTime.Parse(
                       xmlReader.ParseApiData("time", "from"));

                    IWeatherData date = ParseXMLToWeatherData(xmlReader);
                 
                    dateWeatherData.Add(forecastDateTime,date);
                }

                return dateWeatherData;
            }
        }

        private static IWeatherData ParseXMLToWeatherData(XmlReader xmlReader)
        {
            string weatherDescription = xmlReader.ParseApiData(
                        "symbol", "name");

            string weatherType = xmlReader.ParseApiData(
                "precipitation", "type");

            float weatherWindDegree = float.Parse(
                xmlReader.ParseApiData("windDirection", "deg"));

            float windSpeed = float.Parse(xmlReader.ParseApiData(
                "windSpeed", "mps"));

            float temperature = float.Parse(xmlReader.ParseApiData(
                "temperature", "value"));

            int humidity = int.Parse(xmlReader.ParseApiData(
                "humidity", "value"));

            return new WeatherData(weatherDescription, temperature, humidity, weatherType, weatherWindDegree, windSpeed);

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
            string description = source.ParseAPIData("weather", "description", OpenWeatherMapApiSeparator, TakeLetters).Replace('\"',' ');
            string mainWeather = source.ParseAPIData("weather", "main", OpenWeatherMapApiSeparator, TakeLetters);
            return new WeatherData(description, temp, humidity, mainWeather,windDegree, windSpeed);
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

       
    }
}
