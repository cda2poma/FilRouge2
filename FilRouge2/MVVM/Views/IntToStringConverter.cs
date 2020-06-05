using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    class IntToStringConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty areThereTypesPosteInListProperty = DependencyProperty.Register(nameof(AreThereTypesPosteInList), typeof(bool), typeof(IntToStringConverter), new PropertyMetadata(null));
        public bool AreThereTypesPosteInList
        {
            get { return (bool)GetValue(areThereTypesPosteInListProperty); }
            set { SetValue(areThereTypesPosteInListProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int i_value && typeof(string) == targetType)
            {
                if (i_value < 0)
                { return "Retenter la connexion"; }
                else if (i_value > 0)
                { return "Connexion au serveur…"; }
                else
                { return AreThereTypesPosteInList ? "Filtrer les offres" : "Aucune offre dans la base de données"; }
            }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { throw new NotSupportedException(); }
    }
}
