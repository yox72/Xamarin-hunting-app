using System;
using System.Windows.Input;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class TestBDVM : ContentView
    {
        private Page _page;

        FirebaseClient client = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/"); 

        public ICommand vueSanglierCommand { get; set; }
        public ICommand vueChevreuilCommand { get; set; }
        public ICommand vueRenardCommand { get; set; }

        public TestBDVM(Page page)
        {
            _page = page;
            initCommands();
        }

        private void initCommands(){
            vueSanglierCommand = new Command(VueSanglier);
            vueRenardCommand = new Command(VueRenardAsync);
            vueChevreuilCommand = new Command(VueChevreuil);
        }

        public void VueSanglier(){}
        public void VueRenardAsync(){}
        /*public async void VueRenardAsync()
        {
            var firebase = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/");

            // add new item to list of data 
            var item = await firebase
                .Child("Chasse")
                .Child("ChasseVue")
                .Child("Vue")
                .Child("Renard")
                .PutAsync(1);

        }*/


        public void VueChevreuil(){}
    }
}

