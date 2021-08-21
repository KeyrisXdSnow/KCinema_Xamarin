using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.service;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage
    {
        private static List<Content> ContentCatalog { get; set; }

        public MapPage(List<Content> contentCatalog)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            
            ContentCatalog = contentCatalog;

            LoadMarkers();
            Map.PinClicked += OnPinClicked;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("NeedUpdate"))
            {
                ContentCatalog.Clear();
                await LoadContent();
                await UpdatePins();
            }
            await LoadMarkers();
        }


        private static async Task LoadContent()
        {
            ContentCatalog = new List<Content>();
            await Load();
        }
        
        private static async Task Load()
        {
            foreach (var content in await new ContentRepo().GetAllContent())
            {
                ContentCatalog.Add(content);
            }
        }

        private async Task LoadMarkers()
        {
            var i = 0;
            foreach (var content in ContentCatalog.Where(content => content.Country != null))
            {
                var location = await MapService.GetPositionByAddress(content.Country);

                if (location == null) continue;

                var pin = new Pin()
                {
                    Type = PinType.Place,
                    Label = content.Name,
                    Tag = i,
                    Address = content.Country,
                    Position = new Position(location.Latitude, location.Longitude)
                };

                Map.Pins.Add(pin);
                i++;
            }
        }

        private async Task UpdatePins()
        {
            foreach (var pin in Map.Pins)
            {
                var content = ContentCatalog[(int) pin.Tag];
                
                if (content.Country == null) continue;
                
                var location = await MapService.GetPositionByAddress(content.Country);

                if (location == null) continue;

                var newPos = new Position(location.Latitude, location.Longitude);
                if (!pin.Position.Equals(newPos))
                {
                    pin.Position = newPos;
                }
            }
        }

        private async void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            var pin = e.Pin;
            var index = (int) pin.Tag;

            var content = ContentCatalog.Find(content => content.Name == pin.Label);
            if (content != null)
            {
                await Navigation.PushAsync(new ContentEntityPage(content));
            }
        }
    }
}