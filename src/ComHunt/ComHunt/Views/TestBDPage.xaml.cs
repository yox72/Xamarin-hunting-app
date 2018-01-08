using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ComHunt.Database;
using ComHunt.Models;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class TestBDPage : ContentPage
    {
        ObservableCollection<Vue> list = new ObservableCollection<Vue>();

        public TestBDPage(string NumberJoinChasse)
        {
            InitializeComponent();
            BindingContext = new TestBDVM(this, NumberJoinChasse);
            _lst.BindingContext = list;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var bd = new BDFire();

            var listAsync = await bd.getList();

            if(list!=null){
                foreach (var item in listAsync){
                    list.Add(item);
                }
            }
        }
    }
}
