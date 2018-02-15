using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ComHunt.ViewModels;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class BlushitBLEPage : ContentPage
    {
        public IAdapter adapter;
        public IBluetoothLE ble;
        String NumberJoinChasse;

        public BlushitBLEPage(String NomChasse)
        {
            InitializeComponent();
            NumberJoinChasse = NomChasse;
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            lv.ItemsSource = deviceList;
            BindingContext = new ConnectionBluetoothVM(this/*, ble, adapter*/);
        }

        ObservableCollection<IDevice> deviceList;
        IDevice device;

        public async void btnScanDev_Clicked(object sender, EventArgs e)
        {
            try
            {
                adapter.ScanTimeout = 10000;
                deviceList.Clear();
                adapter.DeviceDiscovered += (s, a) =>
                {
                    deviceList.Add(a.Device);
                };
                await adapter.StartScanningForDevicesAsync();
            }
            catch
            {
                await DisplayAlert("Bluetooth", "Verifiez que le Bluetooth est activé", "OK");
            }
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lv.SelectedItem == null)
            {
                return;
            }
            device = lv.SelectedItem as IDevice;
        }

        private async void btnConnect_Clicked(object sender, EventArgs lo)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
                await Navigation.PushAsync(new TestBDPage(NumberJoinChasse, device));
            }
            catch (DeviceConnectionException e)
            {
                await DisplayAlert("Alerte", "Problème de connexion", "OK");
            }
        }

        /*IList<IService> services;
        IService service;
        IList<ICharacteristic> characteristics;
        ICharacteristic characteristic;*/
    }
}
