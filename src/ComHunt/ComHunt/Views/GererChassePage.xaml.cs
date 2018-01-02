using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class GererChassePage : ContentPage
    {
        public GererChassePage(string NumberChasse)
        {
            InitializeComponent();
            BindingContext = new GererChasseVM(this, NumberChasse);
        }
    }
}
