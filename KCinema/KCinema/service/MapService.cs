using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace KCinema.service
{
    public static class MapService
    {

        public static async Task<Location> GetPositionByAddress(string address)
        {
            return (await Geocoding.GetLocationsAsync(address)).FirstOrDefault(); ;
        }
    }
}