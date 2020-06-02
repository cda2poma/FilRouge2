using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string s_value && typeof(Visibility) == targetType)
            { return string.IsNullOrEmpty(s_value) ? Visibility.Collapsed : Visibility.Visible; }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { throw new NotSupportedException(); }
    }
}