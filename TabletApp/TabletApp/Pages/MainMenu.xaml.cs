using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;

using Realms;

namespace TabletApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public static MainMenu _instance;

        private bool isClickedEnabled = false;

        public MainMenu()
        {
            InitializeComponent();

            _instance = this;

            RealmManager.AddOrUpdate<TableManager>(new TableManager
            {
                table = "Table 19"
            });

            uxOrderButton.Clicked += uxOrderButton_Clicked;
            uxPlayButton.Clicked += UxPlayButton_Clicked;
            uxCallServerButton.Clicked += uxCallServerButton_Clicked;
            uxRefillButton.Clicked += uxRefillButton_Clicked;
        }

        public static void OnReturn()
        {
            if (RealmManager.All<OrderedList>().Count() > 0)
            {
                _instance.uxCheckOutButton.BackgroundColor = new Color(0x24BF87);
                if (!_instance.isClickedEnabled) _instance.uxCheckOutButton.Clicked += uxCheckOutButton_Clicked;
                _instance.isClickedEnabled = true;
            }
            else
            {
                _instance.uxCheckOutButton.BackgroundColor = new Color(0x222222);
                _instance.uxCheckOutButton.Clicked -= uxCheckOutButton_Clicked;
                _instance.isClickedEnabled = false;
            }
        }

        private static async void uxCheckOutButton_Clicked(object sender, EventArgs e)
        {
            await _instance.Navigation.PushAsync(new CheckoutPage(RealmManager.All<OrderedList>().First()));
        }

        private async void uxOrderButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new menuPage());
        }

        private async void UxPlayButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new gamepage());
        }

        private async void uxRefillButton_Clicked(object sender, EventArgs e)
        {
            bool response = await NotificationManager.SendNotification("Refill");

            if (!response) await DisplayAlert("Refill Request Failed", "", "Ok");
            else await DisplayAlert("Refill Request Sent", "Server on their way", "Ok");
        }

        private async void uxCallServerButton_Clicked(object sender, EventArgs e)
        {
            bool response = await NotificationManager.SendNotification("Help");

            if (!response) await DisplayAlert("Help Request Failed", "", "Ok");
            else await DisplayAlert("Help Request Sent", "Server on their way", "Ok");
        }
    }
}