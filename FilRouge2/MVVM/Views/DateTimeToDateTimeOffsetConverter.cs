using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime value_d && typeof(DateTimeOffset) == targetType)
            { return new DateTimeOffset(((DateTime)value).ToUniversalTime()); }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime value_do && typeof(DateTime) == targetType)
            { return ((DateTimeOffset)value).DateTime; }
            else
            { throw new InvalidCastException(); }
        }
    }
}
