using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Xamarin.Essentials;
using CountryName.Views;
using Rg.Plugins.Popup.Extensions;
using CountryName.Views.Modals;

namespace CountryName.ViewModels
{
    public class EditSubdivisionViewModel : INotifyPropertyChanged
    {
        Page page;
        INavigation Navigation;
        DAL.Models.Country country;
        DAL.Models.Subdivision subdivision;

        public event PropertyChangedEventHandler PropertyChanged;

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Code"));
                }
            }
        }

        public ICommand Save { get; set; }
        public ICommand CloseModal { get; set; }
        public EditSubdivisionViewModel(INavigation nav, Page _page, DAL.Models.Country _country, DAL.Models.Subdivision _sub)
        {
            Navigation = nav;
            page = _page;
            if (_country == null) _country = new DAL.Models.Country();
            if (_sub == null) _sub = new DAL.Models.Subdivision();
            country = _country;
            subdivision = _sub;
            Code = subdivision.code;
            Name = subdivision.name;
            Save = new Command(SaveCommand);
            CloseModal = new Command(CloseModalCommand);
        }

        async void SaveCommand()
        {
            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Name))
            {
                subdivision.code = Code;
                subdivision.name = Name;
                var data = await new DAL.Subdivision().SaveAsync(country.id.Value, subdivision);
                if (data != null && data.Error == null)
                {
                    MessagingCenter.Send<EditSubdivisionPage,DAL.Models.Subdivision>((EditSubdivisionPage)page, "ChangeSubdivision", data);
                    await page.DisplayAlert("Subdivision", "Subdivision registered", "Ok");
                    CloseModalCommand();
                }
                if (data != null && data.Error != null && data.Error.errorMessages?.Count > 0)
                {
                    await page.DisplayAlert("Subdivision", data.Error.errorMessages[0], "Ok");
                }
            }
            else
                await page.DisplayAlert("Subdivision", "Some mandatory fields are missing", "Ok");

        }

        async void CloseModalCommand()
        {
            await Navigation.PopPopupAsync();
        }
    }
}
