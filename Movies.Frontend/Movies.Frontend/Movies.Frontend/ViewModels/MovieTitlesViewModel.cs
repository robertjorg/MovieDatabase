using Movies.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Frontend.ViewModels
{
    class MovieTitlesViewModel
    {
        private bool isDataLoaded;

        private MovieTitleViewModel selectedMovie;

        private readonly IPageService pageService;

        private BaseMovieTitleStore movieStore;

        public ICommand AddMovieCommane { get; private set; }

        public ICommand SelectedMovieCommand { get; set; }

        public ICommand LoadDataCommand { get; set; }

        public ICommand DeleteMovieCommand { get; set; }

        public MovieTitlesViewModel()
        {
            this.LoadDataCommand = new Command(async () => await LoadData());

            this.AddMovieCommane = new Command(async () => await AddContacts());

            this.SelectedMovieCommand = new Command<MovieTitleViewModel>(async c => await SelectMovie(c));

            this.DeleteMovieCommand = new Command<MovieTitleViewModel>(async c => await DeleteMovie(c));


        }
    }
}
