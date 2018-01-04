using System;
using System.Linq;
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
                //await _page.Navigation.PushAsync(new TestBDPage(NumberChasse));//Ouvrir vue TestBD
                _page.Navigation.PopModalAsync();
            }
            else await _page.DisplayAlert("Alerte", "Attente de l'activation du responsable de battue", "OK");//Afficher erreur 
        }
    }
}

