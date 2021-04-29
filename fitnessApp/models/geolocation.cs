using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Threading.Tasks;

namespace fitnessApp.models
{
    class geolocation
    {

        public async Task<Location> GPSresquest()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.Medium, Timeout = TimeSpan.FromSeconds(10) });
                return location;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"something went wrong: {ex.Message}");
            }
            return await Geolocation.GetLastKnownLocationAsync();
        }
    }
}
