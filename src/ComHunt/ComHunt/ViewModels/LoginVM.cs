using System;
using ComHunt.Models;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        private User _user;
        private Page _page;

        public Action<string> CallBack;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginVM(Page page)
        {
            _page = page;
            _user = new User() { Email = "Yohann", Password = "" };
            loginCommand = new Command(Login);
        }


        public ICommand loginCommand { get; set; }
        public void Login()
        {
            if (Password != null && Password.Length > 0){
                CallBack(Email);
                _page.Navigation.PopModalAsync();
            } 
            else _page.DisplayAlert("Alerte", "Mot de passe vide", "OK");
        } 

        public string Email { 
            get 
            { 
                return _user.Email;
            } 

            set 
            {
                if (value != _user.Email){
                    _user.Email = value;

                    if (PropertyChanged != null){
                        PropertyChanged(this, new PropertyChangedEventArgs("Email")); //update
                    }
                }
            }
        }

        public string Password
        {
            get
            {
                return _user.Password;
            }

            set
            {
                if (value != _user.Password)
                {
                    _user.Password = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Password")); //update
                    }
                }
            }
        }
    }
}
