using System.Collections.Generic;
using CoreLocation;
using Foundation;
using SHOME;

namespace Exhibitor.Mobile.iOS.Classes
{
    internal class iBeacon : IBBeaconIterface
    {
        private CLLocationManager locationMgr;

        private readonly List<CLBeaconRegion> Regions = new List<CLBeaconRegion>();

        public event IBBeaconDelegates.BeaconsInRangeHandler EnteredRange;
        public event IBBeaconDelegates.RegionEnteredHandler EnteredRegion;
        public event IBBeaconDelegates.RegionExitedHandler ExitedRegion;

        public void ServiceConnected()
        {
            locationMgr = new CLLocationManager();

            locationMgr.DidRangeBeacons += locationMgr_DidRangeBeacons;
            locationMgr.RegionEntered += locationMgr_RegionEntered;
            locationMgr.RegionLeft += locationMgr_RegionLeft;
        }

        public string StartListening(List<IBBeacon> beacons)
        {
            foreach (var b in beacons)
            {
                var r = new CLBeaconRegion(
                    b.ProximityUuid == null ? new NSUuid("") : new NSUuid(b.ProximityUuid),
                    b.BeaconId);

                r.NotifyEntryStateOnDisplay = true;
                r.NotifyOnEntry = true;
                r.NotifyOnExit = true;

                locationMgr.StartMonitoring(r);
                locationMgr.StartRangingBeacons(r);
                Regions.Add(r);
            }


            return "Success";
        }

        public string ResumeListening()
        {
            foreach (var r in Regions)
            {
                locationMgr.StopMonitoring(r);
                locationMgr.StopRangingBeacons(r);
            }

            return "Success";
        }

        public string StopListening()
        {
            foreach (var r in Regions)
            {
                locationMgr.StopMonitoring(r);
                locationMgr.StopRangingBeacons(r);
            }

            return "Success";
        }

        private void locationMgr_RegionLeft(object sender, CLRegionEventArgs e)
        {
            if (ExitedRegion == null) return;
            //IBRegion r = null;
            if (e.Region != null)
            {
                //r = new IBRegion(e.Region.Major, e.Region.Minor, e.Region.ProximityUuid, e.Region.UniqueId);
            }
            ExitedRegion(sender, new IBMonitorEventArgs());
        }

        private void locationMgr_RegionEntered(object sender, CLRegionEventArgs e)
        {
            if (EnteredRegion == null) return;
            IBRegion r = null;
            if (e.Region != null)
            {
                //r = new IBRegion(e.Region.Major, e.Region.Minor, e.Region.ProximityUuid, e.Region.UniqueId);
            }
            EnteredRegion(sender, new IBMonitorEventArgs());
        }

        private void locationMgr_DidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            if (EnteredRange == null) return;
            if (e.Beacons.Length > 0)
            {
                var iba = new IBRangeEventArgs();
                iba.Region = new IBRegion(e.Region.Major, e.Region.Minor, e.Region.ProximityUuid.ToString(),
                    e.Region.Identifier);
                iba.Beacons = new List<IBBeacon>();
                foreach (var b in e.Beacons)
                {
                    var proximity = 0;
                    switch (b.Proximity)
                    {
                        case CLProximity.Immediate:
                            proximity = (int) IBProximityType.Immediate;
                            break;
                        case CLProximity.Near:
                            proximity = (int) IBProximityType.Near;
                            break;
                        case CLProximity.Far:
                            proximity = (int) IBProximityType.Far;
                            break;
                        case CLProximity.Unknown:
                            proximity = (int) IBProximityType.Unknown;
                            break;
                    }

                    iba.Beacons.Add(new IBBeacon
                    {
                        Accuracy = b.Accuracy,
                        Major = (int) b.Major,
                        Minor = (int) b.Minor,
                        Proximity = proximity,
                        ProximityUuid = b.ProximityUuid.ToString(),
                        Rssi = (int) b.Rssi
                    });


                    EnteredRange(sender, iba);
                }
            }
        }
    }
}