using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

using Realms;

namespace TabletApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public static MainMenu _instance;

        private Button checkOutButton;

        public MainMenu()
        {
            InitializeComponent();

            _instance = this;

            OnReturn();

            uxOrderButton.Clicked += uxOrderButton_Clicked;
            uxPlayButton.Clicked += UxPlayButton_Clicked;
            uxCallServerButton.Clicked += uxCallServerButton_Clicked;
            uxRefillButton.Clicked += uxRefillButton_Clicked;
        }

        public static async void OnReturn()
        {
            bool unpaid = false;

            await GetTableRequest.SendGetTableRequest(RealmManager.All<Table>().First().table_number);

            for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
            {
                if (!RealmManager.All<Table>().First().order_id.menuItems[i].paid)
                {
                    unpaid = true;
                    break;
                }
            }

            if (unpaid)
            {
                if (_instance.checkOutButton == null)
                {
                    _instance.checkOutButton = new Button()
                    {
                        Margin = new Thickness(100, 0, 100, 0),
                        Padding = new Thickness(0, 15, 0, 15),
                        Text = "Pay Ticket",
                        FontSize = 35,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Xamarin.Forms.Color.White,
                        CornerRadius = 15,
                        BackgroundColor = Xamarin.Forms.Color.FromHex("#24BF87")
                    };

                    _instance.checkOutButton.Clicked += uxCheckOutButton_Clicked;

                    _instance.uxTitleButtonStack.Children.Add(_instance.checkOutButton);
                }
            }
            else
            {
                if (_instance.checkOutButton != null) _instance.uxTitleButtonStack.Children.Remove(_instance.checkOutButton);

                _instance.checkOutButton = null;
            }
        }

        private static async void uxCheckOutButton_Clicked(object sender, EventArgs e)
        {
            await _instance.Navigation.PushAsync(new CheckoutPage());
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