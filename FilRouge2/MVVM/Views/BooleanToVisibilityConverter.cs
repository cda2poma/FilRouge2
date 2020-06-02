using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    public class BooleanToVisibilityConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty isReversedProperty = DependencyProperty.Register(nameof(IsReversed), typeof(bool), typeof(BooleanToVisibilityConverter), new PropertyMetadata(null));
        public bool IsReversed
        {
            get { return (bool)GetValue(isReversedProperty); }
            set { SetValue(isReversedProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && typeof(Visibility) == targetType)
            {
                if ((bool)value)
                    return IsReversed ? Visibility.Collapsed : Visibility.Visible;
                else
                    return IsReversed ? Visibility.Visible : Visibility.Collapsed;
            }
            else
                throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
