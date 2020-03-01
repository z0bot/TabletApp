using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menuPage : ContentPage
	{
		public menuPage ()
		{
			InitializeComponent ();

            mpEntreeOne.Clicked += mpEntreeOne_Clicked;
            mpEntreeTwo.Clicked += mpEntreeTwo_Clicked;

            mpRefillButton.Clicked += mpRefillButton_Clicked;
            mpCallServerButton.Clicked += mpCallServerButton_Clicked;
		}

        private async void mpEntreeOne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage("STREET BUNG", "A lot of Meat")
            {
                BackgroundImageSource = "steakOne.PNG"
            });
        }
        private async void mpEntreeTwo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpRefillButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Refill Request Sent", "Your Server is on their way", "Thristy Aint'cha");
        }

        private async void mpCallServerButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Server Summoned", "Pray to your god while you can", "Run");
        }
    }
}