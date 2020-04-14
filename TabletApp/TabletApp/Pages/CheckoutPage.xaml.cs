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
        private double totalPrice = 0;

        private OrderList checkOutOrder = new OrderList();

		public CheckoutPage()
		{
			InitializeComponent();

            for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
            {
                Models.OrderItem x = RealmManager.All<Table>().First().order_id.menuItems[i];

                if (!x.paid)
                {
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
                        double price = x.price;

                        if (newSwitch.IsToggled)
                        {
                            checkOutOrder.orderItems.Add(x);

                            totalPrice += price;
                        }
                        else
                        {
                            checkOutOrder.orderItems.Remove(x);

                            totalPrice -= price;
                        }

                        updateTotalPrice();
                    };

                    cpScrollGridSwitch.Children.Add(newSwitch);
                }
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