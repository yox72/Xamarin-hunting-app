using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class TestBDVM : INotifyPropertyChanged
    {
        private Page _page;
        public event PropertyChangedEventHandler PropertyChanged;

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
            /*var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var observable = firebase
                .Child(NumberChasse)
                .AsObservable<Chasse>()
                .Subscribe(
                    c =>
                    {
                        getEtat(c.Object);
                        SendBTData(c.Object);
                    });*/
            getEtat2();
        }

        private void initCommands()
        {
            sanglierVueCommand = new Command(VueSanglier);
            renardVueCommand = new Command(VueRenard);
            chevreuilVueCommand = new Command(VueChevreuil);
            chevreuilTuerCommand = new Command(TuerChevreuil);
            renardTuerCommand = new Command(TuerRenard);
            sanglierTuerCommand = new Command(TuerSanglier);
            //affichageCommand = new Command(Affichage);
        }

        /*public async void getEtat(Chasse c)
        {
            if (c.etat == "Arret")
            {
                await _page.Navigation.PushModalAsync(new PausePage(NumberChasse));
            }
        }*/

        public async void getEtat2()
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

        public async void SendBTData(Chasse c)
        {
            //send c.ChevreuilTuer;
            //send c.ChevreuilVue;
            //send c.RenardTuer;
            //send c.RenardVue;
            //...
        }

        public async void VueSanglier()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");

            var nbSangl = (await firebase
                              .Child(NumberChasse)
                              .OnceAsync<Chasse>())
                              .FirstOrDefault().Object.SanglierVue;
            int number = int.Parse(nbSangl) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("SanglierVue")
                .PutAsync(number);
        }


        public async void VueRenard()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbRen = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.RenardVue;
            int number = int.Parse(nbRen) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("RenardVue")
                .PutAsync(number);
        }


        public async void VueChevreuil()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbChev = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilVue;
            int number = int.Parse(nbChev) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChevreuilVue")
                .PutAsync(number);
        }





        public async void TuerChevreuil()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbChev = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilVue;
            int numberVue = int.Parse(nbChev) - 1;
            var nbChevT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilTuer;
            int numberTuer = int.Parse(nbChevT) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChevreuilVue")
                .PutAsync(numberVue);
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChevreuilTuer")
                .PutAsync(numberTuer);
        }


        public async void TuerRenard()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbRen = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.RenardVue;
            int numberVue = int.Parse(nbRen) - 1;
            var nbRenT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.RenardTuer;
            int numberTuer = int.Parse(nbRenT) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("RenardVue")
                .PutAsync(numberVue);
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("RenardTuer")
                .PutAsync(numberTuer);
        }
        public async void TuerSanglier()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbSang = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.SanglierVue;
            int numberVue = int.Parse(nbSang) - 1;
            var nbSangT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.SanglierTuer;
            int numberTuer = int.Parse(nbSangT) + 1;
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("SanglierVue")
                .PutAsync(numberVue);
            await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("SanglierTuer")
                .PutAsync(numberTuer);
        }

        public async Task<List<Vue>> Afficher()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var list = (await firebase
                        .Child("Chasse")
                        .Child("ChasseVue")
                        .OrderByKey()
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

