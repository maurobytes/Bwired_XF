﻿using System;
using Xamarin.Forms;

namespace Bwired.Views
{
    public class WebsiteView : BaseView
    {
        public WebsiteView(string site, string title)
        {
            this.Title = title;
            var webView = new WebView();
            webView.Source = new UrlWebViewSource
            {
                Url = site
            };
            Content = webView;
        }
    }
}

