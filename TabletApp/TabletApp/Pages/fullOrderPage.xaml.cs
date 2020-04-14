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
	public partial class fullOrderPage : ContentPage
	{
        private double priceTotal = 0;

        private OrderList fullOrder;

        public fullOrderPage(OrderList order)
        {
            InitializeComponent();

            fullOrder = new OrderList();

            foSubmitFoodButton.Clicked += foSubmitFoodButton_Clicked;

            for(int i = 0; i < order.orderItems.Count(); i++)
            {
                Models.OrderItem x = order.orderItems[i];

                entreeScroll.Children.Add(new Label()
                {
                    Text = x.name,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    TextColor = Color.Black
                });

                priceScroll.Children.Add(new Label()
                {
                    Text = "$" + x.price,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    TextColor = Color.Black
                });

                priceTotal += x.price;

                fullOrder.orderItems.Add(x);
            }

            foOrderTotal.Text += "$" + priceTotal;
        }

        private async void foSubmitFoodButton_Clicked(object sender, EventArgs e)
        {
            if (fullOrder.orderItems.Count() != 0)
            {
                await UpdateOrderMenuItemsRequest.SendUpdateOrderMenuItemsRequest(RealmManager.All<Table>().First().order_id._id, fullOrder.orderItems);

                RealmManager.RemoveAll<OrderList>();
                RealmManager.AddOrUpdate<OrderList>(new OrderList());

                MainMenu.OnReturn();

                await Navigation.PopAsync();
            }
        }
	}
}