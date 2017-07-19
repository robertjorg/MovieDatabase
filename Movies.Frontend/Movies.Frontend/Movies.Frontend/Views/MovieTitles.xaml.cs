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
    public partial class MovieTitles : ContentPage
    {
        public MovieTitles()
        {
            var movieStore = new BaseMovieTitleStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new MovieTitlesViewModel(movieStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedMovieCommand.Execute(e.SelectedItem);
        }

        public MovieTitlesViewModel ViewModel
        {
            get { return BindingContext as MovieTitlesViewModel; }
            set { BindingContext = value; }
        }
    }
}
