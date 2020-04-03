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
        private Models.MenuItem menuItem;

        public menuItemPage(Models.MenuItem item)
		{
            InitializeComponent();

            menuItem = item;

            miAddToOrder.Clicked += miAddToOrder_Clicked;

            miItemTitle.Text = menuItem.name;
            miItemDescription.Text = menuItem.desctription;
        }

        private async void miAddToOrder_Clicked(object sender, EventArgs e)
        {
            UpdateOrder();
            await DisplayAlert("Add Item to Order", "Item Sent", "Confirm");
            await Navigation.PopAsync();
        }

        private void UpdateOrder()
        {
            OrderList order = new OrderList();
            
            for (int i = 0; i < RealmManager.All<OrderList>().First().menuItems.Count(); i++)
            {
                order.menuItems.Add(RealmManager.All<OrderList>().First().menuItems[i]);
            }

            order.menuItems.Add(menuItem);

            RealmManager.RemoveAll<OrderList>();
            RealmManager.AddOrUpdate<OrderList>(order);
        }
    }
}