using Movies.Frontend.Models;
using Movies.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorageMethodsPage : ContentPage
    {
        private BaseStorageStore storageStore;

        public ObservableCollection<StorageViewModel> Storages { get; private set; } = new ObservableCollection<StorageViewModel>();

        public StorageMethodsPage()
        {
            InitializeComponent();

            PopulateList();
        }

        public async Task PopulateList()
        {
            var storageTypes = await this.storageStore.GetStorageAsync();

            foreach (var c in storageTypes)
            {
                Storages.Add(new StorageViewModel(c));
            }

            storageList.ItemsSource = storageTypes;
            //{
            //    "DVD",
            //    "Vudu",
            //    "BluRay",
            //    "GooglePlay",
            //    "Amazon"
            //};
        }

        public ListView StorageTypes
        {
            get
            {
                return storageList;
            }
        }
    }
}
