using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ComHunt.Services;
using Firebase.Xamarin.Auth;
using ComHunt.Views;

namespace ComHunt.ViewModels
{
    public class NewUserVM : INotifyPropertyChanged
    {
        private Page _page;

        public string Email { get; set; }
        public string Password { get; set; }    //Actualise tout seul grace à Fody

        public ICommand loginCommand { get; set; }

        public Action<string> CallBack;

        public event PropertyChangedEventHandler PropertyChanged; //Event update

        public NewUserVM(Page page)
        {
            _page = page;
            Email = DependencyService.Get<IUserService>().getName();
            Password = DependencyService.Get<IUserService>().getPassword();
            loginCommand = new Command(Login);
        }

        public async void Login()
        {
            if (Password != null && Password.Length > 0 && Email != null && Email.Length > 0)
            {
//                CallBack(Email);
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBMmB8Ra_vGbeybLtcNOq56q6udnjCFy10"));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                _page.Navigation.PopModalAsync();
            }
            else _page.DisplayAlert("Alerte", "Nom et/ou Mot de passe vide", "OK");
        }
    }
}
