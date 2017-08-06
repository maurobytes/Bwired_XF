using Bwired.Models;
using Bwired.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Bwired.Views
{
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {

            this.root = root;
            InitializeComponent();
            if (!App.IsWindows10)
            {
                BackgroundColor = Color.FromHex("#03A9F4");
                ListViewMenu.BackgroundColor = Color.FromHex("#F5F5F5");
            }
            BindingContext = new BaseViewModel
            {
                Title = "Bwired.Forms",
                Subtitle = "Bwired.Forms",
                Icon = "slideout.png"
            };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem { Title = "About", MenuType = MenuType.About, Icon ="about.png" },
                    new HomeMenuItem { Title = "Blog", MenuType = MenuType.Blog, Icon = "blog.png" },
                    new HomeMenuItem { Title = "Twitter", MenuType = MenuType.Twitter, Icon = "twitternav.png" },
                    new HomeMenuItem { Title = "Contact Us", MenuType = MenuType.Contact, Icon = "twitternav.png" },
                    new HomeMenuItem { Title = "Take Picture", MenuType = MenuType.TakePicture, Icon = "twitternav.png" }

                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;

                await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
            };
        }
    }
}

