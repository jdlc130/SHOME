using System;
using System.Collections.Generic;
using AltBeaconOrg.BoundBeacon;
using Object = Java.Lang.Object;

namespace SHOME.Droid.Classes
{
    public class RangeEventArgs : EventArgs
    {
        public Region Region { get; set; }
        public ICollection<Beacon> Beacons { get; set; }
    }

    public class RangeNotifier : Object, IRangeNotifier
    {
        public void DidRangeBeaconsInRegion(ICollection<Beacon> beacons, Region region)
        {
            OnDidRangeBeaconsInRegion(beacons, region);
        }

        public event EventHandler<RangeEventArgs> DidRangeBeaconsInRegionComplete;

        private void OnDidRangeBeaconsInRegion(ICollection<Beacon> beacons, Region region)
        {
            DidRangeBeaconsInRegionComplete?.Invoke(this, new RangeEventArgs {Beacons = beacons, Region = region});
        }
    }
}