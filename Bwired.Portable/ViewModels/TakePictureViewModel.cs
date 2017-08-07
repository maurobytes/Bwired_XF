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

        private Command<string> takePictureCommand;
        public Command<string> TakePictureCommand
        {
            get
            {
                return takePictureCommand ?? (takePictureCommand = new Command<string>(async (p) =>
                {
                    await ExecuteTakePictureCommand(p);
                }, (p) =>
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
            GetImageGalleryCommand.ChangeCanExecute();
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
                {
                    IsBusy = false;
                    GetImageGalleryCommand.ChangeCanExecute();
                    return;
                }

                MyImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
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
            GetImageGalleryCommand.ChangeCanExecute();
        }

        public async Task ExecuteTakePictureCommand(string isSelfie)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            TakePictureCommand.ChangeCanExecute();
            var error = false;

            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    var page = new ContentPage();
                    await page.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var opts = new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                };

                if (isSelfie.Equals("true"))
                {
                    opts.DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(opts);

                if (file == null)
                {
                    IsBusy = false;
                    TakePictureCommand.ChangeCanExecute();
                    return;
                }
                   

                MyImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
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
            TakePictureCommand.ChangeCanExecute();

        }
    }
}
