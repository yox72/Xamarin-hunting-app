using System;
using System.Collections.Generic;
using ComHunt.Database;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class TestBDPage : ContentPage
    {
        public TestBDPage()
        {
            InitializeComponent();
            BindingContext = new TestBDVM(this);
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var bd = new BDFire();
            var list = await bd.getList();

            if(list==null){}
        }
    }
}
