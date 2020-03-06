using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menuItemPage : ContentPage
	{
        private OrderItem newItem;

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

            newItem = new OrderItem();

            newItem.itemType = OrderItem.orderType.ENTREE;
            newItem.itemName = "STEAK";
            newItem.price = 14.99f;
            newItem.specialInstructions = "well done";
        }

        private async void miAddToOrder_Clicked(object sender, EventArgs e)
        {
            menuPage.fullOrder.Push(newItem);

            await DisplayAlert("Add Item to Order", "Item Sent", "Confirm");
            await Navigation.PopAsync();
        }
    }
}