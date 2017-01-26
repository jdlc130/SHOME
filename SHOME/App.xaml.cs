using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
//using SystemConfiguration;

namespace SHOME
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public IBBeaconIterface Ibeacon;
        public IBBeacon Currentbeacon;

        public App()
        {
           /* Ibeacon = ServiceLocator.Current.GetService<IBBeaconIterface>();
            Ibeacon.EnteredRegion += IbeaconEnteredRegion;
        	 Ibeacon.ExitedRegion += IbeaconExitedRegion;
            Ibeacon.EnteredRange += IbeaconEnteredRange;*/
            Currentbeacon = new IBBeacon() {Proximity = 1};

		
			   if (!IsUserLoggedIn)
					{
						MainPage = new NavigationPage(new LoginPage());
					}
					else {
						MainPage = new NavigationPage(new SHOME.MenuPage());
					}
			
        }
		/*
        protected override void OnStart()
        {
            var beacons = SampleData.Exhibits.Select(exhibit => new IBBeacon()
            {
                ProximityUuid = exhibit.BeaconUuid,
                BeaconId = exhibit.Identifier,
                Major = exhibit.BeaconMajor,
                Minor = exhibit.BeaconMinor
            }).ToList();
            
            Ibeacon.StartListening(beacons);
        }

        protected override void OnSleep()
        {
            //Handle
        }
        
        protected override void OnResume()
        {
            Ibeacon.StopListening();
        }

        private void IbeaconEnteredRange(object sender, IBRangeEventArgs e)
	    {
            foreach (var beacon in e.Beacons)
            {
                //update the proximity of the currentbeacon
                if (Currentbeacon.BeaconId == beacon.BeaconId)
                {
                    Currentbeacon.Proximity = beacon.Proximity;
                }
                else
                {
                    if (beacon.Proximity < Currentbeacon.Proximity || beacon.Proximity == 0)
                    {
                        Currentbeacon = beacon;
                    }
                }
            }

            #region Sample Data
            var str = "";
	        if (Currentbeacon.ProximityUuid == null) return;
	        var exhibit = SampleData.Exhibits.Find(o =>
	                Currentbeacon.ProximityUuid.ToUpper().EndsWith(o.BeaconUuid.ToUpper()) &&
	                o.BeaconMajor == Currentbeacon.Major &&
	                o.BeaconMinor == Currentbeacon.Minor
	        );
	        if (exhibit == null)
	        {
	            str = "No Beacons In Range";
	        }
	        else
	        {
	            str = exhibit.Name + " - " + exhibit.Description;
	        }
	        MessagingCenter.Send(this, "Alert", str);

	        #endregion
        }

        private void IbeaconExitedRegion(object sender, IBMonitorEventArgs e)
        {
            Currentbeacon = new IBBeacon() { Proximity = 1 };
        }

        private void IbeaconEnteredRegion(object sender, IBMonitorEventArgs e)
        {
            Currentbeacon = new IBBeacon() { Proximity = 1 };
        }

	    public static Page GetMainPage()
	    {
            //ServiceLocator.Current.SetService(new NavigationPage(new MenuPage()));
            //return ServiceLocator.Current.GetService<NavigationPage>();
	        return new NavigationPage(new MenuPage());

	    } */
	}
}
