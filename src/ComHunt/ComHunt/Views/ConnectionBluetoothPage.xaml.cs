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
    public partial class ConnectionBluetoothPage : ContentPage
    {
        public IAdapter adapter;
        public IBluetoothLE ble;
        public ConnectionBluetoothPage()
        {
            InitializeComponent();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            lv.ItemsSource = deviceList;
            BindingContext = new ConnectionBluetoothVM(this/*, ble, adapter*/);
        }

        private void btnStatus_Clicked(object sender, EventArgs e){
            var state = ble.State;
            this.DisplayAlert("Etat Bluetooth", state.ToString(), "OK");
        }

        ObservableCollection<IDevice> deviceList;
        IDevice device;

        private async void btnScanDev_Clicked(object sender, EventArgs e){
            adapter.ScanTimeout = 10000;
            //adapter.ScanMode = ScanMode.Balanced;

            deviceList.Clear();
            adapter.DeviceDiscovered += (s, a) =>
            {
                deviceList.Add(a.Device);
            };
            await adapter.StartScanningForDevicesAsync();
            //await adapter.StopScanningForDevicesAsync();
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e){
            if (lv.SelectedItem == null)
            {
                return;
            }
            device = lv.SelectedItem as IDevice;
        }

        private async void btnConnect_Clicked (object sender, EventArgs lo){
            try
            {
                await adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException e)
            {
                await DisplayAlert("Alerte", "Problème de connexion", "OK");
            } 
        }

        private async void btnConnectKnow_Clicked (object sender, EventArgs lo){
            try
            {
                //await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));
                await adapter.ConnectToKnownDeviceAsync(new Guid("146F9EA1-1C21-BEAB-1B3B-72C78D5B9985"));
            }
            catch (DeviceConnectionException e)
            {
                //... Couldn't connect to the device
            }
        }

        IList<IService> services;
        IService service;
        IList<ICharacteristic> characteristics;
        ICharacteristic characteristic;

        /*private async void btnGetServs_Clicked(object sender, EventArgs e){
            services = await device.GetServicesAsync();

            service = await device.GetServiceAsync(
                //Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
                Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
        }

        private async void btnGetCaracters_Clicked (object sender, EventArgs e){
            var characteristics = await service.GetCharacteristicsAsync();
            characteristic = await service.GetCharacteristicAsync(
                //Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
                Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
        }

        private async void btnUpdate_Clicked (object sender, EventArgs e){
            characteristic.ValueUpdated += (o, args) =>
            {
                var bytes = args.Characteristic.Value;
            };
            await characteristic.StopUpdatesAsync();
        }*/

        private async void btnChasseArret_Clicked (object sender, EventArgs e){
            //services = await device.GetServicesAsync();
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            var data = await characteristic.ReadAsync();
            //data[0] = "0,1,2,3,4,5,6;";
            //await characteristic.WriteAsync(data);
            //Fin changement
            byte[] byteText = Encoding.UTF8.GetBytes("0,1,2,3,4,5,6;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnSanglierVue_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,1,0,0,0,0,0;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnSanglierTuer_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,0,1,0,0,0,0;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnChevreuilVue_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,0,0,1,0,0,0;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnChevreuilTuer_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,0,0,0,1,0,0;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnRenardVue_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,0,0,0,0,1,0;");
            await characteristic.WriteAsync(byteText);
        }

        private async void btnRenardTuer_Clicked(object sender, EventArgs e)
        {
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            byte[] byteText = Encoding.UTF8.GetBytes("1,0,0,0,0,0,1;");
            await characteristic.WriteAsync(byteText);
        }

        /*IList<IDescriptor> descriptors;
        IDescriptor descriptor;

        private async void btnDescriptor_Clicked (object sender, EventArgs e){
            descriptors = await characteristic.GetDescriptorsAsync();
            descriptor = await characteristic.GetDescriptorAsync(
                Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
        }

        private async void btnGetDescRW_Clicked (object sender, EventArgs e){
            var bytes = await descriptor.ReadAsync();
            await descriptor.WriteAsync(bytes);
        }*/
    }
}
