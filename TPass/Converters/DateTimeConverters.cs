using System;
//using Humanizer;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace TPass.Converters {

    class CheckInTimeDisplayConverter : IValueConverter {

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    try {

		return DateTime.Now;
		//return Device.OS == TargetPlatform.iOS ? session.GetDisplayTime() : session.GetDisplayName();
	    }
	    catch (Exception ex) {

		Debug.WriteLine("Unable to convert: " + ex);
	    }

	    return string.Empty;
	}


	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
	    throw new NotImplementedException();
	}
    }



}
