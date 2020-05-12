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
    public class EditCountryViewModel : INotifyPropertyChanged
    {
        Page page;
        INavigation Navigation;
        DAL.Models.Country country;
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

        string alpha2;
        public string Alpha2
        {
            get { return alpha2; }
            set
            {
                alpha2 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Alpha2"));
                }
            }
        }

        string alpha3;
        public string Alpha3
        {
            get { return alpha3; }
            set
            {
                alpha3 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Alpha3"));
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

        bool isIndependent;
        public bool IsIndependent
        {
            get { return isIndependent; }
            set
            {
                isIndependent = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsIndependent"));
                }
            }
        }

        public ICommand Save { get; set; }
        public ICommand CloseModal { get; set; }
        public EditCountryViewModel(INavigation nav, Page _page, DAL.Models.Country _country)
        {
            Navigation = nav;
            page = _page;
            if (_country == null) _country = new DAL.Models.Country();
            country = _country;
            Code = country.code?.ToString();
            Alpha2 = country.alpha2;
            Alpha3 = country.alpha3;
            Name = country.name;
            IsIndependent = country.is_independent.GetValueOrDefault();
            Save = new Command(SaveCommand);
            CloseModal = new Command(CloseModalCommand);
        }

        async void SaveCommand()
        {
            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Alpha2) &&
                !string.IsNullOrEmpty(Alpha2) && !string.IsNullOrEmpty(Name))
            {
                var code = 0;
                int.TryParse(Code, out code);
                country.code = code;
                country.alpha2 = Alpha2;
                country.alpha3 = Alpha3;
                country.name = Name;
                IsIndependent = country.is_independent.GetValueOrDefault();
                var data = await new DAL.Country().SaveAsync(country);
                if (data != null && data.Error==null)
                {
                    MessagingCenter.Send<EditCountryPage>((EditCountryPage)page, "ChangeCountry");
                    await page.DisplayAlert("Country", "Country registered", "Ok");
                    CloseModalCommand();
                }
                if(data != null && data.Error != null && data.Error.errorMessages?.Count>0)
                {
                    await page.DisplayAlert("Country", data.Error.errorMessages[0], "Ok");
                }
            }
            else
                await page.DisplayAlert("Country", "Some mandatory fields are missing", "Ok");

        }

        async void CloseModalCommand()
        {
            await Navigation.PopPopupAsync();
        }
    }
}
