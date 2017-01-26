using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOME
{
    class SampleData
    {
        static List<Exhibit> _Exhibits = null;

        public static List<Exhibit> Exhibits
        {
            get
            {
                if (_Exhibits == null)
                {
                    LoadExhibits();
                }

                return _Exhibits;
            }
        }

        private static void LoadExhibits()
        {
            _Exhibits = new List<Exhibit>
            {
                new Exhibit()
                {
                    Name = "ICE",
                    Description = "Ice Beacon",
                    BeaconUuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D",
                    Identifier = "ddf68224908d",
                    BeaconMajor = 37005,
                    BeaconMinor = 33316
                },
                new Exhibit()
                {
                    Name = "BLUEBERRY",
                    Description = "Blue Beacon",
                    BeaconUuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D",
                    Identifier = "f01be58cd0c8",
                    BeaconMajor = 53448,
                    BeaconMinor = 53448
                }
            };
        }

        public class Exhibit
        {
            public string Name = "";
            public string Description = "";
            public string BeaconUuid = "";
            public string Identifier = "";
            public int BeaconMajor = 0;
            public int BeaconMinor = 0;
        }
    }
}
