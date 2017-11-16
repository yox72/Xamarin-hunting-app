using System;
using System.ComponentModel;
using System.Windows.Input;
using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class WelcomePageVM : INotifyPropertyChanged
    {
        private Page _page;

        public event PropertyChangedEventHandler PropertyChanged;

        //public string Email => (string)Application.Current.Properties["Name"];

        public WelcomePageVM(Page page)
        {
            _page = page;
            init();
            initCommands();
        }

        private void initCommands()
        {
            creaChasseCommand = new Command(CreaChasse);
            joinChasseCommand = new Command(JoinChasse);
            deconnexionCommand = new Command(Deconnexion);
        }

        public ICommand creaChasseCommand { get; set; }
        public ICommand joinChasseCommand { get; set; }
        public ICommand deconnexionCommand { get; set; }

        void init()
        {

            if (Application.Current.Properties["IsLogged"] != null && !((bool)Application.Current.Properties["IsLogged"]))
            {
                var loginPage = new LoginPage();
                var loginVM = (LoginVM) loginPage.BindingContext;
                loginVM.CallBack = OnNewUserCallback;

                _page.Navigation.PushModalAsync(loginPage);
                Application.Current.Properties["IsLogged"] = true;
            }
        }

        public void OnNewUserCallback(string name){
            Name = name;
        }

        public string Name
        {
            get
            {
                return (string)Application.Current.Properties["Name"];
            }

            set
            {
                Application.Current.Properties["Name"] = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name")); //update
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

            if (ans)
            {
                Application.Current.Properties["IsLogged"] = false;
                init();
            }
        }
    }
}
