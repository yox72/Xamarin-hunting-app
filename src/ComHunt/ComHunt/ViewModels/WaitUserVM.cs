using System;
using System.Linq;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class WaitUserVM : ContentView
    {
        private Page _page;
        public string NumberChasse;
        public string nombreChasseurs;
        public string nombreChasseurActifs;
        public string nombreChefs;
        public string nombreChefsActifs;

        public ICommand refreshCommand { get; set; } 

        public WaitUserVM(Page page, string NumeroChasse)
        {
            _page = page;
            NumberChasse = NumeroChasse;
            initCommand();
        }

        public void initCommand(){
            refreshCommand = new Command(refresh);
        }

        public async void refresh(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                                 .Child(NumberChasse)
                                 .Child("Nombre")
                                   .OrderByKey()
                                   //.LimitToFirst(2)
                                   //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                                   .OnceAsync<Chasse>())
                                   .Select(item =>
                                           new Chasse
                                           {
                                               nombreChasseurs = item.Object.nombreChasseurs,
                                               nombreChasseursActifs = item.Object.nombreChasseursActifs,
                                               nombreChefs = item.Object.nombreChefs,
                                               nombreChefsActifs = item.Object.nombreChefsActifs
                                           }
                                       ).ToList();
            nombreChasseurs = list[0].nombreChasseurs;
            nombreChasseurActifs = list[0].nombreChasseursActifs;
            nombreChefs = list[1].nombreChefs;
            nombreChefsActifs = list[1].nombreChefsActifs;

            if (nombreChefs == nombreChefsActifs && nombreChasseurs == nombreChasseurActifs){
                await _page.Navigation.PushAsync(new GererChassePage(NumberChasse));//Ouvirir vue GererChasse
            }
        }
    }
}

