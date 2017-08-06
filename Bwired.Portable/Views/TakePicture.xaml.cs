using Bwired.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bwired.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePicture : ContentPage
    {
        private TakePictureViewModel ViewModel
        {
            get { return BindingContext as TakePictureViewModel; }
        }
        public TakePicture()
        {
            InitializeComponent();
            BindingContext = new TakePictureViewModel();
        }
    }
}