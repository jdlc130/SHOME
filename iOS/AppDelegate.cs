using System;
using System.Collections.Generic;
using System.Linq;
using Exhibitor.Mobile.iOS.Classes;
using Foundation;
using UIKit;

namespace SHOME.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

            var ib = new iBeacon();
            ib.ServiceConnected();

            ServiceLocator.Current.SetService<IBBeaconIterface>(ib);
            
            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            #endif

			LoadApplication(new App());

			return true;
		}

        public override void DidEnterBackground(UIApplication application)
        {
            SHOME.ServiceLocator.Current.GetService<IBBeaconIterface>().StopListening();
        }

        public override void WillEnterForeground(UIApplication application)
        {
            SHOME.ServiceLocator.Current.GetService<IBBeaconIterface>().ResumeListening();
        }
    }
}
