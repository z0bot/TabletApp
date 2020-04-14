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
		public menuPage ()
		{
			InitializeComponent ();

            SetupMenuItems();

            if (RealmManager.All<OrderList>().Count() == 0) RealmManager.AddOrUpdate<OrderList>(new OrderList());

            mpViewOrderButton.Clicked += mpViewOrderButton_Clicked;

            mpRefillButton.Clicked += mpRefillButton_Clicked;
            mpCallServerButton.Clicked += mpCallServerButton_Clicked;
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
                    IList<Ingredient>  ingredients = ingredientList.doc.Where((Ingredient ing) => (ing._id == currItem.ingredients[j]._id)).ToList<Ingredient>();

                    if (ingredients.Count() == 0)
                    {
                        available = false;
                        break;
                    }
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

                newButton.Clicked += async (sender, args) =>
                {
                    menuItemPage newPage = new menuItemPage(currItem);
                    newPage.BackgroundImageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(Convert.FromBase64String((currItem.picture.Split(',')[1]))));

                    await Navigation.PushAsync(newPage);
                };

                if (currItem.category == "Entree") entreeScroll.Children.Add(newButton);
                else if (currItem.category == "Appetizer") appScroll.Children.Add(newButton);
                else if (currItem.category == "Drink") drinkScroll.Children.Add(newButton);
                else if (currItem.category == "Side") sideScroll.Children.Add(newButton);
                else if (currItem.category == "Dessert") sideScroll.Children.Add(newButton);
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