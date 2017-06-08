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
    public class MovieTitleDetailViewModel
    {
        private readonly BaseMovieTitleStore movieTitleStore;
        private readonly IPageService pageService;

        public event EventHandler<MovieTitle> MovieAdded;
        public event EventHandler<MovieTitle> MovieUpdated;
        public event EventHandler<MovieTitleViewModel> MovieDeleted;

        public MovieTitle MovieTitle { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public MovieTitleDetailViewModel(MovieTitleViewModel viewModel, BaseMovieTitleStore movieStore, IPageService pageService)
        {
            if(viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            this.pageService = pageService;
            this.movieTitleStore = movieStore;

            this.SaveCommand = new Command(async () => await Save());
            this.DeleteCommand = new Command<MovieTitleViewModel>(async c => await Delete(viewModel));

            MovieTitle = new MovieTitle
            {
                Id = viewModel.Id,
                TitleReleaseDate = viewModel.TitleReleaseDate,
                Title = viewModel.Title,
                MovieDesc = viewModel.MovieDesc,
                ReleaseDate = viewModel.ReleaseDate,
                ImdbUrl = viewModel.ImdbUrl,
                DateAdded = viewModel.DateAdded,
                DateModified = viewModel.DateModified
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

        private async Task Delete(MovieTitleViewModel titleViewModel)
        {
            if(await this.pageService.DisplayAlert("Warning", $"Are you sure you want to delete {titleViewModel.Title}?", "Yes", "No"))
            {
                var movieTitle = await this.movieTitleStore.GetMovie(titleViewModel.Id);
                MovieDeleted?.Invoke(this, titleViewModel);
                await this.movieTitleStore.DeleteMovieTitle(movieTitle);
                await this.pageService.PopAsync();
            }
        }
    }
}
