using Xamarin.Forms;

namespace KCinema.utils
{
    public static class FontsHelper
    {
        public static async void ChangeFontSize(float fontSize)
        {
            Application.Current.Properties["ActiveFontSize"] = fontSize;
            await Application.Current.SavePropertiesAsync();
        }
    }
}