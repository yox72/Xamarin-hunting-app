using System;
using System.Linq;
using System.Reactive.Linq;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class PauseVM : ContentView
    {
        private Page _page;
        public Command refreshCommand { get; set; }
        public string NumberChasse;

        public PauseVM(Page page, string NomChasse)
        {
            _page = page;
            NumberChasse = NomChasse;
            initCommand();
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var observable = firebase
                    .Child(NumberChasse)
                    .Child("EtatDeChasse")
                .AsObservable<Etat>()
                .Subscribe(e => init(e.Key));
        }

        private void initCommand(){
            refreshCommand = new Command(Refresh);
        }

        public async void Refresh(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var Bonjour = (await firebase
                           .Child(NumberChasse)
                           .Child("EtatDeChasse")
                           .OrderByKey()
                           //.LimitToFirst(2)
                           //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                           .OnceAsync<Etat>())
                           .Select(item =>
                                   new Etat
                                   {
                                       etat = item.Object.etat
                                   }
                               ).ToList();
            if (Bonjour[0].etat == "Actif")
            {
                _page.Navigation.PopModalAsync();
            }
            else await _page.DisplayAlert("Alerte", "Attente de l'activation du responsable de battue", "OK");//Afficher erreur 
        }
        /*public async void Refresh()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var observable = firebase
                    .Child(NumberChasse)
                    .Child("EtatDeChasse")
                .AsObservable<Chasse>()
                .Subscribe(c => init(c));
        }*/

        public async void init(Etat e){
            if (c.etat=="actif"){
                _page.Navigation.PopModalAsync();
            }
        }

    }
}

