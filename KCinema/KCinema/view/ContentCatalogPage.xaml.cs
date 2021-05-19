using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.repo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentCatalogPage : ContentPage
    {
        private ObservableCollection<Content> ContentCatalog { get; set; }
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        public ContentCatalogPage()
        {
            InitializeComponent();
            BindingContext = this;
            //ContentList.ItemsSource = ContentCatalog;
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void OnSignOut(object sender, EventArgs e)
        {
            if (_firebaseAuthentication.SignOut())
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnAddContent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}