using System;
using System.Globalization;
using KCinema.resource;
using KCinema.service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            Resources = DependencyService.Resolve<ResourceDictionary>();
            InitializeComponent();
            InitializeView();
            NavigationPage.SetHasBackButton(this, false);
        }

        private void InitializeView()
        {
            if (SettingsService.GetLightTheme() == OSAppTheme.Light)
            {
                SettingsService.SetLightTheme(new LightTheme(), OSAppTheme.Light);
                LightThemeSwitch.IsToggled = false;
            }
            else
            {
                SettingsService.SetLightTheme(new DarkTheme(), OSAppTheme.Dark);
                LightThemeSwitch.IsToggled = true;
            }

            if (Application.Current.Resources.ContainsKey("FontFamily"))
            {
                Fonts.SelectedItem = Application.Current.Resources["FontFamily"];
            }

            var isToggled = SettingsService.GetLanguage() == "en"
                ? ActiveLocaleSwitch.IsToggled = false
                : ActiveLocaleSwitch.IsToggled = true;

            FontSizeSlider.Value = SettingsService.GetFontSize();
            FontSizeValue.Text = SettingsService.GetFontSize().ToString();
        }

        private void OnFontSizeChange(object sender, ValueChangedEventArgs e)
        {
            SettingsService.SetFontSize(Convert.ToInt32(FontSizeSlider.Value));
            FontSizeValue.Text = Convert.ToInt32(FontSizeSlider.Value).ToString(CultureInfo.InvariantCulture);
        }

        private void OnSwitchLanguage(object sender, ToggledEventArgs e)
        {
            SettingsService.SetLanguage(e.Value ? "ru" : "en");
        }

        private void OnSwitchLightTheme(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                SettingsService.SetLightTheme(new DarkTheme(), OSAppTheme.Dark);
            }
            else
            {
                SettingsService.SetLightTheme(new LightTheme(), OSAppTheme.Light);
            }
        }

        private void Fonts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsService.SetFontFamily(Fonts.SelectedItem as string);
        }
    }
}