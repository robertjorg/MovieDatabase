using Movies.Frontend.Interfaces;
using Movies.Frontend.Models;
using Movies.Frontend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Frontend.ViewModels
{
    public class StorageMethodsViewModel : BaseViewModel
    {
        private bool isDataLoaded;

        private StorageViewModel storageType;

        private readonly IPageService pageService;

        private BaseStorageStore storageStore;

        public event EventHandler<Storage> StorageSelected;

        public ObservableCollection<StorageViewModel> Storages { get; private set; } = new ObservableCollection<StorageViewModel>();

        public StorageViewModel SelectedStorage
        {
            get
            {
                return this.storageType;
            }
            set
            {
                if (this.storageType != value)
                {
                    this.storageType = value;
                    this.OnPropertyChanged(nameof(SelectedStorage));
                }
            }
        }

        public ICommand AddStorageCommand { get; private set; }

        public ICommand LoadDataCommand { get; set; }

        public StorageMethodsViewModel(BaseStorageStore storageStore, IPageService pageService)
        {
            this.storageStore = storageStore;
            this.pageService = pageService;

            this.LoadDataCommand = new Command(async () => await LoadData());

            this.AddStorageCommand = new Command(async () => await AddStorage());
        }

        private async Task LoadData()
        {
            if (this.isDataLoaded)
            {
                return;
            }

            this.isDataLoaded = true;

            var storageTypes = await this.storageStore.GetStorageAsync();

            foreach (var c in storageTypes)
            {
                Storages.Add(new StorageViewModel(c));
            }
        }

        private async Task AddStorage()
        {
            var viewModel = new StorageTypeDetailViewModel(new StorageViewModel(), this.storageStore, this.pageService);

            viewModel.StorageAdded += (source, storageType) =>
            {
                //try
                //{
                Storages.Add(new StorageViewModel(storageType));
                //}
                //catch (Exception e)
                //{
                //    Debugger.Break();
                //}
            };

            await this.pageService.PushAsync(new StorageTypeDetailView(viewModel));
        }
    }
}
