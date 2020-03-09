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
    public class menuItem
    {
        public int ID;

        public Button buttonInfo { get; set; }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menuPage : ContentPage
	{
        public static Stack<Models.OrderItem> fullOrder;

        public Stack<menuItem> entrees;
        public Stack<menuItem> drinks;
        public Stack<menuItem> sides;

		public menuPage ()
		{
			InitializeComponent ();

            fullOrder = new Stack<OrderItem>();

            entrees = new Stack<menuItem>();
            drinks = new Stack<menuItem>();
            sides = new Stack<menuItem>();

            int entreeAmt = 4;
            int drinkAmt = 3;
            int sideAmt = 3;

            for (int i = 0; i < entreeAmt; i++)
            {
                menuItem newItem = new menuItem();

                mpEntreesScroll.Children.Add(newItem.buttonInfo = (new Button()
                {
                    Text = "NEW ENTREE",
                    Margin = new Thickness(30f, 0f, 30f, 15f),
                    FontSize = 15f,
                    FontAttributes = FontAttributes.None,
                    WidthRequest = 100f,
                    TextColor = Color.Black,
                }));

                newItem.ID = i;

                newItem.buttonInfo.Clicked += async (sender, args) => await Navigation.PushAsync(new menuItemPage("Entree", "Good ass Steak", 14.99f) { BackgroundImageSource = "steakOne" });

                entrees.Push(newItem);
            }

            for (int i = 0; i < drinkAmt; i++)
            {
                menuItem newItem = new menuItem();

                mpDrinkSideScroll.Children.Add(newItem.buttonInfo = (new Button()
                {
                    Text = "NEW DRINK",
                    Margin = new Thickness(30f, 0f, 30f, 15f),
                    FontSize = 15f,
                    FontAttributes = FontAttributes.None,
                    WidthRequest = 100f,
                    TextColor = Color.Black,
                }));

                newItem.ID = i;

                newItem.buttonInfo.Clicked += async (sender, args) => await Navigation.PushAsync(new menuItemPage("Drink", "Smooth ass Drinks", 3.99f) { BackgroundImageSource = "drinksOne" });

                drinks.Push(newItem);
            }

            mpDrinkSideScroll.Children.Add(new Label()
            {
                Text = "Sides",
                Margin = new Thickness(30f, 30f, 30f, 30f),
                FontSize = 20f,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
            });

            for (int i = 0; i < sideAmt; i++)
            {
                menuItem newItem = new menuItem();

                mpDrinkSideScroll.Children.Add(newItem.buttonInfo = (new Button()
                {
                    Text = "NEW SIDE",
                    Margin = new Thickness(30f, 0f, 30f, 15f),
                    FontSize = 15f,
                    FontAttributes = FontAttributes.None,
                    WidthRequest = 100f,
                    TextColor = Color.Black,
                }));

                newItem.ID = i;

                newItem.buttonInfo.Clicked += async (sender, args) => await Navigation.PushAsync(new menuItemPage("Side", "Crunchy ass Sides", 2.99f) { BackgroundImageSource = "sidesOne" });

                sides.Push(newItem);
            }

            mpRefillButton.Clicked += mpRefillButton_Clicked;
            mpCallServerButton.Clicked += mpCallServerButton_Clicked;
            mpViewOrderButton.Clicked += mpViewOrderButton_Clicked;
        }

        //Bottom Buttons
        private async void mpRefillButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Refill Request Sent", "Your Server is on their way", "Thristy Aint'cha");
        }

        private async void mpCallServerButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Server Summoned", "Pray to your god while you can", "Run");
        }

        //View Order
        private async void mpViewOrderButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new fullOrderPage(fullOrder));
        }
    }
}