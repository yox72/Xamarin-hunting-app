using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class GererChassePage : ContentPage
    {
        public string NumberChasse2;
        public GererChassePage(string NumberChasse)
        {
            //NumberChasse2 = NumberChasse;
            InitializeComponent();
            BindingContext = new GererChasseVM(this, NumberChasse);
        }
    }
}
