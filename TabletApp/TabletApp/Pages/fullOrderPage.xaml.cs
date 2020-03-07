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
        private float price = 0f;

		public fullOrderPage ()
		{
			InitializeComponent ();
		}

        public fullOrderPage(Stack<OrderItem> order)
        {
            InitializeComponent();

            foreach (OrderItem x in order)
            {
                foFullOrderScroll.Children.Add(new Label()
                {
                    Text = (x.itemName + "                            " + x.price),
                    Margin = new Thickness(30f, 10f, 30f, 10f),
                    FontSize = 20f,
                    TextColor = Color.Black
                });

                price += x.price;
            }

            if (order.Count == 0)
            {
                foFullOrderScroll.Children.Add(new Label()
                {
                    Text = "ORDER EMPTY",
                    Margin = new Thickness(30f, 10f, 30f, 10f),
                    FontSize = 20f,
                    TextColor = Color.Black
                });
            }

            foFullOrderTotal.Text = "ORDER TOTAL: $" + price;
        }
	}
}