using Bwired.ViewModels;
using Plugin.Messaging;
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

        private Command sendEmailCommand;
        public Command SendEmailCommand
        {
            get
            {
                return sendEmailCommand ??
                    (sendEmailCommand = new Command(async () =>
                    {
                        await ExecuteSendEmailCommand();
                    }, () =>
                    {
                        return !IsBusy;
                    }));
            }
        }

        public async Task ExecuteSendEmailCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            SendEmailCommand.ChangeCanExecute();
            var error = false;
            try
            {
                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {
                    //TODO: Manage border cases

                    //Just a simple mail

                    //You can change the destination here if info@bwired.ca is not good at all for testing, im sorry :(
                    //PD: Oops, also check in spam folder :P 
                    emailMessenger.SendEmail("m_contreras@outlook.cl", "Email de prueba Xamarin Forms", "Hello there, if you received this message means my all night work worth it!!");

                    var page = new ContentPage();
                    await page.DisplayAlert("Yay!!", "We sent your message", "OK");
                }
                else
                {
                    var page = new ContentPage();
                    await page.DisplayAlert("Oops", "Try to test on real device", "OK");
                }

            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to Send Message", "OK");
            }

            IsBusy = false;
            SendEmailCommand.ChangeCanExecute();
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
                //TODO: manage conections
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("(519)744-7000‬");
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
