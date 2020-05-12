using System;
using System.Collections.Generic;
using CountryName.ViewModels;
using Xamarin.Forms;

namespace CountryName.Views.Modals
{
    public partial class EditCountryPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public EditCountryPage(DAL.Models.Country _country)
        {
            InitializeComponent();
            BindingContext = new EditCountryViewModel(this.Navigation,this,_country);
        }
    }
}
