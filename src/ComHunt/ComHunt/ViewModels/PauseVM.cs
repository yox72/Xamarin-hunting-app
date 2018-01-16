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
        private string state = "";

        public PauseVM(Page page, string NomChasse)
        {
            _page = page;
            NumberChasse = NomChasse;
            initCommand();
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            /*var observable = firebase
                .Child("Chasse n°1")
                .Child("etat")
                .AsObservable<string>()
                .Subscribe(
                    c =>
                    {
                        init(c.Object);
                        System.Diagnostics.Debug.WriteLine("State of chasse n°1 : " + c.Object);
                    });
            initDB();*/
            /*var observable = firebase
                .Child("chasses")
                .AsObservable<Chasse>()
                .Subscribe(
                    c =>
                    {
                        init(c.Object);
                    });*/
            var observable = firebase
                .Child(NumberChasse)
                .Child("EtatDeChasse")
                .AsObservable<Etat>()
                .Subscribe(
                    c =>
                    {
                        init(c.Object);
                    });
        }

        private async void initDB(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var items = await firebase.Child("chasses").OnceAsync<Chasse>();
            foreach(var item in items)
            {
                var name = item.Object.name;
                var etat = item.Object.etat;
            }

        }

        private void initCommand(){
            refreshCommand = new Command(Refresh);
        }

        public async void Refresh(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var chasseEtat = 
                (await firebase.Child(NumberChasse).Child("EtatDeChasse").OnceAsync<Etat>())
                    .FirstOrDefault().Object.etat;
            
            if (/*chasseEtat.Equals("Actif")*/ state.Equals("True"))
            {
                _page.Navigation.PopModalAsync();
            }
            else 
                await _page.DisplayAlert("Alerte", "Attente de l'activation du responsable de battue", "OK");//Afficher erreur 
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

        public async void init(Etat c){
            if (c.etat=="Actif"){
                _page.Navigation.PopModalAsync();
                //state = "True";
            }
        }

    }
}

