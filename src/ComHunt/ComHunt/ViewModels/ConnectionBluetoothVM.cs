using System;
using System.ComponentModel;
using System.Windows.Input;
using ComHunt.Views;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class ConnectionBluetoothVM : INotifyPropertyChanged
    {
        private Page _page;
        private IAdapter adapter;
        private IBluetoothLE ble;
        private ListView deviceList;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand connectionCommand { get; set; }
        public ICommand deconnectionCommand { get; set; }
        public ICommand led1Command { get; set; }
        public ICommand led2Command { get; set; }
        public ICommand led3Command { get; set; }
        public ICommand ledShutDownCommand { get; set; }


        public ConnectionBluetoothVM(Page page, IBluetoothLE ble, IAdapter adapter)
        {
            _page = page;
            this.ble = ble;
            this.adapter = adapter;
            initCommands();
            //var state = ble.State;
        }


        void initCommands(){
            connectionCommand = new Command(Connection);
            deconnectionCommand = new Command(Deconnection);
            led1Command = new Command(Led1);
            led2Command = new Command(Led2);
            led3Command = new Command(Led3);
            ledShutDownCommand = new Command(LedShutDown);
        }

        private async void init(string state){
            
        }

        public async void Connection(){
            if (device.IsPairingRequestSupported && device.PairingStatus != PairingStatus.Paired)
            {
                // there is an optional argument to pass a PIN in PairRequest as well
                device.PairRequest().Subscribe(isSuccessful => { });
            }
        }
        public void Deconnection(){}
        public void Led1(){
            
        }
        public void Led2(){}
        public void Led3(){}
        public void LedShutDown(){}

    }
}

