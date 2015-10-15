using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;

namespace WeatherChecker.Models
{
    public class UserInterface :IUserInterface
    {
        public string Temperature { get; } = "Temperatura";
        public string WindSpeed { get; } = "Szybość wiatru";
        public string WindDegree { get; } = "Kąt wiatru";
        public string Humidity { get; } = "Wilgotnosc powietrza";
        public string City { get; } = "Miasto";
    }
}
