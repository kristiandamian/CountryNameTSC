using System;
using System.Collections.Generic;
using CountryName.ViewModels;
using Xamarin.Forms;

namespace CountryName.Views.Modals
{
    public partial class EditSubdivisionPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public EditSubdivisionPage(DAL.Models.Country country, DAL.Models.Subdivision subdivision)
        {
            InitializeComponent();
            BindingContext = new EditSubdivisionViewModel(this.Navigation, this, country, subdivision);
        }
    }
}
