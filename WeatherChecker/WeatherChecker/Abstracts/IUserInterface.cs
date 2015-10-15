using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Abstracts
{
    public interface IUserInterface
    {
        string Temperature { get; }
        string WindSpeed { get; }
        string WindDegree { get; }
        string Humidity { get; }
        string City { get; }
    }

   
}
