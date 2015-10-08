using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
namespace WeatherChecker.Extension
{
    public class WeatherToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ( (string)value)
            {
                default:
                    return null;
                    break;
                case "\"Clouds\"":
                    return @"http://images.freeimages.com/images/previews/838/cloud-1573010.jpg";
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
            
            return Math.Round( result - 273.15,2);
                
              
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) + 273.15;
        }
    }
}
