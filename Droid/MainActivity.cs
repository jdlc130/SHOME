using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltBeaconOrg.BoundBeacon;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using SHOME.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Resource = Xamarin.Forms.Platform.Android.Resource;

namespace SHOME.Droid
{
    [Activity(Label = "SHOME.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity, IDialogInterfaceOnDismissListener, IBeaconConsumer
    {
        public List<Beacon> Data;
        private readonly RangeNotifier _rangeNotifier;

        private BeaconManager _beaconManager;
        private Region _emptyRegion;
        private Region _tagRegion;

        /// <summary>
        ///     Construtor.
        /// </summary>
        public MainActivity()
        {
            _rangeNotifier = new RangeNotifier();
            Data = new List<Beacon>();
        }

        public void OnBeaconServiceConnect()
        {
            _beaconManager.SetForegroundBetweenScanPeriod(1100);
            _beaconManager.SetRangeNotifier(_rangeNotifier);

            _tagRegion = new Region("myUniqueBeaconId", Identifier.Parse("B9407F30-F5F8-466E-AFF9-25556B57FE6D"), null,
                null);
            _emptyRegion = new Region("myEmptyBeaconId", null, null, null);

            _beaconManager.StartRangingBeaconsInRegion(_tagRegion);
            _beaconManager.StartRangingBeaconsInRegion(_emptyRegion);
        }

        /// <summary>
        ///     IDialogInterface' Implementation
        /// </summary>
        /// <param name="dialog"></param>
        public void OnDismiss(IDialogInterface dialog)
        {
            //Activity done and should be closed.
            //Finish(); //Closes the app.
        }

        public bool BindService(Intent p0, IServiceConnection p1, int p2)
        {
            return true;
        }

        protected override void OnCreate(Bundle bundle)
        {
            //LOAD RESOURCES
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());

            foreach (var beacon in Data)
            {
                //TODO Precisas disto.
                var id = beacon.Id1;
                var distance = beacon.Distance;
            }

            VerityBluetooth();

            _beaconManager = BeaconManager.GetInstanceForApplication(this);
            var iBeaconParser = new BeaconParser();
            iBeaconParser.SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24");
            _beaconManager.BeaconParsers.Add(iBeaconParser);
            _beaconManager.Bind(this);

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
        }

        protected override void OnResume()
        {
            base.OnResume();

            //TODO SEE THIS
            ((BeaconReferenceApplication) ApplicationContext).MainActivity = this;

            if (_beaconManager.IsBound(this))
                _beaconManager.SetBackgroundMode(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_beaconManager.IsBound(this)) _beaconManager.Unbind(this);
        }

        /// <summary>
        ///     Create the alert box interface
        /// </summary>
        private void VerityBluetooth()
        {
            try
            {
                if (BeaconManager.GetInstanceForApplication(this).CheckAvailability()) return;
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Bluetooth not enabled");
                builder.SetMessage("Please enable bluetooth in settings and restart this application.");
                builder.SetPositiveButton(Android.Resource.String.Ok, (EventHandler<DialogClickEventArgs>) null);
                builder.SetOnDismissListener(this);
                builder.Show();
            }
            catch (BleNotAvailableException e)
            {
                Log.Debug("BleNotAvailableException", e.Message);

                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Bluetooth LE not available");
                builder.SetMessage("Sorry, this device does not support Bluetooth LE.");
                builder.SetPositiveButton(Android.Resource.String.Ok, (EventHandler<DialogClickEventArgs>) null);
                builder.SetOnDismissListener(this);
                builder.Show();
            }
        }

        private void RemoveBeaconsNoLongerVisible(ICollection<Beacon> allBeacons)
        {
            if ((allBeacons == null) || (allBeacons.Count == 0)) return;

            var delete = Data.Where(d => allBeacons.All(ab => ab.Id1.ToString() != d.Id1.ToString())).ToList();
            Data.RemoveAll(d => delete.Any(del => del.Id1.ToString() == d.Id1.ToString()));

            if (delete.Count > 0)
                delete = null;
        }

        private async void RangingBeaconsInRegion(object sender, RangeEventArgs e)
        {
            if (e.Beacons.Count <= 0) return;
            var allBeacons = e.Beacons.ToList();

            var orderedBeacons = allBeacons.OrderBy(b => b.Distance).ToList();
            await UpdateData(orderedBeacons);
        }

        private async Task UpdateData(IList<Beacon> beacons)
        {
            await Task.Run(() =>
            {
                var newBeacons = new List<Beacon>();

                foreach (var beacon in beacons)
                    if (Data.Exists(b => b.Id1.ToString() == beacon.Id1.ToString()))
                    {
                        //Update Data
                        var index = Data.FindIndex(b => b.Id1.ToString() == beacon.Id1.ToString());
                        Data[index] = beacon;
                    }
                    else
                    {
                        newBeacons.Add(beacon);
                    }

                RunOnUiThread(() =>
                {
                    foreach (var beacon in newBeacons)
                        Data.Add(beacon);

                    if (newBeacons.Count > 0)
                        Data.Sort((x, y) => x.Distance.CompareTo(y.Distance));
                });
            });
        }
    }
}