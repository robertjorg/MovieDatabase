﻿using Movies.Frontend.ViewModels;
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
    public partial class PlexLogin : ContentPage
    {
        public PlexLogin(PlexLoginViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
