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
    public partial class RandomGamePage : ContentPage
    {
        static int winner;
        public RandomGamePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            winner = new Random(System.DateTime.Now.Second).Next(1, 5);
        }

        async void onClicked(object sender, EventArgs e)
        {
            if (((Button)sender).Text == winner.ToString())
            {
                await DisplayAlert("Congratulations!", "Correct! The number was " + winner.ToString() + ".", "Yay!");
                // Print/email coupon
            }
            else
            {
                await DisplayAlert("Sorry!", "You guessed wrong. The number was " + winner.ToString() + ".", "Aww!");
            }

            await Navigation.PopAsync();
        }

        async void OnRefillButtonClicked(object sender, EventArgs e)
        {
            // Send refill request


            await DisplayAlert("Refill", "Server Notified of Refill Request", "OK");
        }

        async void OnServerButtonClicked(object sender, EventArgs e)
        {
            // Send Help Request

            await DisplayAlert("Help Request", "Server Notified of Help Request", "OK");
        }

        // Prevent going back to previous pages, must complete game to continue
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}