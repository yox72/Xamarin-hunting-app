using System;

using Xamarin.Forms;

namespace ComHunt.Views
{
    public class ConnectBLE : ContentPage
    {
        public ConnectBLE()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

