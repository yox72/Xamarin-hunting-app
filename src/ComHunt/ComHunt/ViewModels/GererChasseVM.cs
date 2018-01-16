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
        public ICommand arretCommand { get; set; }

        public GererChasseVM(Page page, string NomChasse)
        {
            _page = page;
            NumberChasse = NomChasse;
            initCommands();
        }

        public void initCommands(){
            demarrerCommand = new Command(DemarrerChasse);
            arretCommand = new Command(ArretChasse);
        }

        public async void DemarrerChasse(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("etat")
                .PutAsync("Actif");
        }

        public async void ArretChasse()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("etat")
                .PutAsync("Arret");
        }
    }
}

