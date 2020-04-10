using System;
using TabletApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletApp
{
    public partial class App : Application
    {
        public static App _instance;

        public App()
        {
            InitializeComponent();

            _instance = this;

            MainPage = new LoginPage();
        }

        public static void changeMainPage(NavigationPage navPage)
        {
            _instance.MainPage = navPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
