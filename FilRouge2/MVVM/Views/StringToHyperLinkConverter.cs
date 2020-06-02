using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

namespace FilRouge2
{
    public class StringToHyperLinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string s_value && typeof(string) == targetType)
            {
                Hyperlink hyperl = new Hyperlink();
                hyperl.NavigateUri = new Uri(s_value);
                return hyperl;
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
