using Bwired.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bwired.Portable.ViewModels
{
    public class ContactUsViewModel : BaseViewModel
    {
        public ContactUsViewModel()
        {
            Title = "Contact Us!";
            Icon = "slideout.png";
        }

        private Command dialNumberCommand;
        public Command DialNumberCommand
        {
            get
            {
                return dialNumberCommand ??
                  (dialNumberCommand = new Command(async () =>
                  {
                      await ExecuteDialNumberCommand();
                  }, () =>
                  {
                      return !IsBusy;
                  }));
            }
        }

        public async Task ExecuteDialNumberCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            DialNumberCommand.ChangeCanExecute();
            var error = false;
            try
            {
                //TODO: Dial number
               

               

            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to Dial Number", "OK");
            }

            IsBusy = false;
            DialNumberCommand.ChangeCanExecute();
        }

    }
}
