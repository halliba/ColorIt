using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorIt
{
    internal class MilliMeterToMeterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int milliMeter ? (double)milliMeter / 1000 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double meter ? (int)meter*1000 : 0;
        }
    }
}