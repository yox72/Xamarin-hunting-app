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
            joinChasseCommand = new Command(JoinChasse);
            deconnexionCommand = new Command(Deconnexion);
        }
        public ICommand creaChasseCommand { get; set; }
        public ICommand joinChasseCommand { get; set; }
        public ICommand deconnexionCommand { get; set; }

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

        public void JoinChasse()
        {
            _page.Navigation.PushAsync(new JoinChassePage());//Ouvirir vue JoinChasse
        }

        async void Deconnexion()
        {
            var ans = await _page.DisplayAlert("Deconnexion", "Voulez vous vous deconnecter ?", "Oui", "Non");
            //BUG N'ATTEND PAS QUE L'UTILISATEUR AI CLIQUÉ SUR OUI OU NON
            if (ans == true)
            {
                Application.Current.Properties["IsLogged"] = false;
                init();
            }
            else {/*do nothing*/}
        }
    }
}
