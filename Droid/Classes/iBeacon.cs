using System;
using System.Collections.Generic;
using AltBeaconOrg.BoundBeacon;
using Android.Content;
using Android.OS;

namespace SHOME.Droid.Classes
{
    internal class iBeacon : IBBeaconIterface
    {
        private BeaconManager _beaconMgr;

        public MonitorNotifier MonitorNotifier;
        private readonly List<NotifiersStruct> _notifiers = new List<NotifiersStruct>();
        public RangeNotifier RangeNotifier;

        public string StartListening(List<IBBeacon> beacons)
        {
            var str = "";
            try
            {
                if (_beaconMgr.CheckAvailability())
                {
                    foreach (var b in beacons)
                    {
                        var nr = new NotifiersStruct
                        {
                            MonitoringRegion = new Region(
                                b.BeaconId,
                                Identifier.Parse(b.ProximityUuid),
                                Identifier.Parse(b.Major.ToString()),
                                Identifier.Parse(b.Minor.ToString())),
                            RangingRegion = new Region(
                                b.BeaconId,
                                Identifier.Parse(b.ProximityUuid),
                                Identifier.Parse(b.Major.ToString()),
                                Identifier.Parse(b.Minor.ToString()))
                        };

                        _beaconMgr.SetForegroundScanPeriod(1000L);
                        _beaconMgr.SetForegroundBetweenScanPeriod(10000L);

                        _beaconMgr.SetRangeNotifier(RangeNotifier);
                        _beaconMgr.SetMonitorNotifier(MonitorNotifier);
                        
                        try
                        {
                            _beaconMgr.StartMonitoringBeaconsInRegion(nr.MonitoringRegion);
                            _beaconMgr.StartRangingBeaconsInRegion(nr.RangingRegion);

                            _notifiers.Add(nr);
                        }
                        catch (RemoteException e)
                        {}
                        
                    }
                    str = "Success";
                    return str;
                }
                str = "IBeacon Service is not Connected";
                return str;
            }
            catch (AltBeaconOrg.BoundBeacon.BleNotAvailableException ex)
            {
                str = "Bluetooth LE not supported by this device.";
                return str;
            }
        }

        public string StopListening()
        {
            foreach (var nr in _notifiers)
            {
                _beaconMgr.StopMonitoringBeaconsInRegion(nr.MonitoringRegion);
                _beaconMgr.StopMonitoringBeaconsInRegion(nr.RangingRegion);
            }
            return "Success";
        }

        public string ResumeListening()
        {
            foreach (var nr in _notifiers)
            {
                _beaconMgr.StartMonitoringBeaconsInRegion(nr.MonitoringRegion);
                _beaconMgr.StartMonitoringBeaconsInRegion(nr.RangingRegion);
            }
            return "Success";
        }

        public void ServiceConnected()
        {
            _beaconMgr.SetMonitorNotifier(MonitorNotifier);
            _beaconMgr.SetRangeNotifier(RangeNotifier);
        }

        public event IBBeaconDelegates.RegionEnteredHandler EnteredRegion;
        public event IBBeaconDelegates.RegionExitedHandler ExitedRegion;
        public event IBBeaconDelegates.BeaconsInRangeHandler EnteredRange;

        public void Bind(IBeaconConsumer ibeaconconsumer)
        {
            _beaconMgr = BeaconManager.GetInstanceForApplication((Context) ibeaconconsumer);
            _beaconMgr.Bind(ibeaconconsumer);

            MonitorNotifier = new MonitorNotifier();
            MonitorNotifier.EnterRegionComplete += EnteredRegionHandler;
            MonitorNotifier.ExitRegionComplete += ExitedRegionHandler;

            RangeNotifier = new RangeNotifier();
            RangeNotifier.DidRangeBeaconsInRegionComplete += EnteredRangeHandler;
        }

        private void EnteredRegionHandler(object sender, MonitorEventArgs e)
        {
            if (EnteredRegion == null) return;
            IBRegion r = null;
            if (e.Region != null)
                r = new IBRegion(e.Region.Id3, e.Region.Id2, e.Region.Id1.ToString(), e.Region.UniqueId);
            EnteredRegion(sender, new IBMonitorEventArgs {Region = r, State = e.State});
        }

        private void ExitedRegionHandler(object sender, MonitorEventArgs e)
        {
            if (ExitedRegion == null) return;
            IBRegion r = null;
            if (e.Region != null)
                r = new IBRegion(e.Region.Id3, e.Region.Id2, e.Region.Id1.ToString(), e.Region.UniqueId);
            ExitedRegion(sender, new IBMonitorEventArgs {Region = r, State = e.State});
        }

        private void EnteredRangeHandler(object sender, RangeEventArgs e)
        {
            if (EnteredRange == null) return;
            var iba = new IBRangeEventArgs
            {
                Region = new IBRegion(e.Region.Id3, e.Region.Id2, e.Region.Id1.ToString(), e.Region.UniqueId),
                Beacons = new List<IBBeacon>()
            };
            foreach (var b in e.Beacons)
                iba.Beacons.Add(new IBBeacon
                {
                    Accuracy = b.Distance,
                    Major = int.Parse(b.Id3.ToString()),
                    Minor = int.Parse(b.Id2.ToString()),
                    Proximity = (int)b.Distance,
                    ProximityUuid = b.Id1.ToString(),
                    Rssi = b.Rssi
                });

            EnteredRange(sender, iba);
        }

        private struct NotifiersStruct
        {
            public Region MonitoringRegion;
            public Region RangingRegion;
        }
    }
}