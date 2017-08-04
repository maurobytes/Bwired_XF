using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Share;
using Bwired.Helpers;

namespace Bwired.Views
{
    public partial class AboutPage : ContentPage
    {

        void OpenBrowser(string url)
        {
            CrossShare.Current.OpenBrowser(url, new Plugin.Share.Abstractions.BrowserOptions
            {
                ChromeShowTitle = true,
                ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
                UseSafariReaderMode = true,
                UseSafariWebViewController = true
            });
        }
        public AboutPage()
        {
            InitializeComponent();

            twitter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    //try to launch twitter or tweetbot app, else launch browser
                    var launch = DependencyService.Get<ILaunchTwitter>();
                    if (launch == null || !launch.OpenUserName("bwiredstudios"))
                        OpenBrowser("http://m.twitter.com/bwiredstudios");
                })
            });

            facebook.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OpenBrowser("https://m.facebook.com/bwired.ca"))
            });


            instagram.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OpenBrowser("https://www.instagram.com/bwiredstudios"))
            });



        }
    }
}
