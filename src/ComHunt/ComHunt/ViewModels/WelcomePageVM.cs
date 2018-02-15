using System.ComponentModel;
using System.Windows.Input;
using ComHunt.Services;
using ComHunt.Views;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class WelcomePageVM : INotifyPropertyChanged
    {
        private Page _page;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        public ICommand creaChasseCommand { get; set; }
        public ICommand joinChasseCommand { get; set; }
        public ICommand deconnexionCommand { get; set; }

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

        void init()
        {

            if (!DependencyService.Get<IUserService>().IsLogged())
            {
                var loginPage = new LoginPage();
                var loginVM = (LoginVM) loginPage.BindingContext;
                loginVM.CallBack = OnNewUserCallback;

                _page.Navigation.PushModalAsync(loginPage);
            }
        }

        public void OnNewUserCallback(string name){
            DependencyService.Get<IUserService>().Connect(name);
            Name = name;
        }

        public void CreaChasse(){
            //Créer numéro random de la chasse
            _page.Navigation.PushAsync(new CreaChassePage());//Ouvrir vue CreaChasse
        }

        public void JoinChasse()
        {
            _page.Navigation.PushAsync(new JoinChassePage());//Ouvirir vue JoinChasse
        }

        /*public void ConnectionBluetooth()
        {
            _page.Navigation.PushAsync(new ConnectionBluetoothPage());//Ouvirir vue ConnectionBluetooth
        }*/

        async void Deconnexion()
        {
            var ans = await _page.DisplayAlert("Deconnexion", "Voulez vous vous deconnecter ?", "Oui", "Non");

            if (ans)
            {
                DependencyService.Get<IUserService>().LogOut();
                init();
            }
        }
    }
}
