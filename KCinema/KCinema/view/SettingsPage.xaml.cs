using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnFontSizeChange(object sender, ValueChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSwitchLanguage(object sender, ToggledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSwitchLigthTheme(object sender, ToggledEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}