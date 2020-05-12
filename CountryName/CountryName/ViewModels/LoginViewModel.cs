using System;
using System.ComponentModel;
using System.Windows.Input;
using CountryName.Views;
using Xamarin.Forms;

namespace CountryName.ViewModels
{
    public class LoginViewModel  :  INotifyPropertyChanged
    {
        Page page;
        INavigation Navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("User"));
                }
            }
        }
        string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }

        public ICommand Login { get; set; }

        public LoginViewModel(INavigation nav, Page _page)
        {
            Navigation = nav;
            page = _page;
            Login = new Command(InicioSesionCommand);
        }

        async void InicioSesionCommand()
        {
            if(new DAL.Login().MakeLogin(User, Password, Helpers.Config.SqliteBD))
            {// go to main
                Application.Current.MainPage = new NavigationPage(new CountryListPage());
            }
            else
                await page.DisplayAlert("Country Name", "Wrong user name or password", "Ok");
        }
    }
}
