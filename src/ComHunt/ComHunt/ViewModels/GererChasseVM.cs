using System;
using System.Windows.Input;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class GererChasseVM : ContentView
    {
        private Page _page;
        public string NumberChasse;

        public ICommand demarrerCommand { get; set; }

        public GererChasseVM(Page page, string NomChasse)
        {
            _page = page;
            NumberChasse = NomChasse;
            initCommands();
        }

        public void initCommands(){
            demarrerCommand = new Command(DemarrerChasse);
        }

        public async void DemarrerChasse(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            await firebase
                .Child(NumberChasse)
                .Child("EtatDeChasse")
                .PutAsync("Actif");
            _page.Navigation.PushAsync(new WelcomePage());
        }
    }
}

