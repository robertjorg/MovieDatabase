using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Movies.Frontend
{
    using Movies.Frontend.Views;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GetAllMovies(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new GetAllMovies());
        }

        private void FindMovies(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new FindMovie());
        }

        private void AddMovies(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddMovies());
        }
    }
}
