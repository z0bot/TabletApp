using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckoutPage : ContentPage
	{
        private double totalPrice = 0;

        private double tip = 0;

        private Order order;

        private OrderList checkOutOrder = new OrderList();

		public CheckoutPage()
		{
			InitializeComponent();

            updateItems();

            cpCheckoutButton.Clicked += cpCheckoutButton_Clicked;
        }

        private async void cpCheckoutButton_Clicked(object sender, EventArgs e)
        {
            await UpdateOrderMenuItemsRequest.SendUpdateOrderMenuItemsRequest(order._id, order.menuItems);

            tip = (cpTip.Text == "") ? 0f : Double.Parse(cpTip.Text);

            updateItems();

            MainMenu.OnReturn();

            await PostTipRequest.SendPostTipRequest(order.employee_id, tip);

            await Navigation.PushAsync(new PaymentPage(totalPrice, tip));
        }

        private void updateItems()
        {
            if (cpScrollGridLabel.Children.Count() > 0) cpScrollGridLabel.Children.Clear();
            if (cpScrollGridSwitch.Children.Count() > 0) cpScrollGridSwitch.Children.Clear();

            Order oldOrder = RealmManager.All<Table>().First().order_id;

            order = new Order();

            order._id = oldOrder._id;
            order.employee_id = oldOrder.employee_id;
            order.send_to_kitchen = oldOrder.send_to_kitchen;

            for (int i = 0; i < oldOrder.menuItems.Count(); i++)
            {
                order.menuItems.Add(new OrderItem(oldOrder.menuItems[i]));
            }

            for (int i = 0; i < order.menuItems.Count(); i++)
            {
                OrderItem x = order.menuItems[i];

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

                            x.paid = true;
                        }
                        else
                        {
                            checkOutOrder.orderItems.Remove(x);

                            totalPrice -= price;

                            x.paid = false;
                        }

                        cpFullPrice.Text = "Total: $" + totalPrice.ToString();
                    };

                    cpScrollGridSwitch.Children.Add(newSwitch);
                }
            }
        }
    }
}