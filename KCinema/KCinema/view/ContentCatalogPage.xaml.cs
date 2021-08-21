using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.repo;
using KCinema.service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentCatalogPage
    {
        private ObservableCollection<Content> FilterContentCatalog { get; set; }
        private List<Content> ContentCatalogList { get; set; }

        private IFirebaseAuthentication _firebaseAuthentication;

        public ContentCatalogPage()
        {
            InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasBackButton(this, false);
            InitView();

            Application.Current.Properties["NeedUpdate"] = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Application.Current.Properties.ContainsKey("NeedUpdate")) return;

            FilterContentCatalog.Clear();
            ContentCatalogList.Clear();


            await LoadContent();
            Application.Current.Properties.Remove("NeedUpdate");
        }

        private void InitView()
        {
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();

            ContentCatalogList = new List<Content>();
            FilterContentCatalog = new ObservableCollection<Content>();
        }

        private async Task LoadContent()
        {
            await Load();

            if (ContentTypePicker.SelectedIndex == -1)
            {
                ContentTypePicker.SelectedIndex = 0;
            }

            FilterContent(ContentTypePicker.SelectedIndex);
        }

        private async Task Load()
        {
            foreach (var content in await new ContentRepo().GetAllContent())
            {
                ContentCatalogList.Add(content);
            }
        }

        private async void OnSignOut(object sender, EventArgs e)
        {
            if (_firebaseAuthentication.SignOut())
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnAddContent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentRedactor(), true);
        }

        private async void OnMap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage(FilterContentCatalog.ToList()));
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var content = ((TappedEventArgs) e).Parameter as Content;
            await Navigation.PushAsync(new ContentEntityPage(content));
        }

        private async void ContentType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FilterContent(((Picker) sender).SelectedIndex);
        }

        private void FilterContent(int itemIndex)
        {
            ContentType? type = itemIndex switch
            {
                1 => ContentType.Film,
                2 => ContentType.Series,
                3 => ContentType.Cartoon,
                4 => ContentType.AnimatedSeries,
                5 => ContentType.Anime,
                _ => null
            };

            if (type != null)
            {
                var filterContent = ContentCatalogList.Where(content => content.Type == type).ToList();
                FilterContentCatalog = new ObservableCollection<Content>(filterContent);
            }
            else
            {
                FilterContentCatalog = new ObservableCollection<Content>(ContentCatalogList);
            }
            ContentListView.ItemsSource = FilterContentCatalog;
        }
    }
}