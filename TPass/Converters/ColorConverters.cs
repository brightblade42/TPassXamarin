using System;
using System.Globalization;
using Xamarin.Forms;

namespace TPass.Converters {

    public class StudentSuspensionColorConverter : IValueConverter {

	//should be a style selection?
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    if (value == null)
		return string.Empty;
	   
	    return (bool)value ? Color.Red : Color.Blue;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    throw new NotImplementedException();
	}
    }
}
