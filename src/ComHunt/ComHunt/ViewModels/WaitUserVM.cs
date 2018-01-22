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
    public class WaitUserVM : INotifyPropertyChanged
    {
        private Page _page;
        public event PropertyChangedEventHandler PropertyChanged;

        public string NumberChasse { get; set; }
        public string nombreChasseurs { get; set; }
        public string nombreChasseurActifs { get; set; }
        public string nombreChefs { get; set; }
        public string nombreChefsActifs { get; set; }

        public ICommand refreshCommand { get; set; }

        public WaitUserVM(Page page, string NumeroChasse)
        {
            _page = page;
            NumberChasse = NumeroChasse;
            initCommand();
        }

        public void initCommand()
        {
            refreshCommand = new Command(refresh);
        }

        public async void refresh()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");
            var c = (await firebase
                                .Child(NumberChasse)
                                .OnceAsync<Chasse>())
                                .FirstOrDefault().Object;
            
            if (c.nombreChefs == c.nombreChefsActifs && c.nombreChasseurs == c.nombreChasseursActifs)
            {
                await _page.Navigation.PushAsync(new GererChassePage(NumberChasse));//Ouvirir vue GererChasse
            }
        }

    }
}

