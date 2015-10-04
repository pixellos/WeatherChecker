using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;


namespace WeatherChecker.Models.Tests
{
    [TestClass()]
    public class OpenWeatherConnectionTest
    {
        public string TemplateJSON = "{\"coord\":{\"lon\":-0.13,\"lat\":51.51},\"weather\":[{\"id\":721,\"main\":\"Haze\",\"description\":\"haze\",\"icon\":\"50n\"}],\"base\":\"stations\",\"main\":{\"temp\":285.34,\"pressure\":1011,\"humidity\":82,\"temp_min\":283.71,\"temp_max\":286.48},\"visibility\":8000,\"wind\":{\"speed\":4.1,\"deg\":90},\"clouds\":{\"all\":88},\"dt\":1443990053,\"sys\":{\"type\":1,\"id\":5091,\"message\":0.0191,\"country\":\"GB\",\"sunrise\":1443938809,\"sunset\":1443979824},\"id\":2643743,\"name\":\"London\",\"cod\":200}";
        [TestMethod()]
        public void GetWeatherDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Parsing()
        {
            OpenWeatherConnection weather = new OpenWeatherConnection();
            var s = weather.ParseStringBeforeSemicolon("main", "temp_max", "\":", TemplateJSON);
            Assert.AreEqual("286.48",s);

        }

        [TestMethod()]
        public void SetNewWeatherTargetTestCityName()
        {
            IWeatherConnection weatherConnection = new OpenWeatherConnection();
            weatherConnection.SetNewWeatherTarget("Krosno");

            Assert.AreEqual(weatherConnection.City, "Krosno");
           
        }

        [TestMethod()]
        public void SetNewWeatherTargetTestCityNameAndCountryCode()
        {
            IWeatherConnection weatherConnection = new OpenWeatherConnection();
            weatherConnection.SetNewWeatherTarget("Krosno","616");

            Assert.AreEqual(weatherConnection.City, "Krosno");
            Assert.AreEqual(weatherConnection.CountryCode, "616");
            
        }

        [TestMethod()]
        public void SetNewWeatherTargetTestLatitudeLongitude()
        {
            IWeatherConnection weatherConnection = new OpenWeatherConnection();
            weatherConnection.SetNewWeatherTarget(61.16, 16.61);

            Assert.AreEqual(weatherConnection.City, null);
            Assert.AreEqual(weatherConnection.CountryCode, null);
            Assert.AreEqual(weatherConnection.Latitude, 16.61);
            Assert.AreEqual(weatherConnection.Longitude, 61.16);
        }
    }
}