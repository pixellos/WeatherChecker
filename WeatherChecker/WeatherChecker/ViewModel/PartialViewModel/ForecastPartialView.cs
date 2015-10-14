using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherChecker.Abstracts;
using WeatherChecker.ViewModel.Helper;


namespace WeatherChecker.ViewModel.PartialViewModel
{
    public class ForecastPartialViewModel
    {
        private const int CountOfSections = 5;
        private IDateWeatherData _dateWeatherData;
        private Delegate _onPropertyChangedDelegate;
        public ForecastPartialViewModel(IDateWeatherData dateWeatherData, Delegate onPropertyChangedDelegate)
        {
            _dateWeatherData = dateWeatherData;
            loadDataIntoSection();
            _onPropertyChangedDelegate = onPropertyChangedDelegate;
        }

        private int _startId=0;

        public ViewModel.Helper.Command GetNextCommand
        {
            get
            {
                return new Command(new Action((() =>
                {
                    loadDataIntoSection();
                    _onPropertyChangedDelegate.DynamicInvoke();
                    MessageBox.Show("Next");
                })));
            }
        }

        public Command GetPervCommand
        {
            get
            {
                return new Command(new Action((() =>
                {
                    if (_startId -1 > 0)
                    {
                        _startId--;
                    }
                    loadDataIntoSection();
                    _onPropertyChangedDelegate.DynamicInvoke();
                    MessageBox.Show("Perv");
                })));
            }
        }

        private void loadDataIntoSection()
        {
            for (int i = 0; i < CountOfSections; i++)
            {
                if (_dateWeatherData.DateTimes.Count >= _startId + i)
                {
                    SectionDateTime[i] = _dateWeatherData.DateTimes[_startId + i];
                    SectionWeatherData[i] = _dateWeatherData.WeatherDatas[_startId + 1];
                }
            }
        }

        #region Sections
        public DateTime[] SectionDateTime { get; set; } = new DateTime[CountOfSections];
        public IWeatherData[] SectionWeatherData { get; set; } = new IWeatherData[CountOfSections];

      

        #endregion


    }
}
