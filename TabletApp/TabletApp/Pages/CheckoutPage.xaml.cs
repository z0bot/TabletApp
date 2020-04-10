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

                cpScrollGridSwitch.Children.Add(new Switch()
                {
                    
                });
            }
        }
	}
}