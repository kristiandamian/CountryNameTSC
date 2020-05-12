using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using Xamarin.Essentials;
using CountryName.Views;
using CountryName.Views.Modals;
using Rg.Plugins.Popup.Extensions;

namespace CountryName.ViewModels
{
    public class SubdivisionListViewModel : INotifyPropertyChanged
    {
        Page page;
        INavigation Navigation;
        DAL.Models.Country country;

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
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        ObservableRangeCollection<DAL.Models.Subdivision> items;
        public ObservableRangeCollection<DAL.Models.Subdivision> Items
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

        public ICommand LoadItems { get; set; }
        public ICommand EditSubdivision { get; set; }
        public ICommand NewSubdivision { get; set; }
        public ICommand Delete { get; set; }

        public SubdivisionListViewModel(INavigation nav, Page _page, DAL.Models.Country _country)
        {
            Navigation = nav;
            page = _page;
            country = _country;
            LoadItems = new Command(LoadItemsCommand);
            EditSubdivision = new Command<DAL.Models.Subdivision>(EditSubdivisionCommand);
            Delete = new Command<DAL.Models.Subdivision>(DeleteCommand);
            NewSubdivision = new Command(NewSubdivisionCommand);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            this.Title = $"{country.name} ({country.alpha2})";
        }

        async void LoadItemsCommand()
        {
            IsBusy = true;
            if (country != null && country.id.HasValue)
            {
                var subs = await new DAL.Subdivision().AllFromCountryAsync(country.id.Value);
                if (subs != null && subs.Count>0)
                {
                    Items = new ObservableRangeCollection<DAL.Models.Subdivision>();
                    Items.AddRange(subs);
                }
            }
            IsBusy = false;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await page.DisplayAlert("Country Name", "The device doesn't have internet connection", "Ok");
                });
            }
            else
            {
                this.LoadItemsCommand();
            }
        }

        void NewSubdivisionCommand()
        {
            EditSubdivisionCommand(new DAL.Models.Subdivision());
        }
        async void EditSubdivisionCommand(DAL.Models.Subdivision subdivision)
        {
            MessagingCenter.Subscribe<EditSubdivisionPage>(this, "ChangeSubdivision", (sender) =>
            {
                this.LoadItemsCommand();
            });
            var edit = new EditSubdivisionPage(country,subdivision) { CloseWhenBackgroundIsClicked = true };

            await Navigation.PushPopupAsync(edit);
        }

        async void DeleteCommand(DAL.Models.Subdivision subdivision)
        {
            if (subdivision.id.HasValue)
            {
                var answer = await page.DisplayAlert("Delete", $"Do you wan't to delete the subdivision {subdivision.name}?", "Yes", "No");
                if (answer)
                {
                    var data = await new DAL.Subdivision().DeleteAsync(country.id.Value, subdivision.id.Value);
                    if (data != null && data.Error != null && data.Error.errorMessages?.Count > 0)
                    {
                        await page.DisplayAlert("Subdivision ", data.Error.errorMessages[0], "Ok");

                    }
                    else
                        LoadItemsCommand();
                }
            }
        }
    }
}
