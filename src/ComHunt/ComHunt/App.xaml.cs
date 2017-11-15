﻿using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Properties["IsLogged"] = false;
            Properties["Name"] = "";
            Properties["NumeroChasse"] = 0;
            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
