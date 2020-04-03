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

		public fullOrderPage ()
		{
			InitializeComponent ();
		}

        public fullOrderPage(OrderList order)
        {
            InitializeComponent();

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
            }

            foOrderTotal.Text += "$" + priceTotal;
        }
	}
}