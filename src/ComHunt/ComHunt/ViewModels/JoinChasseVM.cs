using System;
using System.Windows.Input;
using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class JoinChasseVM : ContentView
    {
        private JoinChassePage joinChassePage;

        public JoinChasseVM()
        {
            entrerCommand = new Command(Entrer);
        }

        public JoinChasseVM(JoinChassePage joinChassePage)
        {
            this.joinChassePage = joinChassePage;
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

