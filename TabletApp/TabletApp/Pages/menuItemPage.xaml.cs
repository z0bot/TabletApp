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

            initialize("DEFAULT", 0f);
        }

        public menuItemPage (string itemTitle, string itemDescription, float price)
		{
            InitializeComponent();

            miItemTitle.Text = itemTitle;
            miItemDescription.Text = itemDescription;

            initialize(itemTitle, price);
		}

        private void initialize(string itemTitle, float price)
        {
            miAddToOrder.Clicked += miAddToOrder_Clicked;

            newItem = new OrderItem();

            newItem.itemType = OrderItem.orderType.ENTREE;
            newItem.itemName = itemTitle;
            newItem.price = price;
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