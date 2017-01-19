using System;
using AltBeaconOrg.BoundBeacon;
using AltBeaconOrg.BoundBeacon.Powersave;
using AltBeaconOrg.BoundBeacon.Startup;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using TaskStackBuilder = Android.App.TaskStackBuilder;

namespace SHOME.Droid
{
    [Application(Label = "AltBeacon Sample", Icon = "@drawable/altbeacon")]
    public class BeaconReferenceApplication : Application, IBootstrapNotifier
    {
        private const string TAG = "AndroidProximityReferenceApplication";
        private Region _backgroundRegion;

        private BeaconManager _beaconManager;
        private bool _haveDetectedBeaconsSinceBoot;

        public BeaconReferenceApplication()
        {
        }

        public BeaconReferenceApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public MainActivity MainActivity { get; set; } = null;

        public void DidDetermineStateForRegion(int state, Region region)
        {
        }

        public void DidEnterRegion(Region region)
        {
            // In this example, this class sends a notification to the user whenever a Beacon
            // matching a Region (defined above) are first seen.
            Log.Debug(TAG, "did enter region.");
            if (!_haveDetectedBeaconsSinceBoot)
            {
                Log.Debug(TAG, "auto launching MonitoringActivity");

                // The very first time since boot that we detect an beacon, we launch the
                // MainActivity
                var intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                // Important:  make sure to add android:launchMode="singleInstance" in the manifest
                // to keep multiple copies of this activity from getting created if the user has
                // already manually launched the app.
                StartActivity(intent);
                _haveDetectedBeaconsSinceBoot = true;
            }
            else
            {
                if (MainActivity != null)
                {
                    Log.Debug(TAG, "I see a beacon again");
                }
                else
                {
                    // If we have already seen beacons before, but the monitoring activity is not in
                    // the foreground, we send a notification to the user on subsequent detections.
                    Log.Debug(TAG, "Sending notification.");
                    SendNotification();
                }
            }
        }

        public void DidExitRegion(Region region)
        {
            Log.Debug(TAG, "did exit region.");
        }

        public override void OnCreate()
        {
            base.OnCreate();

            _beaconManager = BeaconManager.GetInstanceForApplication(this);

            var iBeaconParser = new BeaconParser();
            //	Estimote > 2013
            iBeaconParser.SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24");
            _beaconManager.BeaconParsers.Add(iBeaconParser);

            Log.Debug(TAG, "setting up background monitoring for beacons and power saving");
            // wake up the app when a beacon is seen
            _backgroundRegion = new Region("backgroundRegion", null, null, null);
            new RegionBootstrap(this, _backgroundRegion);

            // simply constructing this class and holding a reference to it in your custom Application
            // class will automatically cause the BeaconLibrary to save battery whenever the application
            // is not visible.  This reduces bluetooth power usage by about 60%
            new BackgroundPowerSaver(this);
        }

        private void SendNotification()
        {
            var builder =
                new NotificationCompat.Builder(this)
                    .SetContentTitle("AltBeacon Reference Application")
                    .SetContentText("A beacon is nearby.")
                    .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo);

            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddNextIntent(new Intent(this, typeof(MainActivity)));
            var resultPendingIntent =
                stackBuilder.GetPendingIntent(
                    0,
                    PendingIntentFlags.UpdateCurrent
                );
            builder.SetContentIntent(resultPendingIntent);
            var notificationManager =
                (NotificationManager) GetSystemService(NotificationService);
            notificationManager.Notify(1, builder.Build());
        }
    }
}