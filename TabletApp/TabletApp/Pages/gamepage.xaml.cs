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
        public gamepage()
        {
            InitializeComponent();
        }
        private async void Game1Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("game 1 info", "Tis no game here", "rip");
        }
        private async void Game2Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("game 2 info", "Tis no game here", "rip");
        }
    }
}