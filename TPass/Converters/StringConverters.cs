using System;
using System.Globalization;
using Xamarin.Forms;


namespace TPass.Converters {

    public class HasStudentCheckedInTextConverter : IValueConverter {
	    	
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    if (value == null)
		return string.Empty;

	    return (bool)value ? "Checked in" : "Not Checked in";
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    throw new NotImplementedException();
	}
    }

}
