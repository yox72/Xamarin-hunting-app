using System;

using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class CreaChasseVM : ContentView
    {
        private Page _page;
        public string Email => (string)Application.Current.Properties["NumeroChasse"];

        public CreaChasseVM(Page page)
        {
            _page = page;
            init();
        }
        public void init(){
            //que faire ici ?
        }
    }
}

