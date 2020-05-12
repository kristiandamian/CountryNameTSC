using System;
using System.Collections.Generic;
using CountryName.ViewModels;
using Xamarin.Forms;

namespace CountryName.Views
{
    public partial class SubdivisionListPage : ContentPage
    {
        public SubdivisionListPage(DAL.Models.Country _country)
        {
            InitializeComponent();
            BindingContext = new SubdivisionListViewModel(this.Navigation, this, _country);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (SubdivisionListViewModel)this.BindingContext;
            if (viewModel.LoadItems.CanExecute(null))
                viewModel.LoadItems.Execute(null);
        }
    }
}
