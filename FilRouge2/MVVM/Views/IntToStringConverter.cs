using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int i_value && typeof(string) == targetType)
            {
                if (i_value < 0)
                { return "Retenter la connexion"; }
                else if (i_value > 0)
                { return "Connexion au serveur…"; }
                else
                { return "Filtrer les offres"; }
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
