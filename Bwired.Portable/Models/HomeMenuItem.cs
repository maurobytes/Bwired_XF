using System;

namespace Bwired.Models
{
    public enum MenuType
    {
        About,
        Blog,
        Twitter,
        Contact,
        TakePicture
    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.About;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}

