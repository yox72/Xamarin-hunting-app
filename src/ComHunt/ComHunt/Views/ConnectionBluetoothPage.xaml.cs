using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class ConnectionBluetoothPage : ContentPage
    {
        private IAdapter adapter;
        private IBluetoothLE ble;
        public ConnectionBluetoothPage()
        {
            InitializeComponent();
            BindingContext = new ConnectionBluetoothVM(this, ble, adapter);
        }
    }
}
