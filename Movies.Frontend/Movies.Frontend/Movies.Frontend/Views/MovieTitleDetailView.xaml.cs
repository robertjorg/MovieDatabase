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
    public partial class MovieTitleDetailView : ContentPage
    {
        public MovieTitleDetailView(MovieTitleDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        private void Storage_Tapped(object sender, EventArgs e)
        {
            var page = new StorageMethodsPage();

            page.StorageMethods.ItemSelected += (source, args) =>
            {
                storageType.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            Navigation.PushAsync(page);
        }
    }
}
