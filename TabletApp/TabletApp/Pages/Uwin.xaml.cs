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
            await Navigation.PushAsync(new gamepage());
        }
    }
}