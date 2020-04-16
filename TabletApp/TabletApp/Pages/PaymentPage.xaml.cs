using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp;
using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentPage : ContentPage
	{
        double total;

        Regex cardNum = new Regex("^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");
        Regex cardSec = new Regex("^[0-9][0-9][0-9]$");
        Regex cardDate = new Regex("^[0-1][1-2]/[0-3][0-9]/[0-9][0-9][0-9][0-9]$");
        Regex cardHolder = new Regex("^[a-zA-Z\\s]+$");

        Match matchNum;
        Match matchSec;
        Match matchDate;
        Match matchHolder;

		public PaymentPage (double contribution, double tip)
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            total = contribution + tip;

            ppTotalCheck.Text = "Total: " + total.ToString("C");
        }

        async void confirmButton(object sender, EventArgs e)
        {
            matchNum = cardNum.Match(ppCardNumEntry.Text);
            matchSec = cardSec.Match(ppCardSecEntry.Text);
            matchDate = cardDate.Match(ppCardDateEntry.Text);
            matchHolder = cardHolder.Match(ppCardNumHolderEntry.Text);

            if (matchNum.Success && matchSec.Success && matchDate.Success && matchHolder.Success)
            {
                await DisplayAlert("Payment Successful", "", "OK");
                await LeavePage();
            }
            else
            {
                await DisplayAlert("Payment Failed", "WRONGLY FORMATTED:\n" +
                                                     ((!matchNum.Success) ? "Card Number\n" : "") +
                                                     ((!matchSec.Success) ? "Security Code\n" : "") +
                                                     ((!matchDate.Success) ? "Date\n" : "") +
                                                     ((!matchHolder.Success) ? "Card Holder" : ""), "OK");
            }
        }

        async void payWithCashClicked(object sender, EventArgs e)
        {
            // Send notification to waitstaff

            bool response = await NotificationManager.SendNotification("Cash Payment");

            if (!response) await DisplayAlert("Payment Request Failed", "", "Ok");
            else await DisplayAlert("Cash Payment", "Your server is on their way to collect your cash payment", "OK");

            await LeavePage();
        }

        async Task LeavePage()
        {
            // Remove previous page to prevent double payment
            if (Navigation.NavigationStack.Count() > 1)
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count() - 2]);

            // Offer game
            if (await DisplayAlert("Game Opportunity", "Would you like to play a game for a chance at a free dessert?", "Yes, please!", "No thanks"))
            {
                Navigation.InsertPageBefore(new RandomGamePage(), this);
            }

            bool orderComplete = true;

            for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
            {
                if (!RealmManager.All<Table>().First().order_id.menuItems[i].paid)
                {
                    orderComplete = false;
                    break;
                }
            }

            if (orderComplete) await FinishOrderRequest.SendFinishOrderRequest(RealmManager.All<Table>().First()._id);

            MainMenu.OnReturn();
            
            await Navigation.PopAsync();
        }

        // Prevent going back to previous pages, as the order has already been sent. Must continue and pay
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}