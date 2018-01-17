using ComHunt.ViewModels;
using Xamarin.Forms;

namespace ComHunt.Views
{
    public partial class CreaChassePage : ContentPage
    {
        public CreaChassePage()
        {
            InitializeComponent();
            BindingContext = new CreaChasseVM(this);
        }
    }
}
