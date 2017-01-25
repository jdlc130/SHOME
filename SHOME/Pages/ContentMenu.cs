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

            deviceWeather.Buttons = buttonWeather;
            deviceWeather.ButtonState = 0;
            roomm.AddDevice(deviceWeather);

            deviceEvents.Buttons = buttonEvents;
            deviceEvents.ButtonState = 0;
            roomm.AddDevice(deviceEvents);

            deviceEnergyConsumption.Buttons = buttonEnergyConsumption;
            deviceEnergyConsumption.ButtonState = 0;
            roomm.AddDevice(deviceEnergyConsumption);

            deviceEnergyManagement.Buttons = buttonEnergyManagement;
            deviceEnergyManagement.ButtonState = 0;
            roomm.AddDevice(deviceEnergyManagement);

            roomm.AddDevice(deviceCctv);
            roomm.AddDevice(deviceIrrigation);

            DivisionData(tab, "sdsd");
        }

        public static List<Division> Divisions { get; set; } = new List<Division>();

        public List<Division> GetDivisions()
        {
            return Divisions;
        }

        public async void DivisionData(string tab, params object[] devices)
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "division");
            var size = json.Count;

            while (size > aux)
            {
                var result = json[aux];
                var division = new Division(
                    result["idDivision"],
                    result["divisionName"],
                    result["typeDivision"],
                    result["BeaconId"]
                );
                Divisions.Add(division);

                DevicesData(division.Id, division, tab);
                aux++;
            }
        }

        public async void DevicesData(int id, Division division, string tab)
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
                        device.Buttons = buttonLight;
                        break;
                    case "CCTV":
                        var buttonCctv = new Image {Source = "cctv.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonCctv;
                        break;
                    case "AC":
                        var buttonAC = new Image {Source = "ac.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonAC;
                        break;
                    case "Audio":
                        var buttonAudio = new Image {Source = "audio.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonAudio;
                        break;
                    case "Blinds":
                        var buttonBlinds = new Image {Source = "blinds.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonBlinds;
                        break;
                    case "Lock":
                        var buttonLock = new Image {Source = "lock.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonLock;
                        break;
                    case "Irrigations":
                        var buttonIrrigation = new Image
                        {
                            Source = "irrigation.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.Buttons = buttonIrrigation;
                        break;
                    case "weather":
                        var buttonWeather = new Image {Source = "weather.png", WidthRequest = 7, HeightRequest = 70};
                        device.Buttons = buttonWeather;
                        break;
                    case "EnergyConsumption":
                        var buttonEnergyConsumption = new Image
                        {
                            Source = "energy_consumption.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.Buttons = buttonEnergyConsumption;
                        break;
                    case "EnergyManagement":
                        var buttonEnergyManagement = new Image
                        {
                            Source = "energy_management.png",
                            WidthRequest = 7,
                            HeightRequest = 70
                        };
                        device.Buttons = buttonEnergyManagement;
                        break;
                    default:
                        break;
                }

                device.ButtonState = 0;
                division.AddDevice(device);
                aux++;
            }

            Construtor(tab);
        }

        private async void Construtor(string tab)
        {
            var semiTransparentColor = new Color(0, 0, 0, 0.5);

            if (Device.OS == TargetPlatform.iOS)
            {
            }

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
                default:
                    break;
            }

            var stack = new StackLayout();
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            foreach (var s in Divisions)
            {
                if ((s.Type != tab) && (tab != "Home")) continue;
                var grid = new Grid
                {
                    BackgroundColor = new Color(0, 0, 0, 0),
                    RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                    }
                };
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
                foreach (var dev in s.Devices)
                {
                    grid.Children.Add(dev.Buttons, columnGrid, rowGrid);

                    if (dev.ButtonState != 0) continue;
                    switch (dev.Type)
                    {
                        case "Light":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "CCTV":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new CameraPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "Lock":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new LocksPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "weather":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new Weather()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "EnergyConsumption":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new ConsumptionPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "EnergyManagement":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new GestaoPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        case "Events":
                            dev.Buttons.GestureRecognizers.Add(new TapGestureRecognizer
                            {
                                Command = new Command(() => { Navigation.PushModalAsync(new ListEventPage()); }),
                                NumberOfTapsRequired = 1
                            });
                            columnGrid++;
                            break;
                        default:
                            break;
                    }

                    dev.ButtonState = 1;
                }
                stack.Spacing = 20;

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

            public Image Buttons { get; set; } = new Image();
            public int ButtonState { get; set; }
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

            public List<Devices> Devices { get; } = new List<Devices>();

            public void AddDevice(Devices dev)
            {
                Devices.Add(dev);
            }
        }
    }
}