using System.Diagnostics;
using System.Linq;
using CoreLocation;
using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SHOME.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        private static NSUuid RegionUuid =>
            new NSUuid("B9407F30-F5F8-466E-AFF9-25556B57FE6D");

        internal CLLocationManager LocationManager { get; private set; }

        public override bool FinishedLaunching(UIApplication application,
            NSDictionary launchOptions)
        {
            RegisterNotifications();

            LocationManager = new CLLocationManager();

            LocationManager.AuthorizationChanged += (s, e) =>
            {
                if (e.Status != CLAuthorizationStatus.AuthorizedAlways) return;

                var region = new CLBeaconRegion(RegionUuid, "Beacons");

                LocationManager.RegionEntered += (s1, e1) => SendEnterNotification();
                LocationManager.RegionLeft += (s1, e1) => SendExitNotification();

                LocationManager.StartMonitoring(region);
            };

            LocationManager.RequestAlwaysAuthorization();

            FindFirstBeacon();

            Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
            Calabash.Start();
            #endif

            LoadApplication(new App());

            return FinishedLaunching(application, launchOptions);
        }

        #region Code to send notification

        private static void RegisterNotifications()
        {
            var settings = UIUserNotificationSettings
                .GetSettingsForTypes(UIUserNotificationType.Alert, new NSSet());
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
        }

        private static void SendNotification(string alertBody)
        {
            var notification = new UILocalNotification {AlertBody = alertBody};
            UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
        }

        private static void SendEnterNotification()
        {
            SendNotification("SEND ENTER NOTIFICATION");
        }

        private static void SendExitNotification()
        {
            SendNotification("SEND EXIT NOTIFICATION");
        }

        #endregion

        private void FindFirstBeacon()
        {
            LocationManager.DidRangeBeacons += (s, e) =>
            {
                var first = e.Beacons.FirstOrDefault();
                if (first?.Accuracy >= 0)
                {
                    Debug.WriteLine($"Distance \n{first.Accuracy:N1}m");
                }
            };

            var region = new CLBeaconRegion(RegionUuid, "EvolveRanging");

            LocationManager.StartRangingBeacons(region);
        }
    }
}