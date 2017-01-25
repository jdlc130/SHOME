using System.Collections.Generic;
using SHOME.Data;
using SHOME.Pages;
using Xamarin.Forms;

namespace SHOME
{
    public class ContentMenu : ContentPage
    {
        public ContentMenu(string tab)
        {
            //	var deviceLock = new Devices("lock", "lock");
            //	var deviceLights = new Devices("Luzes", "lights");

            //	var deviceAudio = new Devices("audio", "audio");
            //	var deviceBlinds = new Devices("blinds", "blinds");
            //	var deviceAC = new Devices("ac", "ac");
            var deviceWeather = new Devices(1000, "Weather", "weather");
            var deviceEvents = new Devices(1008, "Events", "Events");
            var deviceEnergyConsumption = new Devices(1001, "EnergyConsumption", "EnergyConsumption");
            var deviceEnergyManagement = new Devices(1002, "EnergyManagement", "EnergyManagement");
            var deviceCctv = new Devices(1003, "Camaras", "cctv");
            var deviceIrrigation = new Devices(1004, "irrigation", "irrigation");
            var buttonEvents = new Image {Source = "icon_events.png", WidthRequest = 7, HeightRequest = 70};
            var buttonWeather = new Image {Source = "weather.png", WidthRequest = 7, HeightRequest = 70};
            var buttonEnergyConsumption = new Image
            {
                Source = "energy_consumption.png",
                WidthRequest = 7,
                HeightRequest = 70
            };
            var buttonEnergyManagement = new Image
            {
                Source = "energy_management.png",
                WidthRequest = 7,
                HeightRequest = 70
            };


            // Division
            var roomm = new Division(1000, "Sugest", "Home", null);
            Divisions.Add(roomm);

            roomm = new Division(1000, "All", "Home", null);
            Divisions.Add(roomm);

            deviceWeather.buttons = buttonWeather;
            deviceWeather.buttonState = 0;
            roomm.AddDivice(deviceWeather);

            deviceEvents.buttons = buttonEvents;
            deviceEvents.buttonState = 0;
            roomm.AddDivice(deviceEvents);


            deviceEnergyConsumption.buttons = buttonEnergyConsumption;
            deviceEnergyConsumption.buttonState = 0;
            roomm.AddDivice(deviceEnergyConsumption);

            deviceEnergyManagement.buttons = buttonEnergyManagement;
            deviceEnergyManagement.buttonState = 0;
            roomm.AddDivice(deviceEnergyManagement);


            roomm.AddDivice(deviceCctv);
            roomm.AddDivice(deviceIrrigation);

            DivisionData(tab, "sdsd");
        }

        public static List<Division> Divisions { get; set; } = new List<Division>();

        public List<Division> getDivisions()
        {
            return Divisions;
        }


        //TODO se fizer sentido e for mais prático fazes aqui o foreach em vez de mandar o iterator.
        public async void DivisionData(string tab, params object[] devices)
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "division");
            var size = json.Count;
            var sizeDivisionData = json.Count;

            while (size > aux)
            {
                var result = json[aux];
                //TODO em vez de "idDivision" é o type (SERVER DOWN)
                var division = new Division(
                    result["idDivision"],
                    result["divisionName"],
                    result["typeDivision"],
                    result["BeaconId"]
                );
                Divisions.Add(division);

                DevicesData(division.Id, division, tab, sizeDivisionData);
                //var deviceLock = new Devices("lock", "lock");
                //var deviceLights = new Devices("Luzes", "lights");
                //var deviceCctv = new Devices("Camaras", "cctv");
                sizeDivisionData--;
                aux++;
                //TODO FOREACH para adicionar devices
            }
        }

        public async void DevicesData(int id, Division division, string tab, int sizeDivisionData)
        {
            var json = await WebServicesData.SyncTask("GET", "ActuatorsDevicesByDivision", id);
            var size = json.Count;
            var aux = 0;

            while (size > aux)
            {
                var result = json[aux];

                var device = new Devices(
                    result["idActuator"],
                    result["deviceName"],
                    result["actuatorName"]
                );

                switch (device.Type)
                {
                    case "Light":
                        var buttonLight = new Image
                        {
                            Source = "lights.png",
                            WidthRequest = 70,
                            HeightRequest = 70,
                            Opacity = 2
                        };
                        device.buttons = buttonLight;
                        break;

                    case "CCTV":
                        var buttonCctv = new Image {Source = "cctv.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonCctv;
                        break;

                    case "AC":
                        var buttonAC = new Image {Source = "ac.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonAC;
                        break;

                    case "Audio":
                        var buttonAudio = new Image {Source = "audio.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonAudio;
                        break;
                    case "Blinds":
                        var buttonBlinds = new Image {Source = "blinds.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonBlinds;
                        break;
                    case "Lock":
                        var buttonLock = new Image {Source = "lock.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonLock;
                        break;
                    case "Irrigations":
                        var buttonIrrigation = new Image
                        {
                            Source = "irrigation.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.buttons = buttonIrrigation;
                        break;
                    case "weather":
                        var buttonWeather = new Image {Source = "weather.png", WidthRequest = 7, HeightRequest = 70};
                        device.buttons = buttonWeather;
                        break;
                    case "EnergyConsumption":
                        var buttonEnergyConsumption = new Image
                        {
                            Source = "energy_consumption.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.buttons = buttonEnergyConsumption;
                        break;
                    case "EnergyManagement":
                        var buttonEnergyManagement = new Image
                        {
                            Source = "energy_management.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.buttons = buttonEnergyManagement;
                        break;
                }


                device.buttonState = 0;
                division.AddDivice(device);
                aux++;
            }

            if (sizeDivisionData <= 0)
            {
            }
            dd(tab);
        }

        public async void dd(string tab)
        {
            var semiTransparentColor = new Color(0, 0, 0, 0.5);


            if (Device.OS == TargetPlatform.iOS)
            {
            }

            /// Device
            //var deviceLock = new Devices("lock", "lock");
            //var deviceLights = new Devices("Luzes", "lights");
            //var deviceCctv = new Devices("Camaras", "cctv");
            //var deviceAudio = new Devices("audio", "audio");
            //var deviceBlinds = new Devices("blinds", "blinds");
            //var deviceAC = new Devices("ac", "ac");
            //var deviceWeather = new Devices("weather", "weather");
            //var deviceIrrigation = new Devices("irrigation", "irrigation");


            switch (tab)
            {
                case "Bedroom":
                    BackgroundImage = "bedroom_background.png";
                    break;

                case "Living Room":
                    BackgroundImage = "lounge_background.png";
                    break;

                case "Home":
                    BackgroundImage = "menu_background.png";
                    break;

                case "Garden":
                    BackgroundImage = "garden_background.png";
                    break;

                case "Kitchen":
                    BackgroundImage = "kitchen_background.png";
                    break;
            }


            //var buttonLights = new List<Image>();
            //var buttonCctvs = new List<Image>();
            //var buttonACs = new List<Image>();
            //var buttonListAudio = new List<Image>();
            //var buttonListBlinds = new List<Image>();
            //var buttonListLock = new List<Image>();
            //var buttonListIrrigation = new List<Image>();
            //var buttonListWeather = new List<Image>();
            //var buttonListEnergyConsumption = new List<Image>();
            //var buttonListEnergyManagement = new List<Image>();


            var stack = new StackLayout(); ////
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            //var tt = await RequestData.SyncTask("division");


            foreach (var s in Divisions)
                if ((s.Type == tab) || (tab == "Home"))
                {
                    var grid = new Grid
                    {
                        BackgroundColor = new Color(0, 0, 0, 0),
                        RowDefinitions = new RowDefinitionCollection
                        {
                            new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                        }
                    };
                    //grid.HorizontalOptions = LayoutOptions.StartAndExpand;
                    stack.Children.Add(new Label
                    {
                        Text = s.Name.ToUpper(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.FromRgba(211, 211, 211, 100),
                        FontSize = 18,
                        FontFamily = "Roboto",
                        FontAttributes = FontAttributes.Bold
                    });


                    var rowGrid = 0;
                    var columnGrid = 0;
                    foreach (var dev in s.devices)
                    {
                        grid.Children.Add(dev.buttons, columnGrid, rowGrid);


                        if (dev.buttonState == 0)
                        {
                            switch (dev.Type)
                            {
                                case "Light":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                                        NumberOfTapsRequired = 1
                                    });

                                    columnGrid++;
                                    break;

                                case "CCTV":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new CameraPage()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;

                                case "Lock":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new LocksPage()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;

                                case "weather":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new Weather()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;
                                case "EnergyConsumption":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command =
                                            new Command(() => { Navigation.PushModalAsync(new ConsumptionPage()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;
                                case "EnergyManagement":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new GestaoPage()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;

                                case "Events":
                                    dev.buttons.GestureRecognizers.Add(new TapGestureRecognizer
                                    {
                                        Command = new Command(() => { Navigation.PushModalAsync(new ListEventPage()); }),
                                        NumberOfTapsRequired = 1
                                    });
                                    columnGrid++;
                                    break;
                            }


                            dev.buttonState = 1;
                        }
                    }
                    stack.Spacing = 20;

                    //grid.Opacity = 0.5;
                    stack.Children.Add(new ScrollView
                    {
                        Content = grid,
                        Orientation = ScrollOrientation.Horizontal
                    });
                }


            var scollVertical = new ScrollView
            {
                Content = stack,
                Orientation = ScrollOrientation.Vertical
            };

            Content = scollVertical;
        }

        public class Devices
        {
            public Devices(int id, string name, string type)
            {
                Id = id;
                Name = name;
                Type = type;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }

            public Image buttons { get; set; } = new Image();
            public int buttonState { get; set; }
        }

        public class Division
        {
            public Division(int id, string name, string type, string beaconId)
            {
                Id = id;
                Name = name;
                Type = type;
                BeaconId = beaconId;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string BeaconId { get; set; }

            public List<Devices> devices { get; } = new List<Devices>();

            public void AddDivice(Devices dev)
            {
                devices.Add(dev);
            }
        }
    }
}