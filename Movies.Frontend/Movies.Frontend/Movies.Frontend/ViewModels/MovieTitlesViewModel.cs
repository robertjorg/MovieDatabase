using Movies.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Movies.Frontend.Views;

namespace Movies.Frontend.ViewModels
{
    public class MovieTitlesViewModel : BaseViewModel
    {
        private bool isDataLoaded;

        private MovieTitleViewModel selectedMovie;

        private readonly IPageService pageService;

        private BaseMovieTitleStore movieStore;

        public ObservableCollection<MovieTitleViewModel> Movies { get; private set; } = new ObservableCollection<MovieTitleViewModel>();

        public MovieTitleViewModel SelectedMovie
        {
            get
            {
                return this.selectedMovie;
            }
            set
            {
                if (this.selectedMovie != value)
                {
                    this.selectedMovie = value;
                    this.OnPropertyChanged(nameof(SelectedMovie));
                }

            }
        }

        public ICommand AddMovieCommand { get; private set; }

        public ICommand SelectedMovieCommand { get; set; }

        public ICommand LoadDataCommand { get; set; }

        public ICommand DeleteMovieCommand { get; set; }

        public MovieTitlesViewModel(BaseMovieTitleStore movieStore, IPageService pageService)
        {
            this.movieStore = movieStore;
            this.pageService = pageService;

            this.LoadDataCommand = new Command(async () => await LoadData());

            this.AddMovieCommand = new Command(async () => await AddMovieTitle());

            this.SelectedMovieCommand = new Command<MovieTitleViewModel>(async c => await SelectMovie(c));

            this.DeleteMovieCommand = new Command<MovieTitleViewModel>(async c => await DeleteMovie(c));
        }

        private async Task LoadData()
        {
            if (this.isDataLoaded)
            {
                return;
            }

            this.isDataLoaded = true;

            var movieTitles = await this.movieStore.GetMovieTitlesAsync();

            foreach(var c in movieTitles)
            {
                Movies.Add(new MovieTitleViewModel(c));
            }
        }

        private async Task AddMovieTitle()
        {
            var viewModel = new MovieTitleDetailViewModel(new MovieTitleViewModel(), this.movieStore, this.pageService);

            viewModel.MovieAdded += (source, movieTitle) =>
            {
                //try
                //{
                    Movies.Add(new MovieTitleViewModel(movieTitle));
                //}
                //catch (Exception e)
                //{
                //    Debugger.Break();
                //}
            };

            await this.pageService.PushAsync(new MovieTitleDetailView(viewModel));
        }

        private async Task SelectMovie(MovieTitleViewModel movieTitle)
        {
            if(movieTitle == null)
            {
                return;
            }

            SelectedMovie = null;

            var viewModel = new MovieTitleDetailViewModel(movieTitle, this.movieStore, this.pageService);
            viewModel.MovieUpdated += (source, updatedMovie) =>
            {
                movieTitle.Id = updatedMovie.Id;
                movieTitle.TitleReleaseDate = updatedMovie.TitleReleaseDate;
                movieTitle.Title = updatedMovie.Title;
                movieTitle.MovieDesc = updatedMovie.MovieDesc;
                movieTitle.ReleaseDate = updatedMovie.ReleaseDate;
                movieTitle.ImdbUrl = updatedMovie.ImdbUrl;
                movieTitle.StorageType = updatedMovie.StorageType;
                movieTitle.DateAdded = updatedMovie.DateAdded;
                movieTitle.DateModified = updatedMovie.DateModified;
            };

            await this.pageService.PushAsync(new MovieTitleDetailView(viewModel));
        }

        private async Task DeleteMovie(MovieTitleViewModel movieTitleViewModel)
        {
            if(await this.pageService.DisplayAlert("Warning", $"Are you sure you want to delete {movieTitleViewModel.Title}?", "Yes", "No"))
            {
                Movies.Remove(movieTitleViewModel);

                var movie = await this.movieStore.GetMovie(movieTitleViewModel.Id);
                await this.movieStore.DeleteMovieTitle(movie);
            }
        }
    }
}
