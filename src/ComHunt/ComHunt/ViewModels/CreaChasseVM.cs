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
            //NomChasse = DependencyService.Get<IUserService>().getName();
            //NbChefs = DependencyService.Get<IUserService>().getName();
            //NbChasseurs = DependencyService.Get<IUserService>().getName();
        }

        public void initCommand(){
            creationChasseCommand = new Command(CreationChasse);
        }

        public async void CreationChasse(){
            try
            {
                int.Parse(NbChefs);
                int.Parse(NbChasseurs);
                if (NomChasse != null && NomChasse.Length > 0)
                {
                    var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
                    //Creation de la liste des noms de chasse
                    await firebase
                        .Child(NomChasse)
                        .Child("NameChasse")
                        .Child("Name")
                        .Child("name")
                        .PutAsync(NomChasse);
                    //Creation de l'état de la chasse
                    await firebase
                        .Child(NomChasse)
                        .Child("EtatDeChasse")
                        .Child("Etat")
                        .Child("etat")
                        .PutAsync("Arret");
                    //Creation des vues
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
                    //Creation des tuer
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
                    await firebase  //Nb chasseurs global
                      .Child(NomChasse)
                      .Child("Nombre")
                      .Child("NombreChasseurs")
                      .Child("nombreChasseurs")
                        .PutAsync(NbChasseurs);
                    await firebase  //Nb chasseurs actifs
                      .Child(NomChasse)
                      .Child("Nombre")
                      .Child("NombreChasseurs")
                      .Child("nombreChasseursActifs")
                        .PutAsync("0");
                    await firebase  //Nb Chefs de ligne
                      .Child(NomChasse)
                      .Child("Nombre")
                      .Child("NombreChefs")
                      .Child("nombreChefs")
                        .PutAsync(NbChefs);
                    await firebase  //Nb chefs de lign actifs
                      .Child(NomChasse)
                      .Child("Nombre")
                      .Child("NombreChefs")
                      .Child("nombreChefsActifs")
                        .PutAsync("0");
                    
                    await _page.Navigation.PushAsync(new WaitUserPage(NomChasse));//Ouvirir vue GererChasse
                }
                else await _page.DisplayAlert("Alerte", "Nom vide", "OK");
            }
            catch
            {
                await _page.DisplayAlert("Alerte", "Nombre de chasseur/chef de ligne incorrect", "OK");
            }
        }
    }
}

