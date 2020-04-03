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
    public partial class Uwin : ContentPage
    {
        public Uwin()
        {
            InitializeComponent();
        }
        private async void ReturnClicked(object sender, EventArgs e) {
            int temp = 1;
            await Navigation.PushAsync(new gamepage(temp));
        }
    }
}