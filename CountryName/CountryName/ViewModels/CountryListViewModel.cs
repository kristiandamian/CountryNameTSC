using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Xamarin.Essentials;
using CountryName.Views;
using Rg.Plugins.Popup.Extensions;
using CountryName.Views.Modals;
using System.Linq;

namespace CountryName.ViewModels
{
    public class CountryListViewModel : INotifyPropertyChanged
    {
        Page page;
        INavigation Navigation;
        public event PropertyChangedEventHandler PropertyChanged;


        bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }


        ObservableRangeCollection<DAL.Models.Country> items;
        public ObservableRangeCollection<DAL.Models.Country> Items
        {
            get { return items; }
            set
            {
                items = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Items"));
                }
            }
        }

        DAL.Models.Country item;
        public DAL.Models.Country Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Item"));
                }
            }
        }
        public ICommand ReloadItems { get; set; }
        public ICommand LoadItems { get; set; }
        public ICommand SearchSubdivision { get; set; }
        public ICommand EndSession { get; set; }
        public ICommand EditCountry { get; set; }
        public ICommand NewCountry { get; set; }
        public ICommand Delete { get; set; }
        public CountryListViewModel(INavigation nav, Page _page)
        {
            Navigation = nav;
            page = _page;

            ReloadItems = new Command(ReloadItemsCommand);
            LoadItems = new Command(LoadItemsCommand);
            SearchSubdivision = new Command<DAL.Models.Country>(SearchSubdivisionCommand);
            EndSession = new Command(EndSessionCommand);
            EditCountry = new Command<DAL.Models.Country>(EditCountryCommand);
            Delete = new Command<DAL.Models.Country>(DeleteCommand);
            NewCountry = new Command(NewCountryCommand);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void SearchSubdivisionCommand(DAL.Models.Country country)
        {
            await Navigation.PushAsync(new SubdivisionListPage(country));
        }
        void NewCountryCommand()
        {
            EditCountryCommand(new DAL.Models.Country());
        }
        async void EditCountryCommand(DAL.Models.Country country)
        {
            MessagingCenter.Subscribe<EditCountryPage, DAL.Models.Country>(this, "ChangeCountry", (sender, arg) =>
            {
                //this.LoadItemsCommand();
                if (arg != null)
                {
                    var pos = Items.IndexOf(Items.Where(x => x.id == arg.id).FirstOrDefault());
                    if (pos >= 0)
                        Items[pos] = arg;
                    else //no existe
                        Items.Add(arg);
                }
            });
            var edit = new Views.Modals.EditCountryPage(country) { CloseWhenBackgroundIsClicked = true };

            await Navigation.PushPopupAsync(edit);
        }
        void ReloadItemsCommand()
        {
            if (Items==null || Items.Count == 0)
                LoadItemsCommand();
        }

        async void LoadItemsCommand()
        {
            IsBusy = true;
            var countries = await new DAL.Country().AllAsync();
            if (countries != null)
            {
                Items = new ObservableRangeCollection<DAL.Models.Country>();
                Items.AddRange(countries);
            }
            IsBusy = false;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await page.DisplayAlert("Country", "The device doesn't have internet connection", "Ok");
                });
            }
            else
            {
                this.LoadItemsCommand();
            }
        }

        void EndSessionCommand()
        {
            new DAL.sqlite.Sesion(Helpers.Config.SqliteBD).EndSession();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async void DeleteCommand(DAL.Models.Country country)
        {
            if (country.id.HasValue)
            {
                var answer = await page.DisplayAlert("Delete", $"Do you wan't to delete the country {country.name}?", "Yes", "No");
                if (answer)
                {
                    var data = await new DAL.Country().DeleteAsync(country.id.Value);
                    if (data != null && data.Error != null && data.Error.errorMessages?.Count > 0)
                    {
                        await page.DisplayAlert("Country", data.Error.errorMessages[0], "Ok");
                    }
                    else
                        items.Remove(country);
                }
            }
        }
    }
}
