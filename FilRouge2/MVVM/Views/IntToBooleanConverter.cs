using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    class IntToBooleanConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty isReversedProperty = DependencyProperty.Register(nameof(IsReversed), typeof(bool), typeof(IntToBooleanConverter), new PropertyMetadata(null));
        public bool IsReversed
        {
            get { return (bool)GetValue(isReversedProperty); }
            set { SetValue(isReversedProperty, value); }
        }

        public static readonly DependencyProperty isRetryButtonProperty = DependencyProperty.Register(nameof(IsRetryButton), typeof(bool), typeof(IntToBooleanConverter), new PropertyMetadata(null));
        public bool IsRetryButton
        {
            get { return (bool)GetValue(isRetryButtonProperty); }
            set { SetValue(isRetryButtonProperty, value); }
        }

        public static readonly DependencyProperty areThereTypesPosteInListProperty = DependencyProperty.Register(nameof(AreThereTypesPosteInList), typeof(bool), typeof(IntToBooleanConverter), new PropertyMetadata(null));
        public bool AreThereTypesPosteInList
        {
            get { return (bool)GetValue(areThereTypesPosteInListProperty); }
            set { SetValue(areThereTypesPosteInListProperty, value); }
        }

        public static readonly DependencyProperty isFilterLimitNull = DependencyProperty.Register(nameof(IsFilterLimitNull), typeof(bool), typeof(IntToBooleanConverter), new PropertyMetadata(null));
        public bool IsFilterLimitNull
        {
            get { return (bool)GetValue(isFilterLimitNull); }
            set { SetValue(isFilterLimitNull, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int i_value && typeof(bool) == targetType)
            {
                if (i_value == 0)
                {
                    if (!IsFilterLimitNull)
                    { return false; }
                    else if ((!IsRetryButton || AreThereTypesPosteInList))
                    { return IsReversed ? false : true; }
                    else
                    { return false; }
                }
                else if (i_value < 0 && IsRetryButton)
                { return true; }
                else
                { return IsReversed ? true : false; }
            }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { throw new NotSupportedException(); }
    }
}
