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
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();

            uxPlayButton.Clicked += UxPlayButton_Clicked;
            uxAddButton.Clicked += UxAddButton_Clicked;
        }

        private async void UxAddButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("No Servers", "How'd you even get in here, we're not even open yet", "I broke in");
        }
        private async void UxPlayButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("You're a gamer", "Just kidding, this doesn't do anything yet", "Damn it");
        }
    }
}