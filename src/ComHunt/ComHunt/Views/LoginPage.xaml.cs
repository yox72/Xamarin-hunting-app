using System;
using System.Collections.Generic;
using ComHunt.ViewModels;

using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class LoginPage : ContentPage
    {
        private App app;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginVM(this) { /*Constructeur*/};
        }

        public LoginPage(App app)
        {
            this.app = app;
        }
    }
}
