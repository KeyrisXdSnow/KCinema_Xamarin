using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentRedactor
    {
        private readonly bool _isAdd;

        public ContentRedactor(bool isAdd)
        {
            InitializeComponent();
            _isAdd = isAdd;
        }

        private async void OnFinish(object sender, EventArgs e)
        {
            if (_isAdd) // add content to bd
            {
                var token = new ContentRepo().SaveContent(CreateContent());

                if (token.ToString().Equals(string.Empty)) return;
                
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PushAsync(new ContentCatalogPage());
            }
            else // update content
            {
            }
        }

        private Content CreateContent()
        {
            const ContentType type = ContentType.Film;
            var content = new Content(name: Name.Text, type: type);

            if (Genres.Text != null && !Genres.Text.Equals(string.Empty))
            {
                content.Genres = Genres.Text;
            }

            if (Country.Text != null && !Country.Text.Equals(string.Empty))
            {
                content.Country = Country.Text;
            }

            if (WatchTime.Text != null && !WatchTime.Text.Equals(string.Empty))
            {
                try
                {
                    content.WatchTime = Int32.Parse(WatchTime.Text);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (ReleaseDate.Text != null && !ReleaseDate.Text.Equals(string.Empty))
            {
                content.ReleaseDate = ReleaseDate.Text;
            }

            if (Poster.Text != null && !Poster.Text.Equals(string.Empty))
            {
                content.PosterUrl = Poster.Text;
            }

            if (Trailer.Text != null && !Trailer.Text.Equals(string.Empty))
            {
                content.TrailerUrl = Trailer.Text;
            }

            if (Description.Text != null && !Description.Text.Equals(string.Empty))
            {
                content.Description = Description.Text;
            }

            if (Gallery.Text != null && !Gallery.Text.Equals(string.Empty))
            {
                var urls = Gallery.Text.Split(' ', ',');
                content.Gallery = new List<string>(urls);
            }

            return content;
        }
        
        
        private Content UpdateContent()
        {
            const ContentType type = ContentType.Film;
            var content = new Content(name: Name.Text, type: type);

            if (Genres.Text != null && !Genres.Text.Equals(string.Empty))
            {
                content.Genres = Genres.Text;
            }

            if (Country.Text != null && !Country.Text.Equals(string.Empty))
            {
                content.Country = Country.Text;
            }

            if (WatchTime.Text != null && !WatchTime.Text.Equals(string.Empty))
            {
                try
                {
                    content.WatchTime = Int32.Parse(WatchTime.Text);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (ReleaseDate.Text != null && !ReleaseDate.Text.Equals(string.Empty))
            {
                content.ReleaseDate = ReleaseDate.Text;
            }

            if (Poster.Text != null && !Poster.Text.Equals(string.Empty))
            {
                content.PosterUrl = Poster.Text;
            }

            if (Trailer.Text != null && !Trailer.Text.Equals(string.Empty))
            {
                content.TrailerUrl = Trailer.Text;
            }

            if (Description.Text != null && !Description.Text.Equals(string.Empty))
            {
                content.Description = Description.Text;
            }

            if (Gallery.Text != null && !Gallery.Text.Equals(string.Empty))
            {
                var urls = Gallery.Text.Split(' ', ',');
                content.Gallery = new List<string>(urls);
            }

            return content;
        }
    }
}