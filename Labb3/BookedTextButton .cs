using System;
using System.Globalization;
using System.Windows.Data;


//Ändrar texten på bokningsknappen i listan till bokad
namespace Labb3
{
    public class BookedTextButton : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isBooked)
            {
                return isBooked ? "Bokad" : "Boka";
            }
            return "Boka";

            throw new InvalidCastException("Expected a boolean value.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
