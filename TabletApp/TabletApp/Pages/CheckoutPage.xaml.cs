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
	public partial class CheckoutPage : ContentPage
	{
        private float totalPrice = 0;

        private OrderedList checkOutOrder = new OrderedList();

		public CheckoutPage(OrderedList order)
		{
			InitializeComponent();

            for (int i = 0; i < order.menuItems.Count(); i++)
            {
                Models.MenuItem x = order.menuItems[i];

                cpScrollGridLabel.Children.Add(new Label()
                {
                    Text = x.name + " --- " + x.price,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    TextColor = Color.Black,
                });

                Switch newSwitch = new Switch() { };

                newSwitch.Toggled += (sender, e) =>
                {
                    float price = x.price;

                    if (newSwitch.IsToggled)
                    {
                        checkOutOrder.menuItems.Add(x);

                        totalPrice += price;
                    }
                    else
                    {
                        checkOutOrder.menuItems.Remove(x);

                        totalPrice -= price;
                    }

                    updateTotalPrice();
                };

                cpScrollGridSwitch.Children.Add(newSwitch);
            }

            cpCheckoutButton.Clicked += cpCheckoutButton_Clicked;
        }

        private async void cpCheckoutButton_Clicked(object sender, EventArgs e)
        {

        }

        private void updateTotalPrice()
        {
            cpFullPrice.Text = "Total: $" + totalPrice.ToString();
        }
    }
}