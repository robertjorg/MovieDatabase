using Movies.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Movies.Frontend.Persistence
{
    class PageService : IPageService
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<Page> PopAsync()
        {
            return await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private Page MainPage
        {
            get
            {
                return Application.Current.MainPage;
            }
        }
    }
}
