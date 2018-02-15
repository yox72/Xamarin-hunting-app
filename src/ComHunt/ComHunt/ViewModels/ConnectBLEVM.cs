using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ComHunt.Views;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class ConnectBLEVM : ContentView
    {
        private Page _page;

        public IAdapter adapter;
        public IBluetoothLE ble;

        public string NumberJoinChasse;
        public ICommand scanCommand { get; set; }
        public ICommand connectCommand { get; set; }
        public ConnectBLEVM(Page page, string NomChasse)
        {
            _page = page;
            NumberJoinChasse = NomChasse;
            init();
            initCommand();
        }

        public void init()
        {
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            //lv.ItemsSource = deviceList;
        }

        public void initCommand()
        {
            scanCommand = new Command(scan);
            connectCommand = new Command(connect);
        }

        ObservableCollection<IDevice> deviceList;
        IDevice device;

        public async void scan(/*object sender, EventArgs e*/)
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
                await _page.DisplayAlert("Bluetooth", "Verifiez que le Bluetooth est activé", "OK");
            }

        }

        private void lv_ItemSelected(/*object sender, SelectedItemChangedEventArgs e*/)
        {
            if (lv.SelectedItem == null)
            {
                return;
            }
            device = lv.SelectedItem as IDevice;
        }

        public async void connect(/*object sender, EventArgs e*/)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
                //_page.Navigation.PushAsync(new TestBDPage(NumberJoinChasse, device));//Ouvrir vue TestBD
            }
            catch (DeviceConnectionException e)
            {
                await _page.DisplayAlert("Alerte", "Problème de connexion", "OK");
            }
        }
    }
}

