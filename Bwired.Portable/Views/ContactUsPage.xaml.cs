using Bwired.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bwired.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsPage : ContentPage
    {
        private ContactUsViewModel ViewModel
        {
            get { return BindingContext as ContactUsViewModel; }
        }
        public ContactUsPage()
        {
            InitializeComponent();
            BindingContext = new ContactUsViewModel();
        }
    }
}