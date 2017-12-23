using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComHunt.ViewModels
{
    public class TestBDVM : ContentView
    {
        private Page _page;

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
            vueRenardCommand = new Command(VueRenard);
            vueChevreuilCommand = new Command(VueChevreuil);
        }

        public void VueSanglier(){}
        public void VueRenard(){}
        public void VueChevreuil(){}
    }
}

