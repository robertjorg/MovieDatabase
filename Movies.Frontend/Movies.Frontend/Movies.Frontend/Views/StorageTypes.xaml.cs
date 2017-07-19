using Movies.Frontend.Persistence;
using Movies.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorageTypes : ContentPage
    {
        public StorageTypes()
        {
            var storageStore = new BaseStorageStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new StorageTypesViewModel(storageStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedStorageCommand.Execute(e.SelectedItem);
        }

        public StorageTypesViewModel ViewModel
        {
            get { return BindingContext as StorageTypesViewModel; }
            set { BindingContext = value; }
        }
    }
}
