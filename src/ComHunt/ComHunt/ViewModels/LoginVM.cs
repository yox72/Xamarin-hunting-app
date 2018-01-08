using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ComHunt.Services;
using Firebase.Xamarin.Auth;
using ComHunt.Views;

namespace ComHunt.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        private Page _page;

        public string Email { get; set; }
        public string Password { get; set; }    //Actualise tout seul grace à Fody

        public ICommand loginCommand { get; set; }
        public ICommand newUserCommand { get; set; }

        public Action<string> CallBack;

        public event PropertyChangedEventHandler PropertyChanged; //Event update

        public LoginVM(Page page)
        {
            _page = page;
            Email = DependencyService.Get<IUserService>().getName();
            Password = DependencyService.Get<IUserService>().getPassword();
            loginCommand = new Command(Login);
            newUserCommand = new Command(NewUser);
        }

        public async void Login()
        {
            try{
                if (Password != null && Password.Length > 0 && Email != null && Email.Length > 0)
                {
                    CallBack(Email);
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBMmB8Ra_vGbeybLtcNOq56q6udnjCFy10"));
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                    _page.Navigation.PopModalAsync();
                }
                else _page.DisplayAlert("Alerte", "Nom et/ou Mot de passe vide", "OK");
            }
            catch (Exception e)
            {
                await _page.DisplayAlert("Alerte", "Adresse mail/Mot de passe incorrect(e)", "OK");//Afficher erreur }     
            }

        } 

        public void NewUser(){
            _page.Navigation.PopModalAsync();
            var pageuser = new NewUserPage();
            _page.Navigation.PushModalAsync(pageuser);
        }
    }
}
