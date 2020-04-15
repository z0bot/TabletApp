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
    public partial class gamepage : ContentPage
    {
        private string result="";

        public gamepage()
        {
            InitializeComponent();
            RealmManager.RemoveAll<Models.KidsLoc>();
            enterpin();
        }

        public gamepage(int temp)
        {
            InitializeComponent();
        }

        private async void Game1Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoryGame());
        }

        private async void enterpin()
        {
            while (result == "")
            {
                result = await DisplayPromptAsync("Pin", "Enter A Pin", "accept", "", initialValue: "", maxLength: 4, keyboard: Keyboard.Numeric);
            }
            Models.KidsLoc kt = new Models.KidsLoc();
            kt.Id = result;
            kt.Master = "9999";
            RealmManager.AddOrUpdate<Models.KidsLoc>(kt);
        }

        private async void Game2Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TickTack());
        }

        private async void HomeClicked(object sender, EventArgs e)
        {
            Models.KidsLoc kt = RealmManager.All<Models.KidsLoc>().First();
            result = await DisplayPromptAsync("Pin", "Enter A Pin", initialValue: "", maxLength: 4, keyboard: Keyboard.Numeric);

            if (result == kt.Id || result == kt.Master) await Navigation.PopAsync();
            else await DisplayAlert("Incorrect Pin", "Please Try Again", "ok");
        }
    }
}