using System;
using CountryName.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountryName
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (new DAL.sqlite.Sesion(Helpers.Config.SqliteBD).Current() != null)
                MainPage = new NavigationPage(new CountryListPage());
            else
                MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
