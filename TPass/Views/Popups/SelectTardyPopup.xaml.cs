using System;

using Xamarin.Forms;

namespace TPass.Views {

    public partial class SelectTardyPopup : ContentPage {
	    
	    public SelectTardyPopup()
		{
			InitializeComponent();
			
		}
	
		private async void Button_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
