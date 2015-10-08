using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.Globalization;

namespace WeatherChecker.Models.Tests
{
    [TestClass()]
    public class OpenWeatherConnectionTest
    {
        public string TemplateJSON = "{\"coord\":{\"lon\":-0.13,\"lat\":51.51},\"weather\":[{\"id\":721,\"main\":\"Haze\",\"description\":\"haze\",\"icon\":\"50n\"}],\"base\":\"stations\",\"main\":{\"temp\":285.34,\"pressure\":1011,\"humidity\":82,\"temp_min\":283.71,\"temp_max\":286.48},\"visibility\":8000,\"wind\":{\"speed\":4.1,\"deg\":90},\"clouds\":{\"all\":88},\"dt\":1443990053,\"sys\":{\"type\":1,\"id\":5091,\"message\":0.0191,\"country\":\"GB\",\"sunrise\":1443938809,\"sunset\":1443979824},\"id\":2643743,\"name\":\"London\",\"cod\":200}";
        [TestMethod()]
        public void GetWeatherDataTest()
        {
            OpenWeatherConnection weather = new OpenWeatherConnection();
            weather.City="Krosno";
            IWeatherData data = weather.GetWeatherData();
            
            Assert.IsNotNull(data.MainInformation);
            Assert.IsNotNull(data.Description);
            Assert.IsNotNull(data.KelvinTemperature);
            Assert.IsNotNull(data.Humidity) ;
            Assert.IsNotNull(data.MaxKelvinTemperature);
            Assert.IsNotNull(data.MinKelnivTemperature);
            Assert.IsNotNull(data.WindDegree);
            Assert.IsNotNull(data.WindSpeed);
        }
        [TestMethod()]
        public void TestMultiHours()
        {
            OpenWeatherConnection weather = new OpenWeatherConnection() {_ByCityAdress= "data/2.5/forecast?q=", _OpenWeatherMapApiJSONSeparator="=\"" };
            weather.City = "Krosno";
            IWeatherData data = weather.GetWeatherData();

            Assert.AreNotEqual("NaN", data.Description);
            Assert.AreNotEqual("NaN", data.MainInformation);
            Assert.AreNotEqual(0, data.KelvinTemperature);
            Assert.AreNotEqual(0, data.Humidity);
            Assert.AreNotEqual(0, data.MaxKelvinTemperature);
            Assert.AreNotEqual(0, data.MinKelnivTemperature);
            Assert.AreNotEqual(0, data.WindDegree);
            Assert.AreNotEqual(0, data.WindSpeed);
        }


        [TestMethod()]
        public void GetWeatherAgainTest()
        {
            OpenWeatherConnection weather = new OpenWeatherConnection();
            weather.City = "Krosno";
            IWeatherData data = weather.GetWeatherData();

            Assert.AreNotEqual("NaN", data.Description);
            Assert.AreNotEqual("NaN", data.MainInformation);
            Assert.AreNotEqual(0,data.KelvinTemperature);
            Assert.AreNotEqual(0,data.Humidity);
            Assert.AreNotEqual(0,data.MaxKelvinTemperature);
            Assert.AreNotEqual(0,data.MinKelnivTemperature);
            Assert.AreNotEqual(0,data.WindDegree);
            Assert.AreNotEqual(0,data.WindSpeed);
        }


        [TestMethod()]
        public void Parsing()
        {
            OpenWeatherConnection weather = new OpenWeatherConnection();
            var s = TemplateJSON.ParseAPIData("main", "temp_max", "\":","0123456789.");
            var result = Double.Parse(s, CultureInfo.InvariantCulture);
            Assert.AreEqual(286.48,result);
        }

        [TestMethod()]
        public void SetNewWeatherTargetTestCityName()
        {
            IWeatherConnection weatherConnection = new OpenWeatherConnection();
            weatherConnection.City = "Krosno";
            Assert.AreEqual(weatherConnection.City, "Krosno");
        }
    }
}