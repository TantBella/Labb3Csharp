using System;
using System.Globalization;
using System.Windows.Data;


//Gör knappen disabled om passet är bokat
namespace Labb3
{
    public class DisableBookingButton : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isBooked)
            {
                return !isBooked;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}