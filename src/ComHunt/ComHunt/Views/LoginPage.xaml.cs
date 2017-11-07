using System;
using System.Collections.Generic;
using ComHunt.ViewModels;

using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginVM(this.Navigation) { /*Constructeur*/};
        }
    }
}
