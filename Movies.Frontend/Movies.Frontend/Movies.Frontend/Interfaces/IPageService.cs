﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Movies.Frontend.Interfaces
{
    public interface IPageService
    {
        Task PushAsync(Page page);

        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        Task DisplayAlert(string title, string message, string ok);
    }
}
