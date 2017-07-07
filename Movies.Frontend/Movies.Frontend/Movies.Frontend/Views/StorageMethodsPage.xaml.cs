using Movies.Frontend.Models;
using Movies.Frontend.Persistence;
using Movies.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Frontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorageMethodsPage : ContentPage
    {
        public StorageMethodsPage()
        {
            InitializeComponent();

            AddStorageTypes();
        }
        
        public ListView StorageMethods
        {
            get { return storageTypes; }
        }

        public void AddStorageTypes()
        {
            storageTypes.ItemsSource = new List<string>
            {
                "Amazon",
                "BluRay",
                "DVD",
                "Disney Anywhere",
                "Google Play",
                "Plex",
                "Vudu/UltraViolet",
            };
        }
    }
}
