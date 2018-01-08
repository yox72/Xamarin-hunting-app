using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class WaitUserPage : ContentPage
    {
        public WaitUserPage(string NumberChasse)
        {
            InitializeComponent();
            BindingContext = new WaitUserVM(this, NumberChasse);
        }
    }
}
