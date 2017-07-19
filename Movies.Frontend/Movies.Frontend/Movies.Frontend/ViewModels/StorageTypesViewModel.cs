using Movies.Frontend.Interfaces;
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
    public class StorageTypesViewModel : BaseViewModel
    {
        private bool isDataLoaded;

        private StorageViewModel storageType;

        private readonly IPageService pageService;

        private BaseStorageStore storageStore;

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

        public ICommand SelectedStorageCommand { get; set; }

        public ICommand LoadDataCommand { get; set; }

        public ICommand DeleteStorageCommand { get; set; }

        public StorageTypesViewModel(BaseStorageStore storageStore, IPageService pageService)
        {
            this.storageStore = storageStore;
            this.pageService = pageService;

            this.LoadDataCommand = new Command(async () => await LoadData());

            this.AddStorageCommand = new Command(async () => await AddStorage());

            this.SelectedStorageCommand = new Command<StorageViewModel>(async c => await SelectStorage(c));

            this.DeleteStorageCommand = new Command<StorageViewModel>(async c => await DeleteStorage(c));
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

        private async Task SelectStorage(StorageViewModel storageType)
        {
            if (storageType == null)
            {
                return;
            }

            SelectedStorage = null;

            var viewModel = new StorageTypeDetailViewModel(storageType, this.storageStore, this.pageService);
            viewModel.StorageUpdated += (source, updatedStorage) =>
            {
                storageType.Id = updatedStorage.Id;
                storageType.StorageName = updatedStorage.StorageName;
                storageType.Url = updatedStorage.Url;
                storageType.DateAdded = updatedStorage.DateAdded;
                storageType.DateModified = updatedStorage.DateModified;
            };

            await this.pageService.PushAsync(new StorageTypeDetailView(viewModel));
        }

        private async Task DeleteStorage(StorageViewModel storageViewModel)
        {
            if (await this.pageService.DisplayAlert("Warning", $"Are you sure you want to delete {storageViewModel}?", "Yes", "No"))
            {
                Storages.Remove(storageViewModel);

                var storage = await this.storageStore.GetStorage(storageViewModel.Id);
                await this.storageStore.DeleteStorage(storage);
            }
        }
    }
}
