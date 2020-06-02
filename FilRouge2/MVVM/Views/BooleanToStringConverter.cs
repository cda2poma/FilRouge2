using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    public class BooleanToStringConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && typeof(string) == targetType)
            {
                if ((bool)value)
                { return "Connexion…"; }
                else
                { return "Filtrer"; }
            }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
