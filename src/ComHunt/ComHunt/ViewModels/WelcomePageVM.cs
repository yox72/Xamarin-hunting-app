using System;
using System.Windows.Input;
using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class WelcomePageVM : MasterDetailPage
    {
        private Page _page;

        public string Email => (string)Application.Current.Properties["Name"];

        public WelcomePageVM(Page page)
        {
            _page = page;
            init();
            creaChasseCommand = new Command(CreaChasse);
            //joinChasseCommand = new Command(JoinChasse);
        }
        public ICommand creaChasseCommand { get; set; }
        //public ICommand joinChasseCommand { get; set; }

        public void init()
        {

            if (Application.Current.Properties["IsLogged"] != null && !((bool)Application.Current.Properties["IsLogged"]))
            {
                _page.Navigation.PushModalAsync(new LoginPage());
                Application.Current.Properties["IsLogged"] = true;
            }
        }

        public void CreaChasse(){
            //Créer numéro random de la chasse
            _page.Navigation.PushAsync(new CreaChassePage());//Ouvirir vue CreaChasse
        }
    }
}
