using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.ComponentModel;

namespace WeatherChecker.ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {


        public MainViewModel()
        {
            WeatherConnection = new WeatherChecker.Models.OpenWeatherConnection();
            WeatherConnection.City = "Warsaw";
        }
        
        public IWeatherConnection WeatherConnection
        {
            get; set;
        }

        public string City
        {
            set {
                WeatherConnection.City = value;
                OnPropertyChanged("weatherData");
            }
            get { return WeatherConnection.City; }
        }


        public IWeatherData weatherData
        {
            get
            {
                return WeatherConnection.GetWeatherData();
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string paramName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(paramName));
            }
        }
    }
}
