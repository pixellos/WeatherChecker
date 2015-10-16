using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WeatherChecker.Abstracts;
using WeatherChecker.ViewModel.Helper;


namespace WeatherChecker.ViewModel.PartialViewModel
{
    public class ForecastPartialViewModel
    {
        private const int CountOfSections = 5;
        private readonly IDateWeatherData _dateWeatherData;
        private readonly Action _onClickChanged;

        public ForecastPartialViewModel(IDateWeatherData dateWeatherData)
        {
            _dateWeatherData = dateWeatherData;
            LoadDataIntoSection();
        }

        public ForecastPartialViewModel(IDateWeatherData dateWeatherData, Action OnClickChanged) : this(dateWeatherData)
        {
            _onClickChanged = OnClickChanged;

        }
        private int _startId=0;

        public ViewModel.Helper.Command GetNextCommand
        {
            get
            {
                if (_dateWeatherData != null)
                {
                    return new Command(new Action((() =>
                    {
                        if (_startId <= _dateWeatherData.DateTimes.Count - CountOfSections)
                        {
                            _startId++;
                        }
                        LoadDataIntoSection();
                        _onClickChanged.Invoke();
                    })));
                }
                return null;
            }
        }

        public Command GetPervCommand
        {
            get
            {
                if (_dateWeatherData != null)
                {
                return new Command(new Action((() =>
                {
                    if (_startId -1 > 0)
                    {
                        _startId--;
                    }
                    LoadDataIntoSection();
                    _onClickChanged.Invoke();
                })));
            }
                return null;
        }
        }

        private void LoadDataIntoSection()
        {
            try
            {
                if (_dateWeatherData != null)
                {
                    for (int i = 0; i < CountOfSections; i++)
                    {
                        if (_dateWeatherData.DateTimes.Count - 1 > _startId + i)
                        {
                            SectionDateTime[i] = _dateWeatherData.DateTimes[_startId + i];
                            var test = _dateWeatherData.WeatherDatas[_startId + i];

                            WeatherDataArray[i] = test;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Sections
        public DateTime[] SectionDateTime { get; set; } = new DateTime[CountOfSections];
        public List<IWeatherData> WeatherDataArray { get; set; } = new List<IWeatherData>(6) {null,null,null,null,null};
        public IWeatherData[] SectionWeatherData { get; set; } = new IWeatherData[CountOfSections];
        #endregion
    }
}
