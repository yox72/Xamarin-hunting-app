using System;
using System.Collections.Generic;
using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class JoinChassePage : ContentPage
    {
        public JoinChassePage()
        {
            InitializeComponent();
            BindingContext = new JoinChasseVM(this);
        }
    }
}
