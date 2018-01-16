using System;
using System.ComponentModel;
using System.Windows.Input;
using ComHunt.Services;
using ComHunt.Views;
using Xamarin.Forms;
using Plugin.Connectivity;  

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
        public ICommand connectionBluetoothCommand { get; set; }
        public ICommand testBDCommand { get; set; }
        public ICommand mapCommand { get; set; }

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
            connectionBluetoothCommand = new Command(ConnectionBluetooth);
            testBDCommand = new Command(TestBD);
            mapCommand = new Command(Map);
        }

        void init()
        {

            /*if (!DependencyService.Get<IUserService>().IsLogged())
            {
                var loginPage = new LoginPage();
                var loginVM = (LoginVM) loginPage.BindingContext;
                loginVM.CallBack = OnNewUserCallback;

                _page.Navigation.PushModalAsync(loginPage);
            }*/
        }

        public void OnNewUserCallback(string name){
            DependencyService.Get<IUserService>().Connect(name);
            Name = name;
        }

        public void CreaChasse(){
            //Créer numéro random de la chasse
            _page.Navigation.PushAsync(new CreaChassePage());//Ouvirir vue CreaChasse
        }

        public void JoinChasse()
        {
            _page.Navigation.PushAsync(new JoinChassePage());//Ouvirir vue JoinChasse
        }

        public void ConnectionBluetooth()
        {
            _page.Navigation.PushAsync(new ConnectionBluetoothPage());//Ouvirir vue ConnectionBluetooth
        }

     
        public void TestBD()
        {
            _page.Navigation.PushAsync(new TestBDPage(""));//Ouvrir vue TestBD
        }

        public void Map()
        {
            //_page.Navigation.PushAsync(new MapPage());
        }

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
