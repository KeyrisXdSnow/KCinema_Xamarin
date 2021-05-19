using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Content> ContentCatalog { get; set; }
        
        
        
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        public ContentCatalogPage()
        {
            InitializeComponent();
            BindingContext = this;
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();
            ContentCatalog = new ObservableCollection<Content>();
            
            ContentListView.ItemsSource = ContentCatalog;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadContent();

        }

        private async Task LoadContent()
        {
            foreach (var content in await new ContentRepo().GetAllContent())
            {
                ContentCatalog.Add(content);
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
            await Navigation.PushAsync(new ContentRedactor(true),true);
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ContentEntityPage(ContentCatalog[e.ItemIndex]));
        }
    }
}