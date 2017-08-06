using Bwired.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bwired.Portable.ViewModels
{
    public class TakePictureViewModel : BaseViewModel
    {
        public TakePictureViewModel()
        {

        }

        private ImageSource myImageSource;
        public ImageSource MyImageSource
        {
            get
            {
                return myImageSource;
            }
            set
            {
                myImageSource = value;
                OnPropertyChanged("MyImageSource");
            }
        }

        private Command takePictureCommand;
        public Command TakePictureCommand
        {
            get
            {
                return takePictureCommand ?? (takePictureCommand = new Command(async () =>
                {
                    await ExecuteTakePictureCommand();
                }, () =>
                {
                    return !IsBusy;
                }));
            }
        }

        private Command getImageGalleryCommand;
        public Command GetImageGalleryCommand
        {
            get
            {
                return getImageGalleryCommand ?? (getImageGalleryCommand = new Command(async () =>
                {
                    await ExecuteGetImageGalleryCommand();
                }, () =>
                {
                    return !IsBusy;
                }));
            }
        }

        public async Task ExecuteGetImageGalleryCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            getImageGalleryCommand.ChangeCanExecute();
            var error = false;

            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    var page = new ContentPage();
                    await page.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                MyImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to Get Picture", "OK");
            }

            IsBusy = false;
            getImageGalleryCommand.ChangeCanExecute();
        }

        public async Task ExecuteTakePictureCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            GetImageGalleryCommand.ChangeCanExecute();
            var error = false;

            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    var page = new ContentPage();
                    await page.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                MyImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to Take Picture", "OK");
            }

            IsBusy = false;
            GetImageGalleryCommand.ChangeCanExecute();

        }
    }
}
