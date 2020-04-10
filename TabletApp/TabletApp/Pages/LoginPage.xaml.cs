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
            RealmManager.RemoveAll<OrderedList>();
            RealmManager.RemoveAll<EmployeeList>();
            RealmManager.RemoveAll<MenuList>();
            RealmManager.RemoveAll<TableManager>();

            lpTableButton.Clicked += lpTableButton_Clicked;
		}

        private async void lpTableButton_Clicked(object sender, EventArgs e)
        {
            await GetEmployeesRequest.SendGetEmployeesRequest();

            LoadTableData();
            App.changeMainPage(new NavigationPage(new MainMenu()));
        }

        private void LoadTableData()
        {
            Employee employee = null;

            for (int i = 0; i < RealmManager.All<EmployeeList>().First().employees.Count(); i++)
            {
                Employee x = RealmManager.All<EmployeeList>().First().employees[i];
                bool boolBreak = false;

                for (int j = 0; j < x.tables.Count(); j++)
                {
                    if (x.tables[j] == lpTableEntry.Text)
                    {
                        employee = x;

                        boolBreak = true;
                        break;
                    }
                }

                if (boolBreak) break;
            }

            if (employee == null)
            {
                employee = new Employee();
                employee._id = "5e8e6d4b9696520004639e73";
            }

            RealmManager.AddOrUpdate<TableManager>(new TableManager()
            {
                table = (lpTableEntry.Text == "") ? "DEFAULT" : lpTableEntry.Text,
                employee_id = employee._id
            });
        }
	}
}