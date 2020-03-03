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
                    Text = (x.itemName + "                            " + x.price)
                });
            }
        }
	}
}