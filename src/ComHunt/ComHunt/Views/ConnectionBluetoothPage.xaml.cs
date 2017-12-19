using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class ConnectionBluetoothPage : ContentPage
    {
        public ConnectionBluetoothPage()
        {
            InitializeComponent();
            BindingContext = new ConnectionBluetoothVM(this);
        }
    }
}
