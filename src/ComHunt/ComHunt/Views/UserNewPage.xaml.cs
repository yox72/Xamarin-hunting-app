using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class UserNewPage : ContentPage
    {
        public UserNewPage()
        {
            InitializeComponent();
            BindingContext = new NewUserVM(this);
        }
    }
}
