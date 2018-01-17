using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ComHunt.Models;
using ComHunt.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class JoinChasseVM : INotifyPropertyChanged
    {
        private Page _page;
        public event PropertyChangedEventHandler PropertyChanged;

        public string NumeroJoinChasse { get; set; }
        public bool TireurChef { get; set; }

        public ICommand entrerCommand { get; set; }

        public JoinChasseVM(Page page)
        {
            _page = page;
            init();
            initCommand();
        }

        void init(){
            //NumeroJoinChasse = DependencyService.Get<IUserService>().getName();
        }

        void initCommand(){
            entrerCommand = new Command(Entrer);
        }


        public async void Entrer(){
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var chasseNom = (await firebase
                           .Child(NumeroJoinChasse)
                           .OnceAsync<Chasse>())
                .FirstOrDefault().Object.name;
            try
            {
                if (chasseNom.Equals(NumeroJoinChasse))
                {
                    //Lecture du nombre de chasseurs actifs
                    var listChasseursActifs = (await firebase
                                .Child(NumeroJoinChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object.nombreChasseursActifs;
                    int newnbChasseursActifs = int.Parse(listChasseursActifs) + 1;
                    await firebase  //Nb chasseurs actifs
                        .Child(NumeroJoinChasse)
                        .Child(NumeroJoinChasse)
                      .Child("nombreChasseursActifs")
                        .PutAsync(newnbChasseursActifs);
                    await _page.Navigation.PushAsync(new TestBDPage(NumeroJoinChasse));//Ouvirir vue JoinChasse
                }
            }
            catch (Exception)
            { 
                await _page.DisplayAlert("Alerte", "Nom de chasse incorrect", "OK");//Afficher erreur }     
            }
        }
    }
}

