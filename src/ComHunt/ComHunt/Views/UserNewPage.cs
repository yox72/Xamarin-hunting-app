using System;

using Xamarin.Forms;

namespace ComHunt.Views
{
    public class UserNewPage : ContentPage
    {
        public UserNewPage()
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

