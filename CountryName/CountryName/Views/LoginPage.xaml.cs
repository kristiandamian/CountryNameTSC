using System;
using System.Collections.Generic;
using CountryName.ViewModels;
using Xamarin.Forms;

namespace CountryName.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(this.Navigation, this);
        }
    }
}
