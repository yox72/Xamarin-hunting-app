using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Plugin.BLE.Abstractions.Contracts;
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
        public ICommand renardToucherCommand { get; set; }
        public ICommand sanglierToucherCommand { get; set; }
        public ICommand chevreuilToucherCommand { get; set; }
        public ICommand renardTuerCommand { get; set; }
        public ICommand sanglierTuerCommand { get; set; }
        public ICommand chevreuilTuerCommand { get; set; }
        public ICommand affichageCommand { get; set; }

        public TestBDVM(Page page, string NumberJoinChasse, IDevice device)
        {
            _page = page;
            initCommands();
            NumberChasse = NumberJoinChasse;
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var observable = firebase
                .Child(NumberChasse)
                .AsObservable<Chasse>()
                .Subscribe(
                    c =>
                    {
                        //getEtat(c.Object);
                        SendBTData(c.Object, device);
                    });
            //getEtat2();
        }

        private void initCommands()
        {
            sanglierVueCommand = new Command(VueSanglier);
            renardVueCommand = new Command(VueRenard);
            chevreuilVueCommand = new Command(VueChevreuil);
            chevreuilTuerCommand = new Command(TuerChevreuil);
            renardTuerCommand = new Command(TuerRenard);
            sanglierTuerCommand = new Command(TuerSanglier);
            sanglierToucherCommand = new Command(ToucherSanglier);
            chevreuilToucherCommand = new Command(ToucherChevreuil);
            renardToucherCommand = new Command(ToucherRenard);
            //affichageCommand = new Command(Affichage);
        }

        public async void getEtat(Chasse c)
        {
            if (c.etat == "Arret")
            {
                await _page.Navigation.PushModalAsync(new PausePage(NumberChasse));
            }
        }

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

        IList<IService> services;
        IService service;
        IList<ICharacteristic> characteristics;
        ICharacteristic characteristic;
        //ObservableCollection<IDevice> deviceList;
        //IDevice device;
        public async void SendBTData(Chasse c, IDevice device)
        {
            int chassetat = 0;
            if(c.etat == "Actif"){
                chassetat = 1;
            }
            if(c.etat == "Pause"){
                chassetat = 2;
            }
            if(c.etat == "Arret"){
                chassetat = 3;
            }
            service = await device.GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
            string data = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9};",chassetat, c.SanglierVue, c.SanglierToucher, c.SanglierTuer, c.ChevreuilVue, c.ChevreuilToucher, c.ChevreuilTuer, c.RenardVue, c.RenardToucher, c.RenardTuer);
            byte[] byteText = Encoding.UTF8.GetBytes(data);
            await characteristic.WriteAsync(byteText);
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

        public async void ToucherRenard()
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
                .Child("RenardToucher")
                .PutAsync(number);
        }

        public async void ToucherSanglier()
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
                .Child("SanglierToucher")
                .PutAsync(number);
        }

        public async void ToucherChevreuil()
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
                .Child("ChevreuilToucher")
                .PutAsync(number);
        }



        public async void TuerChevreuil()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var nbChev = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilVue;
            var nbToucher = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilToucher;
            if (int.Parse(nbToucher) > 0)
            {
                int numberToucher = int.Parse(nbToucher) - 1;
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChevreuilToucher")
                .PutAsync(numberToucher);
            }
            else
            {
                int numberVue = int.Parse(nbChev) - 1;
                if (numberVue <= 0) { numberVue = 0; }
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("ChevreuilVue")
                .PutAsync(numberVue); 
            }
            var nbChevT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.ChevreuilTuer;
            int numberTuer = int.Parse(nbChevT) + 1;
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
            var nbToucher = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.RenardToucher;
            if (int.Parse(nbToucher) > 0)
            {
                int numberToucher = int.Parse(nbToucher) - 1;
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("RenardToucher")
                .PutAsync(numberToucher);
            }
            else
            {
                int numberVue = int.Parse(nbRen) - 1;
                if (numberVue <= 0) { numberVue = 0; }
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("RenardVue")
                .PutAsync(numberVue);
            }
            var nbRenT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.RenardTuer;
            int numberTuer = int.Parse(nbRenT) + 1;
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
            var nbToucher = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.SanglierToucher;
            if (int.Parse(nbToucher) > 0)
            {
                int numberToucher = int.Parse(nbToucher) - 1;
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("SanglierToucher")
                .PutAsync(numberToucher);
            }
            else
            {
                int numberVue = int.Parse(nbSang) - 1;
                if (numberVue <= 0) { numberVue = 0; }
                await firebase
                .Child(NumberChasse)
                .Child(NumberChasse)
                .Child("SanglierVue")
                .PutAsync(numberVue);
            }
            var nbSangT = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.SanglierTuer;
            int numberTuer = int.Parse(nbSangT) + 1;
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

