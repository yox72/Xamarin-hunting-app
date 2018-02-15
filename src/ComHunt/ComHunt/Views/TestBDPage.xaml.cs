using System.Collections.ObjectModel;
using ComHunt.Database;
using ComHunt.Models;
using ComHunt.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class TestBDPage : ContentPage
    {
        ObservableCollection<Vue> list = new ObservableCollection<Vue>();
        private IDevice device;

        /*public TestBDPage()
        {
            InitializeComponent();
            BindingContext = new TestBDVM(this, "");
            _lst.BindingContext = list;
        }*/

        public TestBDPage(string NumberJoinChasse, IDevice device)
        {
            InitializeComponent();
            BindingContext = new TestBDVM(this, NumberJoinChasse, device);
        }
    }
}
