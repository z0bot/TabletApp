using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletApp;
using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class menuPage : ContentPage
	{
        public static menuPage _instance;
        private Button checkOutButton;
        public menuPage ()
		{
			InitializeComponent ();

            SetupMenuItems();

            _instance = this;

            OnReturn();

            if (RealmManager.All<OrderList>().Count() == 0) RealmManager.AddOrUpdate<OrderList>(new OrderList());

            mpViewOrderButton.Clicked += mpViewOrderButton_Clicked;

            mpRefillButton.Clicked += mpRefillButton_Clicked;
            mpCallServerButton.Clicked += mpCallServerButton_Clicked;
        }

        public static async void OnReturn()
        {
            bool unpaid = false;

            await GetTableRequest.SendGetTableRequest(RealmManager.All<Table>().First().table_number);

            for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
            {
                if (!RealmManager.All<Table>().First().order_id.menuItems[i].paid)
                {
                    unpaid = true;
                    break;
                }
            }

            if (unpaid)
            {
                if (_instance.checkOutButton == null)
                {
                    _instance.checkOutButton = new Button()
                    {
                        Margin = new Thickness(100, 0, 100, 0),
                        Padding = new Thickness(0, 15, 0, 15),
                        Text = "Pay Ticket",
                        
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Xamarin.Forms.Color.White,
                        CornerRadius = 15,
                        BackgroundColor = Xamarin.Forms.Color.FromHex("#24BF87")
                    };

                    _instance.checkOutButton.Clicked += uxCheckOutButton_Clicked;

                    _instance.mpOrderConfirmation.Children.Add(_instance.checkOutButton);
                }
            }
            else
            {
                if (_instance.checkOutButton != null) _instance.mpOrderConfirmation.Children.Remove(_instance.checkOutButton);

                _instance.checkOutButton = null;
            }
        }

        private static async void uxCheckOutButton_Clicked(object sender, EventArgs e)
        {
            await GetTableRequest.SendGetTableRequest(RealmManager.All<Table>().First().table_number);
            await _instance.Navigation.PushAsync(new CheckoutPage());
        }

        async void SetupMenuItems()
        {
            await GetMenuItemsRequest.SendGetMenuItemsRequest();
            await GetIngredientsRequest.SendGetIngredientsRequest();

            IngredientList ingredientList = new IngredientList();

            for(int i = 0; i < RealmManager.All<IngredientList>().First().doc.Count(); i++)
            {
                ingredientList.doc.Add(RealmManager.All<IngredientList>().First().doc[i]);
            }

            for (int i = 0; i < RealmManager.All<MenuList>().First().menuItems.Count(); i++)
            {
                Models.MenuItem currItem = RealmManager.All<MenuList>().First().menuItems[i];

                bool available = true;

                for(int j = 0; j < currItem.ingredients.Count(); j++)
                {
                    IList<Ingredient>ingredients = ingredientList.doc.Where((Ingredient ing) => (ing._id == currItem.ingredients[j]._id)).ToList<Ingredient>();

                    for (int k = 0; k < ingredients.Count(); k++)
                    {
                        if (ingredients[k].quantity == 0)
                        {
                            available = false;
                            break;
                        }
                    }

                    if (!available) break;
                }

                if (!available) continue;

                Button newButton = new Button()
                {
                    Text = currItem.name,
                    Margin = new Thickness(30, 0, 30, 15),
                    FontSize = 20,
                    WidthRequest = 100,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Xamarin.Forms.Color.White,
                    CornerRadius = 15,
                    BackgroundColor = Xamarin.Forms.Color.FromHex("#24BF87"),
                };

                newButton.Clicked += async (sender, args) => await Navigation.PushAsync(new menuItemPage(currItem));

                if (currItem.category == "Entrees") entreeScroll.Children.Add(newButton);
                else if (currItem.category == "Appetizers") appScroll.Children.Add(newButton);
                else if (currItem.category == "Kids Meals") kidsScroll.Children.Add(newButton);
                else if (currItem.category == "Drinks") drinkScroll.Children.Add(newButton);
                else if (currItem.category == "Sides") sideScroll.Children.Add(newButton);
                else if (currItem.category == "Desserts") dessertScroll.Children.Add(newButton);
                else otherScroll.Children.Add(newButton);
            }
        }

        private async void mpViewOrderButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new fullOrderPage(RealmManager.All<OrderList>().First()));
        }

        private async void mpRefillButton_Clicked(object sender, EventArgs e)
        {
            bool response = await NotificationManager.SendNotification("Refill");

            if (!response) await DisplayAlert("Refill Request Failed", "", "Ok");
            else await DisplayAlert("Refill Request Sent", "Server on their way", "Ok");
        }

        private async void mpCallServerButton_Clicked(object sender, EventArgs e)
        {
            bool response = await NotificationManager.SendNotification("Help");

            if (!response) await DisplayAlert("Help Request Failed", "", "Ok");
            else await DisplayAlert("Help Request Sent", "Server on their way", "Ok");
        }
    }
}