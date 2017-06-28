using Movies.Frontend.Interfaces;
using Movies.Frontend.Models;
using Movies.Frontend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Frontend.ViewModels
{
    public class MovieTitleDetailViewModel
    {
        private readonly BaseMovieTitleStore movieTitleStore;
        private BaseStorageStore storageStore;
        private readonly IPageService pageService;

        public event EventHandler<MovieTitle> MovieAdded;
        public event EventHandler<MovieTitle> MovieUpdated;
        public event EventHandler<MovieTitleViewModel> MovieDeleted;
        public event EventHandler<StorageMethodsViewModel> SelectStorage;

        public MovieTitle MovieTitle { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public MovieTitleDetailViewModel(MovieTitleViewModel viewModel, BaseMovieTitleStore movieStore, IPageService pageService)
        {
            if(viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            this.pageService = pageService;
            this.movieTitleStore = movieStore;

            this.SaveCommand = new Command(async () => await Save());

            MovieTitle = new MovieTitle
            {
                Id = viewModel.Id,
                TitleReleaseDate = viewModel.TitleReleaseDate,
                Title = viewModel.Title,
                MovieDesc = viewModel.MovieDesc,
                ReleaseDate = viewModel.ReleaseDate,
                ImdbUrl = viewModel.ImdbUrl,
                StorageType = viewModel.StorageType,
                DateAdded = viewModel.DateAdded,
                DateModified = viewModel.DateModified,
            };
        }

        private async Task Save()
        {
            if (String.IsNullOrWhiteSpace(MovieTitle.Title))
            {
                await this.pageService.DisplayAlert("Error", "Please enter a title.", "Ok");
                return;
            }

            if(MovieTitle.Id == 0)
            {
                await this.movieTitleStore.AddMovieTitle(MovieTitle);

                MovieAdded?.Invoke(this, MovieTitle);
            }
            else
            {
                await this.movieTitleStore.UpdateMovieTitle(MovieTitle);

                MovieUpdated?.Invoke(this, MovieTitle);
            }

            await this.pageService.PopAsync();
        }
    }
}
