using System;
using System.ComponentModel;
using System.Windows.Input;
using ComHunt.Views;
//using Plugin.BLE.Abstractions.Contracts;
//using MvvmCross.Plugins.BLE;
using Xamarin.Forms;
using MvvmCross.Platform;

namespace ComHunt.ViewModels
{
    public class ConnectionBluetoothVM : INotifyPropertyChanged
    {
        private Page _page;
        //IAdapter adapter;
        //IBluetoothLE ble;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand connectionCommand { get; set; }
        public ICommand deconnectionCommand { get; set; }
        public ICommand led1Command { get; set; }
        public ICommand led2Command { get; set; }
        public ICommand led3Command { get; set; }
        public ICommand ledShutDownCommand { get; set; }


        public ConnectionBluetoothVM(Page page/*, IBluetoothLE ble, IAdapter adapter*/)
        {
            // discover some devices
            _page = page;
            //this.ble = ble;
            //this.adapter = adapter;
            init();
            initCommands();
        }


        void initCommands(){
            connectionCommand = new Command(Connection);
            deconnectionCommand = new Command(Deconnection);
            led1Command = new Command(Led1);
            led2Command = new Command(Led2);
            led3Command = new Command(Led3);
            ledShutDownCommand = new Command(LedShutDown);
        }

        private async void init(){
            //adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            //await adapter.StartScanningForDevicesAsync();
        }

        public async void Connection(){
            //await adapter.ConnectToDeviceAsync("device");    //Nom de la device
        }
        public void Deconnection(){}
        public void Led1(){
            
        }
        public void Led2(){}
        public void Led3(){}
        public void LedShutDown(){}

    }
}

