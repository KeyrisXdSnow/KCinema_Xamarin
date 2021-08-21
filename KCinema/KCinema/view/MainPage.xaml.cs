using System;
using KCinema.resource.localization;
using KCinema.repo;
using Xamarin.Forms;

using Xamarin.CommunityToolkit.Helpers;


namespace KCinema.view
{
    public partial class MainPage
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        
        
        public MainPage()
        {
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnUserSignIn();
        }

        private async void OnUserSignIn()
        {
            if (_firebaseAuthentication.IsSignIn())
            {
                await Navigation.PushAsync(new ContentCatalogPage());
            }
        }
        
        private async void signUpButton_Clicked(object sender, EventArgs e)
        {
            var token = await _firebaseAuthentication.SignUpWithEmailAndPassword(UserEmail.Text, UserPassword.Text);
            if (!token.Equals(string.Empty))
            {
                await Navigation.PushAsync(new ContentCatalogPage());
            }
        }

        private async  void loginButton_Clicked(object sender, EventArgs e)
        {
            var token = await _firebaseAuthentication.SignInWithEmailAndPassword(UserEmail.Text, UserPassword.Text);
            if (token != string.Empty)
            {
                await Navigation.PushAsync(new ContentCatalogPage());
            }
        }
    }
}