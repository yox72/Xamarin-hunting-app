using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class ConnectBLEPage : ContentPage
    {
        public ConnectBLEPage(string NumberChasse)
        {
            InitializeComponent();
            BindingContext = new ConnectBLEVM(this, NumberChasse);
        }
    }
}
