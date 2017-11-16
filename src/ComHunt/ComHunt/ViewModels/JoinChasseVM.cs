using System;
using System.Windows.Input;
using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class JoinChasseVM : ContentView
    {
        private Page _page;

        public JoinChasseVM(Page page)
        {
            _page = page;
            entrerCommand = new Command(Entrer);
        }
        public ICommand entrerCommand { get; set; }

        public void Entrer(){
            if (Application.Current.Properties["NumerChasse"] == Application.Current.Properties["NuemroJoinChasse"])
            {
                //C'EST GAGNÉ !!!!!!!!!
            }
            
        }
    }
}

