using System;
using System.Linq;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Services;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class JoinChasseVM : ContentView
    {
        private Page _page;
        public string NumeroJoinChasse { get; set; }

        public JoinChasseVM(Page page)
        {
            _page = page;
            NumeroJoinChasse = DependencyService.Get<IUserService>().getName();
            entrerCommand = new Command(Entrer);
        }
        public ICommand entrerCommand { get; set; }

        public async void Entrer(){
            /*var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");

            //Test Pour savoir si la chasse existe bien
            string LaBDD = await firebase.ToString();

            var list = (await firebase
                        .Child("Chasse")
                        .Child("ChasseVue")
                        //.Child("Vue")
                        .OrderByKey()
                        //.LimitToFirst(2)
                        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                        .OnceAsync<Vue>())
                        .Select(item =>
                                new Vue
                                {
                                    Chevreuil = item.Object.Chevreuil,
                                    Renard = item.Object.Renard,
                                    Sanglier = item.Object.Sanglier
                                }

                       ).ToList();*/
            _page.Navigation.PushAsync(new TestBDPage(NumeroJoinChasse));//Ouvirir vue JoinChasse
        }
    }
}

