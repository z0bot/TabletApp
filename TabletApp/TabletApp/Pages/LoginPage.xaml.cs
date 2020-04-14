using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public LoginPage ()
		{
			InitializeComponent ();

            RealmManager.RemoveAll<OrderList>();
            RealmManager.RemoveAll<EmployeeList>();
            RealmManager.RemoveAll<IngredientList>();
            RealmManager.RemoveAll<MenuList>();

            lpTableButton.Clicked += lpTableButton_Clicked;
		}

        private async void lpTableButton_Clicked(object sender, EventArgs e)
        {
            await GetTableRequest.SendGetTableRequest(Int32.Parse(lpTableEntry.Text));

            App.changeMainPage(new NavigationPage(new MainMenu()));
        }
	}
}