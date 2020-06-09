using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FilRouge2
{
    class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime value_d && typeof(string) == targetType)
            {
                StringBuilder sb = new StringBuilder($"{value_d.Day}{(value_d.Day == 1 ? "er " : " ")}");
                switch (value_d.Month)
                {
                    case 1:
                        sb.Append("janvier ");
                        break;
                    case 2:
                        sb.Append("février ");
                        break;
                    case 3:
                        sb.Append("mars ");
                        break;
                    case 4:
                        sb.Append("avril ");
                        break;
                    case 5:
                        sb.Append("mai ");
                        break;
                    case 6:
                        sb.Append("juin ");
                        break;
                    case 7:
                        sb.Append("juillet ");
                        break;
                    case 8:
                        sb.Append("août ");
                        break;
                    case 9:
                        sb.Append("septembre ");
                        break;
                    case 10:
                        sb.Append("octobre ");
                        break;
                    case 11:
                        sb.Append("novembre ");
                        break;
                    case 12:
                        sb.Append("décembre ");
                        break;
                }
                sb.Append(value_d.Year);
                return sb.ToString();
            }
            else
            { throw new InvalidCastException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { throw new NotImplementedException(); }
        /*
         switch (date.Month)
            {
                case 1:
                    sb.Append("janvier ");
                    break;
                case 2:
                    sb.Append("février ");
                    break;
                case 3:
                    sb.Append("mars ");
                    break;
                case 4:
                    sb.Append("avril ");
                    break;
                case 5:
                    sb.Append("mai ");
                    break;
                case 6:
                    sb.Append("juin ");
                    break;
                case 7:
                    sb.Append("juillet ");
                    break;
                case 8:
                    sb.Append("août ");
                    break;
                case 9:
                    sb.Append("septembre ");
                    break;
                case 10:
                    sb.Append("octobre ");
                    break;
                case 11:
                    sb.Append("novembre ");
                    break;
                case 12:
                    sb.Append("décembre ");
                    break;
            }
         */
    }
}
