using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class TestBDVM : ContentView
    {
        private Page _page;
        public string NumberChasse;

        public ICommand sanglierVueCommand { get; set; }
        public ICommand chevreuilVueCommand { get; set; }
        public ICommand renardVueCommand { get; set; }
        public ICommand renardTuerCommand { get; set; }
        public ICommand sanglierTuerCommand { get; set; }
        public ICommand chevreuilTuerCommand { get; set; }
        public ICommand affichageCommand { get; set; }

        public TestBDVM(Page page, string NumberJoinChasse)
        {
            _page = page;
            initCommands();
            NumberChasse = NumberJoinChasse;
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            if (firebase
                .Child(NumberChasse)
                .Child("EtatDeChasse").ToString().Equals("Arret")){
                _page.Navigation.PushModalAsync(new PausePage());
            }

            /*if (!DependencyService.Get<IUserService>().IsLogged())
            {
                var loginPage = new LoginPage();
                var loginVM = (LoginVM)loginPage.BindingContext;
                loginVM.CallBack = OnNewUserCallback;

                _page.Navigation.PushModalAsync(loginPage);
            }*/
        }

        private void initCommands(){
            sanglierVueCommand = new Command(VueSanglier);
            renardVueCommand = new Command(VueRenard);
            chevreuilVueCommand = new Command(VueChevreuil);
            chevreuilTuerCommand = new Command(TuerChevreuil);
            renardTuerCommand = new Command(TuerRenard);
            sanglierTuerCommand = new Command(TuerSanglier);
            //affichageCommand = new Command(Affichage);
        }

        public async void VueSanglier(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");

            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int number = int.Parse(list[0].Sanglier) + 1;
            number.ToString();

            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }


        public async void VueRenard(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int number = int.Parse(list[0].Renard) + 1;
            number.ToString();
            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }


        public async void VueChevreuil(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int number = int.Parse(list[0].Chevreuil) + 1;
            number.ToString();
            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }





        public async void TuerChevreuil(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int numberVue = int.Parse(list[0].Chevreuil) - 1;
            numberVue.ToString();

            var list2 = (await firebase
                         .Child(NumberChasse)
                        .Child("ChasseTuer")
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

                       ).ToList();
            int numberTuer = int.Parse(list2[0].Chevreuil) + 1;
            numberTuer.ToString();

            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);

            await firebase
                .Child(NumberChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberTuer);
        }


        public async void TuerRenard(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int numberVue = int.Parse(list[0].Renard) - 1;
            numberVue.ToString();

            var list2 = (await firebase
                        .Child(NumberChasse)
                        .Child("ChasseTuer")
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

                       ).ToList();
            int numberTuer = int.Parse(list2[0].Renard) + 1;
            numberTuer.ToString();

            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);

            await firebase
                .Child(NumberChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberTuer);
        }
        public async void TuerSanglier(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child(NumberChasse)
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

                       ).ToList();
            int numberVue = int.Parse(list[0].Sanglier) - 1;
            numberVue.ToString();

            var list2 = (await firebase
                        .Child(NumberChasse)
                        .Child("ChasseTuer")
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

                       ).ToList();
            int numberTuer = int.Parse(list2[0].Sanglier) + 1;
            numberTuer.ToString();

            await firebase
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);

            await firebase
                .Child(NumberChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberTuer);
        }

        public async Task<List<Vue>>Afficher(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child("Chasse")
                        .Child("ChasseVue")
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

                       ).ToList();
            return list;
        }
    }
}

