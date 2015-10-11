using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using WeatherChecker.Models;

namespace WeatherChecker.ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {
        public MainViewModel()
        {
            WeatherConnection = new OpenWeatherConnection();
            WeatherConnection.City = "Warsaw";
            WeatherData = WeatherConnection.GetWeatherData();
            new Thread(BackgroundThreat).Start();
        }

        void BackgroundThreat()
        {
            while (true)
            {
                Thread.Sleep(1000);
                WeatherData = WeatherConnection.GetWeatherData();
                OnPropertyChanged("weatherData");
            }
        }

        public IWeatherConnection WeatherConnection
        {
            get; set;
        }

        public string City
        {
            set
            {
                if (value != string.Empty)
                {
                    WeatherConnection.City = value;
                }
            }
            get { return WeatherConnection.City; }
        }
        
        public IWeatherData WeatherData { get; private set; }

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
