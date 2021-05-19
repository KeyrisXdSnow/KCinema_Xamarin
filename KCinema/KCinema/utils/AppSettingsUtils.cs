using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCinema.utils
{
    public class AppSettingsUtils
    {
        public static bool IsAppThemeDark()
        {
            return Application.Current.Properties.ContainsKey("IsAppThemeDark")
                   && Application.Current.Properties["IsAppThemeDark"].ToString().ToUpper().Equals("TRUE");
        }

        public static async Task IsAppThemeDark(bool appThemeIsDark)
        {
            Application.Current.Properties["IsAppThemeDark"] = appThemeIsDark.ToString();
            await Application.Current.SavePropertiesAsync();
        }

        
    }
}