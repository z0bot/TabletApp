using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

using Realms;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        Regex tableLogin = new Regex("^([0-9]|[1-2][0-9])$");

        Match loginMatch;

		public LoginPage ()
		{
			InitializeComponent ();

            RealmManager.RemoveAll<Table>();
            RealmManager.RemoveAll<OrderList>();
            RealmManager.RemoveAll<EmployeeList>();
            RealmManager.RemoveAll<IngredientList>();
            RealmManager.RemoveAll<MenuList>();

            lpTableButton.Clicked += lpTableButton_Clicked;
		}

        private async void lpTableButton_Clicked(object sender, EventArgs e)
        {
            loginMatch = tableLogin.Match(lpTableEntry.Text);

            if (loginMatch.Success)
            {
                await GetTableRequest.SendGetTableRequest(Int32.Parse(lpTableEntry.Text));
                App.changeMainPage(new NavigationPage(new MainMenu()));
            }
            else
            {
                await DisplayAlert("Login FAILED", "", "Try Again");
            }
        }
	}
}