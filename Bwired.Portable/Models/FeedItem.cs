using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Bwired.Models
{
    public class FeedItem : INotifyPropertyChanged
    {
        public string Description { get; set; }
        public string Link { get; set; }
        private string publishDate;
        public string PublishDate
        {
            get { return publishDate; }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                    publishDate = time.ToLocalTime().ToString("D");
                else
                    publishDate = value;
            }
        }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public int Id { get; set; }
        public string CommentCount { get; set; }
        public string Category { get; set; }

        public string Mp3Url { get; set; }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

            }
        }

        private string caption;

        public string Caption
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(caption))
                    return caption;


                //eliminamos tags en html
                caption = Regex.Replace(Description, "<[^>]*>", string.Empty);


                //eliminabos espacios en blanco
                caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

                caption = caption.Substring(0, caption.Length < 200 ? caption.Length : 200).Trim() + "...";
                return caption;
            }
        }

        public string Length { get; set; }

        private bool showImage = true;

        public bool ShowImage
        {
            get { return showImage; }
            set { showImage = value; }
        }

        private string image = @"https://secure.gravatar.com/avatar/70148d964bb389d42547834e1062c886?s=60&r=x&d=http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";

        /// <summary>
        /// Cuando seteamos la imagen, marcamos true en showImage
        /// </summary>
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                showImage = true;
            }

        }

        private string firstImage;
        public string FirstImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(firstImage))
                    return firstImage;


                var regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);
                var matches = regx.Matches(Description);

                if (matches.Count == 0)
                    firstImage = TwittHead;
                else
                    firstImage = matches[0].Value;

                return firstImage;
            }
        }

        public ImageSource FirstImageSource
        {
            get
            {
                var image = FirstImage;
                return UriImageSource.FromUri(new Uri(image));
            }
        }

        public string TwittHead { get { return "https://bwired.ca/wp-content/uploads/2015/03/dan-2.jpg"; } }

        private decimal progress = 0.0M;
        public decimal Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged("Progress"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
