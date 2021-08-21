using System;
using System.Collections.Generic;
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
        private readonly bool _isAdd = false;
        private readonly Content _content;

        public ContentRedactor()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            _isAdd = true;
        }
        
        public ContentRedactor(Content content)
        {
            InitializeComponent();
            _content = content;
            InitializeView();
        }

        private void InitializeView()
        {
            if (_content == null) return;
            
            if (_content.Name != null) Name.Text = _content.Name;
            ContentTypePicker.SelectedIndex = _content.Type switch
            {
                ContentType.Film => (int) ContentType.Film,
                ContentType.Series => (int) ContentType.Series,
                ContentType.Cartoon => (int) ContentType.Cartoon,
                ContentType.AnimatedSeries => (int) ContentType.AnimatedSeries,
                ContentType.Anime => (int) ContentType.Anime,
                _ => ContentTypePicker.SelectedIndex
            };
            if (_content.Rating != 0)
            {
                Rating.Value = _content.Rating;
                RatingValue.Text = _content.Rating.ToString();
            }
            if (_content.Genres != null) Genres.Text = _content.Genres;
            if (_content.Country != null) Country.Text = _content.Country;
            if (_content.WatchTime != 0) WatchTime.Text = _content.WatchTime.ToString();
            if (_content.ReleaseDate != null) ReleaseDate.Text = _content.ReleaseDate;
            if (_content.PosterUrl != null) Poster.Text = _content.PosterUrl;
            if (_content.TrailerUrl != null) Trailer.Text = _content.TrailerUrl;
            if (_content.Description != null) Description.Text = _content.Description;
            if (_content.Gallery != null)
            {
                Gallery.Text = string.Join(",\n", _content.Gallery);
            }
            
        }

        private async void OnFinish(object sender, EventArgs e)
        {
            Task token;
            if (_isAdd) // add content to bd
            {
                token = new ContentRepo().SaveContent(CreateContent());
                
                if (token.ToString().Equals(string.Empty)) return;
               
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Application.Current.Properties["NeedUpdate"] = true;
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PushAsync(new ContentCatalogPage());
                
            }
            else // update content
            {
                var id = _content.Id;
                
                var content = CreateContent();
                content.Id = id;
                
                token = new ContentRepo().UpdateContent(content); 
                
                if (token.ToString().Equals(string.Empty)) return;

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                Application.Current.Properties["NeedUpdate"] = true;
                await Navigation.PushAsync(new ContentEntityPage(content), true);
                
            }
            
        }

        private Content CreateContent()
        {
            ContentType? type = ContentTypePicker.SelectedIndex switch
            {
                0 => ContentType.Film,
                1 => ContentType.Series,
                2 => ContentType.Cartoon,
                3 => ContentType.AnimatedSeries,
                4 => ContentType.Anime,
                _ => null
            };
            var content = new Content(name: Name.Text, type: type.Value);

            if (RatingValue.Text != null)
            {
                try
                {
                    content.Rating = float.Parse(RatingValue.Text);
                }
                catch (Exception)
                {
                    // ignored
                }
                
            } 

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
                var urls = Gallery.Text.Split(new[] {' ', ',','\n'},StringSplitOptions.RemoveEmptyEntries);
                content.Gallery = new List<string>(urls);
            }

            return content;
        }

        private void OnRatingValueChange(object sender, ValueChangedEventArgs e)
        {
            RatingValue.Text =  e.NewValue.ToString();
        }

        private void ContentType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}