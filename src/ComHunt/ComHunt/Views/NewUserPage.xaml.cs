using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
            BindingContext = new NewUserVM(this);
        }
    }
}
