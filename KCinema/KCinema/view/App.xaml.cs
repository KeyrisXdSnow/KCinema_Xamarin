using KCinema.resource;
using KCinema.resource.localization;
using KCinema.service;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("Stick-Regular.ttf", Alias = "Stick")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "Open Sans")]

namespace KCinema.view
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            InitializeView();
            
            LocalizationResourceManager.Current.Init(AppLang.ResourceManager);

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color) Current.Resources["TopBarColor"],
                BarTextColor = (Color) Current.Resources["TopBarColor"]
            };
        }


        protected override async void OnSleep()
        {
            base.OnSleep();
            await SettingsService.SaveSettings();
        }
        
        private static void InitializeView()
        {

                        
            if (Current.Properties.ContainsKey("LightMode"))
            {
                if ((string) Current.Properties["LightMode"] == "Light")
                {
                    SettingsService.SetLightTheme(new LightTheme(), OSAppTheme.Light);
                }
                else
                {
                    SettingsService.SetLightTheme(new DarkTheme(), OSAppTheme.Dark);
                }
            }
            
            if (Current.Properties.ContainsKey("Lang"))
            {
                SettingsService.SetLanguage(Current.Properties["Lang"] as string);
            }
            
            if (Current.Properties.ContainsKey("FontSize"))
            {
                SettingsService.SetFontSize((int) Current.Properties["FontSize"]);
            }
            
            if (Current.Properties.ContainsKey("FontFamily"))
            {
                SettingsService.SetFontFamily(Current.Properties["FontFamily"] as string);
            }
        }
    }
}