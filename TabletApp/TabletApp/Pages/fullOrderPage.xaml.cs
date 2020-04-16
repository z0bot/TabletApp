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
                OrderItem x = order.orderItems[i];

                Grid newGrid = new Grid();
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                newGrid.Children.Add(new Label()
                {
                    Text = x.name,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    TextColor = Color.Black
                }, 0, 0);

                newGrid.Children.Add(new Label()
                {
                    Text = "$" + x.price,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    TextColor = Color.Black
                }, 1, 0);

                Button deleteButton = new Button()
                {
                    Text = "Remove",
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Xamarin.Forms.Color.Red,
                    Margin = new Thickness(50, 0, 50, 0),
                    Padding = new Thickness(0, 15, 0, 15),
                    CornerRadius = 15
                };

                deleteButton.Clicked += (sender, e) =>
                {
                    foScroll.Children.Remove(newGrid);
                    fullOrder.orderItems.Remove(x);
                    RealmManager.RemoveAll<OrderList>();
                    RealmManager.AddOrUpdate<OrderList>(new OrderList(fullOrder));
                };

                newGrid.Children.Add(deleteButton, 2, 0);

                foScroll.Children.Add(newGrid);

                priceTotal += x.price;

                fullOrder.orderItems.Add(x);
            }

            foOrderTotal.Text += "$" + priceTotal;
        }

        private async void foSubmitFoodButton_Clicked(object sender, EventArgs e)
        {
            if (fullOrder.orderItems.Count() != 0)
            {
                for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
                    fullOrder.orderItems.Add(RealmManager.All<Table>().First().order_id.menuItems[i]);

                await UpdateOrderMenuItemsRequest.SendUpdateOrderMenuItemsRequest(RealmManager.All<Table>().First().order_id._id, fullOrder.orderItems);

                RealmManager.RemoveAll<OrderList>();
                RealmManager.AddOrUpdate<OrderList>(new OrderList());

                MainMenu.OnReturn();

                await Navigation.PopAsync();
            }
        }
	}
}