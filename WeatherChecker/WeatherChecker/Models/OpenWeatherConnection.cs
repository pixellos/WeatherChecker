using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.Net.Http;
using System.Net.Http.Headers;


namespace WeatherChecker.Models
{
    public class OpenWeatherConnection : IWeatherConnection
    {
        private const string _BaseAdress = "api.openweathermap.org/";
        private const string _ByCityAdress = "data/2.5/weather?q=";

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
        }

        private string _countryCode;
        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
        }

        private double _latitude;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
        }

        private double _longitude;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
        }

		private string _LastRawData;
        private async Task GetDataFromServer()
        {
            using (var httpConnection = new HttpClient())
            {
                httpConnection.BaseAddress = new Uri(_BaseAdress);
                httpConnection.DefaultRequestHeaders.Accept.Clear();
                httpConnection.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpConnection.GetAsync(_ByCityAdress + _city + "," + _countryCode);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                    _LastRawData = response.ToString();
                }
            }

        }

        public string ParseStringBeforeSemicolon(string sectionName, string parametrName, string parametrSighs, string data)
        {
            string result = "";
            result = data.Substring(
                data.IndexOf(sectionName)
                );
               ;

            result = result.Substring(result.IndexOf(parametrName+parametrSighs)+parametrName.Length+parametrSighs.Length);
            result = result.Remove(6);



            return result;
        }

		private IWeatherData ParseJSONToData(string rawData)
        {

            return new WeatherData();
        }

        public IWeatherData GetWeatherData()
        {
            GetDataFromServer().Wait();
            return ParseJSONToData(_LastRawData);
        }

        public bool SetNewWeatherTarget(string city)
        {
            _city = city;
            return true;
        }

        public bool SetNewWeatherTarget(string city, string countryCode)
        {
            _city = city;
            _countryCode = countryCode;
            return true;
        }

        public bool SetNewWeatherTarget(double longitude, double latitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            return true;
        }
    }
}
