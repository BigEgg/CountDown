using System;
using System.Globalization;
using System.Windows.Data;
using CountDown.Presentation.Properties;

namespace CountDown.Presentation.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeToStringConverter : IValueConverter
    {
        private static readonly DateTimeToStringConverter defaultInstance = new DateTimeToStringConverter();

        public static DateTimeToStringConverter Default { get { return defaultInstance; } }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime temp = (DateTime)value;
                double days = (temp.Date - DateTime.Now.Date).TotalDays;

                if (days == 0.0)         //  Same day
                    return temp.ToString(Resources.DateString);
                else
                    return temp.ToString(Resources.DateStringWithDays);
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
