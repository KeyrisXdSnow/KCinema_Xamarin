using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.service;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;


namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentEntityPage
    {
        private new Content Content { get; }
        private string[] _images = new string[0];

        public string[] Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        public ContentEntityPage(Content content)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = this;

            Content = content;
            if (Content.Gallery != null) _images = Content.Gallery.ToArray();

            // if (Application.Current.Properties.ContainsKey("NeedUpdate"))
            // {
            //     Application.Current.Properties.Remove("NeedUpdate");
            // }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitView();
        }

        private async Task InitView()
        {
            Name.Text = Content.Name;
            Genres.Text = Content.Genres;
            Rating.Text = Content.Rating.ToString(CultureInfo.InvariantCulture);
            Country.Text = Content.Country;
            WatchTime.Text = Content.WatchTime.ToString();
            ReleaseDate.Text = Content.ReleaseDate;
            Description.Text = Content.Description;

            
            if (!string.IsNullOrEmpty(Content.TrailerUrl))
            {
                GetVideoContent();
            }

            if (Content.PosterUrl != null)
            {
                Poster.Source = Content.PosterUrl;
            }

            if (Content.Gallery != null)
            {
                ImagesView.ItemsSource = _images;
            }
        }

        private async Task GetVideoContent()
        {
            //MyActivityIndicator.IsVisible = true;
            var youtube = new YoutubeClient();
        
            //Now it's time to get stream :
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(Content.TrailerUrl);
        
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestBitrate();
            {
                // // Get the actual stream
                // var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
        
                VideoPlayer.Source = streamInfo.Url;
                VideoPlayer.AutoPlay = false;
            }
        }


        private async void OnContentDelete(object sender, EventArgs e)
        {
            var token = DeleteContent();
            if (token.ToString().Equals("")) return;

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PushAsync(new ContentCatalogPage());
        }

        private async Task DeleteContent()
        {
            Application.Current.Properties["NeedUpdate"] = true;
            await new ContentRepo().DeleteContent(Content);
        }

        private async void OnEditContent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentRedactor(Content), true);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
        }

        private void MediaElement_MediaOpened(object sender, EventArgs e)
        {
            
        }
    }
}