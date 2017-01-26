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
using SHOME.Droid.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Beacon = AltBeaconOrg.BoundBeacon.Beacon;

namespace SHOME.Droid
{
    [Activity(Label = "SHOME.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity, IBeaconConsumer
    {
        protected override void OnCreate(Bundle bundle)
        {
            //LOAD RESOURCES
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            var ib = new iBeacon();
            ib.Bind(this);

            SHOME.ServiceLocator.Current.SetService<IBBeaconIterface>(ib);
            
            LoadApplication(new App());
        }

        public void OnBeaconServiceConnect()
        {
            ServiceLocator.Current.GetService<IBBeaconIterface>().ServiceConnected();
        }

        protected override void OnResume()
        {
            SHOME.ServiceLocator.Current.GetService<IBBeaconIterface>().ResumeListening();
            base.OnResume();
        }

        protected override void OnStop()
        {
            SHOME.ServiceLocator.Current.GetService<IBBeaconIterface>().StopListening();
            base.OnStop();
        }
    }
}