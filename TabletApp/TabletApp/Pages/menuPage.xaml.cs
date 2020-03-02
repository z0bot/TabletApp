using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletApp;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menuPage : ContentPage
	{
        private List<Models.OrderItem> fullOrder;

		public menuPage ()
		{
			InitializeComponent ();

            //Entrees
            mpEntreeOne.Clicked += mpEntreeOne_Clicked;
            mpEntreeTwo.Clicked += mpEntreeTwo_Clicked;
            mpEntreeThree.Clicked += mpEntreeThree_Clicked;
            mpEntreeFour.Clicked += mpEntreeFour_Clicked;

            //Drinks
            mpDrinkOne.Clicked += mpDrinkOne_Clicked;
            mpDrinkTwo.Clicked += mpDrinkTwo_Clicked;
            mpDrinkThree.Clicked += mpDrinkThree_Clicked;

            //Sides
            mpSideOne.Clicked += mpSidesOne_Clicked;
            mpSideTwo.Clicked += mpSidesTwo_Clicked;
            mpSideThree.Clicked += mpSidesThree_Clicked;

            //Bottom Buttons
            mpRefillButton.Clicked += mpRefillButton_Clicked;
            mpCallServerButton.Clicked += mpCallServerButton_Clicked;
		}

        //Entrees
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

        private async void mpEntreeThree_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpEntreeFour_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        //Drinks
        private async void mpDrinkOne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpDrinkTwo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpDrinkThree_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        //Sides
        private async void mpSidesOne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpSidesTwo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        private async void mpSidesThree_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuItemPage());
        }

        //Bottom Buttons
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