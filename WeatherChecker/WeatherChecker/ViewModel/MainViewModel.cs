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
        private readonly IWeatherConnection _weatherConnection;

        void TakeWeatherData()
        {
            ForecastPartialViewModel = new ForecastPartialViewModel(
                _weatherConnection.GetDateWeatherData(),
                () => {
                          OnPropertyChanged("ForecastPartialViewModel"); }
                );
        }

        void BackgroundThreat()
        {
            while (true)
            {
                Thread.Sleep(1000);
                WeatherData = _weatherConnection.GetWeatherData();
                OnPropertyChanged("");
            }
        }

        public MainViewModel()
        {
            _weatherConnection = new OpenWeatherConnection();
            if (_weatherConnection.City == null)
            {
                _weatherConnection.City = "New York";
            }
            WeatherData = _weatherConnection.GetWeatherData();

            ForecastPartialViewModel = new ForecastPartialViewModel(_weatherConnection.GetDateWeatherData(), ()=> {OnPropertyChanged("ForecastPartialViewModel");});
            OnPropertyChanged("");

            new Thread(BackgroundThreat) {IsBackground = true}.Start();
        }

       
        public string City
        {
            set
            {
                if (value !=null)
                {
                    _weatherConnection.City = value;
                    new Thread(TakeWeatherData) {IsBackground = true}.Start();
                }
            }
            get { return _weatherConnection.City; }
        }

        public IWeatherData WeatherData { get; private set; }
       
        #region forecast

        private ForecastPartialViewModel _forecastPartialViewModel;
        public ForecastPartialViewModel ForecastPartialViewModel {
            get
            {
                return _forecastPartialViewModel;
            }
            private set
            {
                _forecastPartialViewModel = value;
                OnPropertyChanged("ForecastPartialViewModel");
            }

        }
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
