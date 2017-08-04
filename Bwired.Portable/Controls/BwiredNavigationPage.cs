using System;
using Xamarin.Forms;

namespace Bwired.Controls
{
    public class BwiredNavigationPage : NavigationPage
    {
        public BwiredNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public BwiredNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#03A9F4");
            BarTextColor = Color.White;
        }
    }
}
