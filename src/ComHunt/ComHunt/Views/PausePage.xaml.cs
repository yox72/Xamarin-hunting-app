using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class PausePage : ContentPage
    {
        public PausePage(string NumberChasse)
        {
            InitializeComponent();
            this.BindingContext = new PauseVM(this, NumberChasse);
        }
    }
}
