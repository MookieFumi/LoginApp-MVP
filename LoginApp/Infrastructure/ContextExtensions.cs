using Android.Content;
using Android.Gms.Common;

namespace LoginApp.Infrastructure
{
    public static class ContextExtensions
    {
        public static GooglePlayServicesAvailabilityResult CheckGooglePlayServicesAvailability(this Context context)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
            if (resultCode != ConnectionResult.Success)
            {
                var message = "This device is not supported";
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    message = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                return new GooglePlayServicesAvailabilityResult(false, message);
            }

            return new GooglePlayServicesAvailabilityResult(true);
        }

        public class GooglePlayServicesAvailabilityResult
        {
            public GooglePlayServicesAvailabilityResult(bool available, string reason = "")
            {
                Available = available;
                Reason = reason;
            }

            public bool Available { get; private set; }
            public string Reason { get; private set; }
        }
    }
}