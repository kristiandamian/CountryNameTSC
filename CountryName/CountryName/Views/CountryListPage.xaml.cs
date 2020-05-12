using System;
using System.Collections.Generic;
using CountryName.ViewModels;
using Xamarin.Forms;

namespace CountryName.Views
{
    public partial class CountryListPage : ContentPage
    {
        public CountryListPage()
        {
            InitializeComponent();
            this.BindingContext = new CountryListViewModel(this.Navigation, this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (CountryListViewModel)this.BindingContext;
            if (viewModel.ReloadItems.CanExecute(null))
                viewModel.ReloadItems.Execute(null);
        }
    }
}
