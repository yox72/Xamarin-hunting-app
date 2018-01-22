using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class PauseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; //Event update

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
                .AsObservable<Chasse>()
                .Subscribe(
                    c =>
                    {
                        init(c.Object);
                    });
        }

        private async void initDB()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var items = await firebase.Child("chasses").OnceAsync<Chasse>();
            foreach (var item in items)
            {
                var name = item.Object.name;
                var etat = item.Object.etat;
            }

        }

        private void initCommand()
        {
            refreshCommand = new Command(Refresh);
        }

        public async void Refresh()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var chasseEtat = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.etat;

            if (chasseEtat.Equals("Actif") /*state.Equals("True")*/)
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

        public async void init(Chasse c)
        {
            if (c.etat == "Actif")
            {
                await _page.Navigation.PopModalAsync();
                //await _page.DisplayAlert("Actif", "La chasse est actif", "OK");
                //state = "True";
            }
        }

    }
}

