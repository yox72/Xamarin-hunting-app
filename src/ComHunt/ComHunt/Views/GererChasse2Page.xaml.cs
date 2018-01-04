using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class GererChasse2Page : ContentView
    {
        public GererChasse2Page(string NumberChasse)
        {
            InitializeComponent();
            BindingContext = new GererChasse2VM(this, NumberChasse);
        }
    }
}
