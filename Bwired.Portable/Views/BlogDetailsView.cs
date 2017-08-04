using Bwired.Models;
using Plugin.Share;
using Plugin.TextToSpeech;
using System;
using Xamarin.Forms;

namespace Bwired.Views
{
    public class BlogDetailsView : BaseView
    {
        public BlogDetailsView(FeedItem item)
        {
            BindingContext = item;
            var webView = new WebView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            webView.Source = new HtmlWebViewSource
            {
                Html = item.Description
            };
            Content = new StackLayout
            {
                Children =
        {
          webView
        }
            };
            var share = new ToolbarItem
            {
                Icon = "ic_share.png",
                Text = "Share",
                Command = new Command(() => CrossShare.Current
                  .Share(new Plugin.Share.Abstractions.ShareMessage
                  {
                      Text = "Be sure to read @bwiredstudios's " + item.Title + " " + item.Link,
                      Title = "Share",
                      Url = item.Link
                  }))
            };

            var speakButton = new ToolbarItem
            {
                Icon = "ic_play.png",
                Text = "Listen",
                

            };

            speakButton.Clicked += async (sender, args) =>
            {
                await CrossTextToSpeech.Current.Speak(item.Description);
                return;
            };

            ToolbarItems.Add(share);
            ToolbarItems.Add(speakButton);

        }
    }
}

