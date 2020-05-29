using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    public class DescPreviewConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string s_value && typeof(string) == targetType)
            {
                if (s_value.Length >= 101)
                { return $"{s_value.Substring(0, 100)}…"; }
                else
                { return s_value; }
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
