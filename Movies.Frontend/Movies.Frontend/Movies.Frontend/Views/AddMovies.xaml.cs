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
    public partial class AddMovies : ContentPage
    {
        public AddMovies()
        {
            this.InitializeComponent();
            this.StorageSelection.Items.Add("DVD");
            this.StorageSelection.Items.Add("Blu-Ray");
            this.StorageSelection.Items.Add("UltraViolet");
            this.StorageSelection.Items.Add("DisneyMovies");
            this.StorageSelection.Items.Add("Amazon");
            this.StorageSelection.Items.Add("GooglePlay");
            this.StorageSelection.SelectedIndex = 0;
        }
    }
}
