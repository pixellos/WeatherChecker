using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Abstracts;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WeatherChecker.Models;
using WeatherChecker.ViewModel.Helper;
using WeatherChecker.ViewModel.PartialViewModel;

namespace WeatherChecker.ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {
        private readonly IWeatherConnection WeatherConnection;

        private Thread _backgroundThread;
        public MainViewModel()
        {
            dateWeatherPropertyChangedAction = () => { DateWeatherDataPropertyChanged(); };
            WeatherConnection = new OpenWeatherConnection();
            WeatherData = WeatherConnection.GetWeatherData();
            dateWeatherData = WeatherConnection.GetDateWeatherData();
            ForecastPartialViewModel = new ForecastPartialViewModel(dateWeatherData,dateWeatherPropertyChangedAction);
            OnPropertyChanged("");
            _backgroundThread = new Thread(BackgroundThreat) {IsBackground = true};
            _backgroundThread.Start();
           

        }

        private Action dateWeatherPropertyChangedAction;
        void DateWeatherDataPropertyChanged()
        {
            OnPropertyChanged("ForecastPartialViewModel");
            MessageBox.Show("DelegateHit");
        }

        public string City
        {
            set
            {
                if (value != string.Empty)
                {
                    WeatherConnection.City = value;
                    new Thread(TakeWeatherData)
                    {
                        IsBackground = true
                    }
                    .Start();
                }
            }
            get { return WeatherConnection.City; }
        }

        public IWeatherData WeatherData { get; private set; }

        void BackgroundThreat()
        {
            while (true)
            {
                Thread.Sleep(1000);
                WeatherData = WeatherConnection.GetWeatherData();
                OnPropertyChanged("weatherData");
            }
        }

        void TakeWeatherData()
        {
            ForecastPartialViewModel = new ForecastPartialViewModel(
                WeatherConnection.GetDateWeatherData()
                ,dateWeatherPropertyChangedAction);
            OnPropertyChanged("ForecastPartialViewModel");
        }
        #region forecast

        IDateWeatherData dateWeatherData;

        public ForecastPartialViewModel ForecastPartialViewModel { get; set; }
        #endregion forecast



        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string paramName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(paramName));
            }
        }
        #endregion PropertyChanged

      
    }
}
