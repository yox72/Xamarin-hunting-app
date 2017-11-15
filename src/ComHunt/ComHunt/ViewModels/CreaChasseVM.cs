using System;

using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class CreaChasseVM : ContentView
    {
        private Page _page;
        //public string Email => (string)Application.Current.Properties["NumeroChasse"];

        public CreaChasseVM(Page page)
        {
            _page = page;
            init();
        }

        private Random randomNumber = new Random();

        public void init(){
            int numChasse = randomNumber.Next(1, 9999);
            Application.Current.Properties["NumeroChasse"] = numChasse;
        }
    }
}

