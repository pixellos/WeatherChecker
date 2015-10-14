using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WeatherChecker.Properties;
using WeatherChecker;
namespace WeatherChecker.Extension
{
    public class WeatherToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ( (string)value)
            {
                default:
                    return @"..\Resources\Slonecznie.jpg";
                case "\"Clouds\"":
                    return @"..\Resources\Pochmurno.jpg";
                    case "\"Clear\"":
                    return @"..\Resources\Slonecznie.jpg";
                case "\"Shower\"":
                    return @"..\Resources\Deszcz.jpg";
                case "\"Fog\"":
                    return @"..\Resources\Mgla.jpg";
                case "\"Few clouds\"":
                    return @"..\Resources\Zprzejasnieniami.jpg";
                case "\"Rain\"":
                    return @"..\Resources\Deszcz.jpg";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class KelvinToCelciusTemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = new Double() ;
            result = System.Convert.ToDouble(value);
            return Math.Round( result - 273.15,2) + " ºC ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) + 273.15 + "ºF";
        }
    }


    
}
