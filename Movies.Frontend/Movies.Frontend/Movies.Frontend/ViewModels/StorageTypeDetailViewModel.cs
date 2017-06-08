using Movies.Frontend.Interfaces;
using Movies.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Frontend.ViewModels
{
    public class StorageTypeDetailViewModel
    {
        private readonly BaseStorageStore storageStore;
        private readonly IPageService pageService;

        public event EventHandler<Storage> StorageAdded;
        public event EventHandler<Storage> StorageUpdated;
        public event EventHandler<StorageViewModel> StorageDeleted;

        public Storage Storage { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public StorageTypeDetailViewModel(StorageViewModel viewModel, BaseStorageStore storageStore, IPageService pageService)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            this.pageService = pageService;
            this.storageStore = storageStore;

            this.SaveCommand = new Command(async () => await Save());
            this.DeleteCommand = new Command<StorageViewModel>(async c => await Delete(viewModel));

            this.Storage = new Storage
            {
                Id = viewModel.Id,
                StorageName = viewModel.StorageName,
                Url = viewModel.Url,
                DateAdded = viewModel.DateAdded,
                DateModified = viewModel.DateModified
            };
        }

        private async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Storage.StorageName))
            {
                await this.pageService.DisplayAlert("Error", "Please enter a storage name.", "Ok");
                return;
            }

            if (Storage.Id == 0)
            {
                await this.storageStore.AddStorageType(Storage);

                StorageAdded?.Invoke(this, Storage);
            }
            else
            {
                await this.storageStore.UpdateStorageType(Storage);

                StorageUpdated?.Invoke(this, Storage);
            }

            await this.pageService.PopAsync();
        }

        private async Task Delete(StorageViewModel storageViewModel)
        {
            if (await this.pageService.DisplayAlert("Warning", $"Are you sure you want to delete {storageViewModel.StorageName}?", "Yes", "No"))
            {
                var storageType = await this.storageStore.GetStorage(storageViewModel.Id);
                StorageDeleted?.Invoke(this, storageViewModel);
                await this.storageStore.DeleteStorage(storageType);
                await this.pageService.PopAsync();
            }
        }
    }
}
