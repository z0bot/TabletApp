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
	public partial class menuItemPage : ContentPage
	{
        public menuItemPage()
        {
            InitializeComponent();

            initialize();
        }

        public menuItemPage (string itemTitle, string itemDescription)
		{
            InitializeComponent();

            miItemTitle.Text = itemTitle;
            miItemDescription.Text = itemDescription;

            initialize();
		}

        private void initialize()
        {
            miAddToOrder.Clicked += miAddToOrder_Clicked;
        }

        private async void miAddToOrder_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Add Item to Order", "Item Sent", "Confirm");
            await Navigation.PopAsync();
        }
    }
}