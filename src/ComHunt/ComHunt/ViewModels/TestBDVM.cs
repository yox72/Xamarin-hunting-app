using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Services;
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
        public string etatChasse;

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
            getEtat();
            init();
        }

        public void init(){
            /*if (etatChasse == "Arret")
            {
                _page.Navigation.PushModalAsync(new PausePage());
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

        public async void getEtat()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var etat = (await firebase
                              .Child(NumberChasse)
                              .OnceAsync<Chasse>())
                              .FirstOrDefault().Object.etat;
            if (etat.Equals("Arret"))
            {
                await _page.Navigation.PushModalAsync(new PausePage(NumberChasse));
            }
        }

        public async void VueSanglier(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");

            var nbSangl = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Sanglier;
            int number = int.Parse(nbSangl) + 1;
            number.ToString();

            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }


        public async void VueRenard(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbRen = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Renard;
            int number = int.Parse(nbRen) + 1;
            number.ToString();
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }


        public async void VueChevreuil(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbChev = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Chevreuil;
            int number = int.Parse(nbChev) + 1;
            string nombre = number.ToString();
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(number);
        }





        public async void TuerChevreuil(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbChev = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Chevreuil;
            int numberVue = int.Parse(nbChev) - 1;
            numberVue.ToString();

            var nbChevT = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseTuer")
                              .OnceAsync<Tuer>())
                              .FirstOrDefault().Object.Chevreuil;
            int numberTuer = int.Parse(nbChevT) + 1;
            numberTuer.ToString();
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Chevreuil")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberTuer);
        }


        public async void TuerRenard(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbRen = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Renard;
            int numberVue = int.Parse(nbRen) - 1;
            numberVue.ToString();

            var nbRenT = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseTuer")
                              .OnceAsync<Tuer>())
                              .FirstOrDefault().Object.Renard;
            int numberTuer = int.Parse(nbRenT) + 1;
            numberTuer.ToString();

            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);

            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseTuer")
                .Child("Tuer")
                .Child("Renard")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberTuer);
        }
        public async void TuerSanglier(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbSang = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseVue")
                              .OnceAsync<Vue>())
                              .FirstOrDefault().Object.Sanglier;
            int numberVue = int.Parse(nbSang) - 1;
            numberVue.ToString();

            var nbSangT = (await firebase
                              .Child(NumberChasse)
                              .Child(NumberChasse)
                              .Child("ChasseTuer")
                              .OnceAsync<Tuer>())
                              .FirstOrDefault().Object.Sanglier;
            int numberTuer = int.Parse(nbSangT) + 1;
            numberTuer.ToString();

            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Sanglier")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(numberVue);

            await firebase
                .Child(NumberChasse)
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

