using System;
using System.Windows.Input;
using ComHunt.Services;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class CreaChasseVM : ContentView
    {
        private Page _page;
        public string NomChasse { get; set; }
        public string NbChefs { get; set; }
        public string NbChasseurs { get; set; }

        public ICommand creationChasseCommand { get; set; }

        public CreaChasseVM(Page page)
        {
            _page = page;
            init();
            initCommand();
        }

        private Random randomNumber = new Random();

        public void init(){
            //int numChasse = randomNumber.Next(1, 9999);
            //Application.Current.Properties["NumeroChasse"] = numChasse;
            NomChasse = DependencyService.Get<IUserService>().getName();
            NbChefs = DependencyService.Get<IUserService>().getName();
            NbChasseurs = DependencyService.Get<IUserService>().getName();
        }

        public void initCommand(){
            creationChasseCommand = new Command(CreationChasse);
        }

        public async void CreationChasse(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            await firebase
                .Child(NomChasse)
                .Child("EtatDeChasse")
                .PutAsync("Arret");
            await firebase
                .Child(NomChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");
            await firebase
                .Child(NomChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");
            await firebase
                .Child(NomChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");
            await firebase
                .Child(NomChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");
            await firebase
                .Child(NomChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");
            await firebase
                .Child(NomChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync("0");

            _page.Navigation.PushAsync(new GererChassePage(NomChasse));//Ouvirir vue CreaChasse
        }
    }
}

