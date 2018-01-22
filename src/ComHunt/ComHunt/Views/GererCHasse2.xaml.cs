using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class GererCHasse2 : ContentPage
    {
        public GererCHasse2()
        {
            InitializeComponent();
            BindingContext = new WelcomePageVM(this);
        }
    }
}
