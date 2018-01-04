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

        public ICommand entrerCommand { get; set; }

        public JoinChasseVM(Page page)
        {
            _page = page;
            init();
            initCommand();
        }

        void init(){
            NumeroJoinChasse = DependencyService.Get<IUserService>().getName();
        }

        void initCommand(){
            entrerCommand = new Command(Entrer);
        }


        public async void Entrer(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var Bonjour = (await firebase
                           .Child(NumeroJoinChasse)
                           .Child("NameChasse")
                           .OrderByKey()
                           //.LimitToFirst(2)
                           //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                           .OnceAsync<Chasse>())
                           .Select(item =>
                                   new Chasse
                                   {
                                        name = item.Object.name
                                   }
                               ).ToList();
            try
            {
                if (Bonjour[0].name.Contains(NumeroJoinChasse))
                {
                    await _page.Navigation.PushAsync(new TestBDPage(NumeroJoinChasse));//Ouvirir vue JoinChasse
                }
            }
            catch (Exception e)
            { 
                await _page.DisplayAlert("Alerte", "Nom de chasse incorrect", "OK");//Afficher erreur }     
            }
        }
    }
}

