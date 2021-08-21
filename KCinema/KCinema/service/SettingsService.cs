using System.Globalization;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace KCinema.service
{
    public static class SettingsService
    {
        private const int DefaultFontSize = 16;
        public static int GetFontSize()
        {
            if (!Application.Current.Properties.ContainsKey("FontSize"))
            {
                
                return DefaultFontSize;
            }

            return (int) Application.Current.Properties["FontSize"];
        }

        public static void SetFontSize(int value)
        {
            if (!Application.Current.Resources.ContainsKey("FontSize"))
            {
                Application.Current.Resources.Add("FontSize", value);
                Application.Current.Properties["FontSize"] = value;
            }
            else
            {
                Application.Current.Resources["FontSize"] = value;
                Application.Current.Properties["FontSize"] = value;
            }
        }

        private static string GetFontFamily()
        {
            if (!Application.Current.Resources.ContainsKey("FontFamily"))
            {
                return "";
            }

            return (string) Application.Current.Resources["FontFamily"];
        }

        public static void SetFontFamily(string font)
        {
            
            if (!Application.Current.Properties.ContainsKey("FontFamily"))
            {
                Application.Current.Resources.Add("FontFamily", font);
            }
            else
            {
                Application.Current.Resources["FontFamily"] = font;
            }
        }

        public static string GetLanguage()
        {
            return LocalizationResourceManager.Current.CurrentCulture.ToString();
        }

        public static void SetLanguage(string langCode)
        {
            LocalizationResourceManager.Current.SetCulture(
                CultureInfo.GetCultureInfo(langCode));   
        }

        public static void SetLightTheme(ResourceDictionary theme, OSAppTheme osAppTheme)
        {
            Application.Current.Resources = theme;
            Application.Current.UserAppTheme = osAppTheme;
        }
        
        public static OSAppTheme GetLightTheme()
        {
            return Application.Current.RequestedTheme;
        }

        public static async Task SaveSettings()
        {
            if (Application.Current.Properties.ContainsKey("Lang"))
            {
                Application.Current.Properties["Lang"] = GetLanguage();
            }
            else
            {
                Application.Current.Properties.Add("Lang",GetLanguage());
            }
            
            
            if (Application.Current.Properties.ContainsKey("FontSize"))
            {
                Application.Current.Properties["FontSize"] = GetFontSize();
            }
            else
            {
                Application.Current.Properties.Add("FontSize",GetFontSize());
            }
            
            
            if (Application.Current.Properties.ContainsKey("FontFamily"))
            {
                Application.Current.Properties["FontFamily"] = GetFontFamily();
            }
            else
            {
                Application.Current.Properties.Add("FontFamily",GetFontFamily());
            }
            
            if (Application.Current.Properties.ContainsKey("LightMode"))
            {
                Application.Current.Properties["LightMode"] = GetLightTheme().ToString();
            }
            else
            {
                Application.Current.Properties.Add("LightMode",GetLightTheme().ToString());
            }
            
            await Application.Current.SavePropertiesAsync();
        }
        
    }
}