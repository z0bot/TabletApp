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
	public partial class fullOrderPage : ContentPage
	{
        private float priceTotal = 0;

        private OrderList fullOrder;

        public fullOrderPage(OrderList order)
        {
            InitializeComponent();

            fullOrder = new OrderList();

            foSubmitFoodButton.Clicked += foSubmitFoodButton_Clicked;

            for(int i = 0; i < order.menuItems.Count(); i++)
            {
                Models.MenuItem x = order.menuItems[i];

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

                fullOrder.menuItems.Add(x);
            }

            foOrderTotal.Text += "$" + priceTotal;
        }

        private async void foSubmitFoodButton_Clicked(object sender, EventArgs e)
        {
            OrderedList tempList = new OrderedList();

            if (RealmManager.All<OrderedList>().Count() > 0)
                for (int i = 0; i < RealmManager.All<OrderedList>().First().menuItems.Count(); i++)
                    tempList.menuItems.Add(RealmManager.All<OrderedList>().First().menuItems[i]);

            for (int i = 0; i < fullOrder.menuItems.Count(); i++)
                tempList.menuItems.Add(fullOrder.menuItems[i]);

            RealmManager.RemoveAll<OrderedList>();
            RealmManager.RemoveAll<OrderList>();

            RealmManager.AddOrUpdate<OrderedList>(tempList);

            MainMenu.OnReturn();

            await Navigation.PopAsync();
        }
	}
}