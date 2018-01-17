using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class WaitUserPage : ContentPage
    {
        public WaitUserPage()
        {
            InitializeComponent();
            BindingContext = new WaitUserVM(this, "");
        }

        public WaitUserPage(string NumberChasse)
        {
            InitializeComponent();
            BindingContext = new WaitUserVM(this, NumberChasse);
        }
    }
}
