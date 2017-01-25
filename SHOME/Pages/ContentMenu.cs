using System.Collections.Generic;
using SHOME.Data;
using SHOME.Pages;
using Xamarin.Forms;

namespace SHOME
{
    public class ContentMenu : ContentPage
    {
        public List<Division> Divisions = new List<Division>();


        //public int tabb;
        //public EstarPage(int tab)
        //{
        //	tabb = tab;
        //}

        public ContentMenu(string tab)
        {
            //	var deviceLock = new Devices("lock", "lock");
            //	var deviceLights = new Devices("Luzes", "lights");

            //	var deviceAudio = new Devices("audio", "audio");
            //	var deviceBlinds = new Devices("blinds", "blinds");
            //	var deviceAC = new Devices("ac", "ac");
            var deviceWeather = new Devices(1000, "Weather", "weather");
            var deviceEnergyConsumption = new Devices(1001, "EnergyConsumption", "EnergyConsumption");
            var deviceEnergyManagement = new Devices(1002, "EnergyManagement", "EnergyManagement");
            var deviceCctv = new Devices(1003, "Camaras", "cctv");
            var deviceIrrigation = new Devices(1004, "irrigation", "irrigation");


            // Division
            var roomm = new Division(1000, "All", "Home", null);
            Divisions.Add(roomm);
            roomm.AddDivice(deviceWeather);
            roomm.AddDivice(deviceEnergyConsumption);
            roomm.AddDivice(deviceEnergyManagement);
            roomm.AddDivice(deviceCctv);
            roomm.AddDivice(deviceIrrigation);

            DivisionData(tab, "sdsd");
        }


        //TODO se fizer sentido e for mais prático fazes aqui o foreach em vez de mandar o iterator.
        public async void DivisionData(string tab, params object[] devices)
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "division");
            var size = json.Count;

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

                DevicesData(division.Id, division, tab);
                //var deviceLock = new Devices("lock", "lock");
                //var deviceLights = new Devices("Luzes", "lights");
                //var deviceCctv = new Devices("Camaras", "cctv");

                aux++;
                //TODO FOREACH para adicionar devices
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
                division.AddDivice(device);
                aux++;
            }

            Construtor(tab);
        }

        private async void Construtor(string tab)
        {
            var semiTransparentColor = new Color(0, 0, 0, 0.5);
            
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


            var buttonLights = new List<Image>();
            var buttonCctvs = new List<Image>();
            var buttonACs = new List<Image>();
            var buttonListAudio = new List<Image>();
            var buttonListBlinds = new List<Image>();
            var buttonListLock = new List<Image>();
            var buttonListIrrigation = new List<Image>();
            var buttonListWeather = new List<Image>();
            var buttonListEnergyConsumption = new List<Image>();
            var buttonListEnergyManagement = new List<Image>();


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
                        new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)}
                    }
                };
                stack.Children.Add(new Label
                {
                    Text = s.Name.ToUpper(),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "Roboto",
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 18,
                    BackgroundColor = Color.Gray
                });
                
                var rowGrid = 0;
                var columnGrid = 0;
                foreach (var dev in s.devices)
                {
                    switch (dev.Type)
                    {
                        case "Light":
                            var buttonLight = new Image
                            {
                                Source = "lights.png",
                                WidthRequest = 70,
                                HeightRequest = 70,
                                Opacity = 2,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonLights.Add(buttonLight);
                            grid.Children.Add(buttonLight, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "cctv":
                            var buttonCctv = new Image { Source = "cctv.png", WidthRequest = 7, HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonCctvs.Add(buttonCctv);
                            grid.Children.Add(buttonCctv, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "ac":
                            var buttonAc = new Image
                            {
                                Source = "ac.png", WidthRequest = 7, HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonACs.Add(buttonAc);
                            grid.Children.Add(buttonAc, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "audio":
                            var buttonAudio = new Image { Source = "audio.png", WidthRequest = 7, HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListAudio.Add(buttonAudio);
                            grid.Children.Add(buttonAudio, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "blinds":
                            var buttonBlinds = new Image
                            {
                                Source = "blinds.png",
                                WidthRequest = 7,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListBlinds.Add(buttonBlinds);
                            grid.Children.Add(buttonBlinds, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "Lock":
                            var buttonLock = new Image { Source = "lock.png", WidthRequest = 7, HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListLock.Add(buttonLock);
                            grid.Children.Add(buttonLock, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "irrigation":
                            var buttonIrrigation = new Image
                            {
                                Source = "irrigation.png",
                                WidthRequest = 7,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListIrrigation.Add(buttonIrrigation);
                            grid.Children.Add(buttonIrrigation, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "weather":
                            var buttonWeather = new Image
                            {
                                Source = "weather.png",
                                WidthRequest = 7,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListWeather.Add(buttonWeather);
                            grid.Children.Add(buttonWeather, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "EnergyConsumption":
                            var buttonEnergyConsumption = new Image
                            {
                                Source = "energy_consumption.png",
                                WidthRequest = 7,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListEnergyConsumption.Add(buttonEnergyConsumption);
                            grid.Children.Add(buttonEnergyConsumption, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        case "EnergyManagement":
                            var buttonEnergyManagement = new Image
                            {
                                Source = "energy_management.png",
                                WidthRequest = 7,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.Center
                            };
                            buttonListEnergyManagement.Add(buttonEnergyManagement);
                            grid.Children.Add(buttonEnergyManagement, columnGrid, rowGrid);
                            columnGrid++;
                            break;
                        default:
                            break;
                    }
                }
                //TODO
                /*
                var addButton = new Image
                {
                    Source = "energy_management.png",
                    WidthRequest = 7,
                    HeightRequest = 70
                };
                buttonListEnergyManagement.Add(addButton);
                grid.Children.Add(addButton, columnGrid, rowGrid);
                */


                stack.Children.Add(new ScrollView
                {
                    Content = grid,
                    Orientation = ScrollOrientation.Horizontal
                });
                stack.Spacing = 20;
            }

            var i = 0;
            while (i < buttonLights.Count)
            {
                buttonLights[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonACs.Count)
            {
                buttonACs[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListLock.Count)
            {
                buttonListLock[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new LockPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListAudio.Count)
            {
                buttonListAudio[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListBlinds.Count)
            {
                buttonListBlinds[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonCctvs.Count)
            {
                buttonCctvs[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new CameraPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListIrrigation.Count)
            {
                buttonListIrrigation[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new GestaoPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListWeather.Count)
            {
                buttonListWeather[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new MyCarouselPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListEnergyConsumption.Count)
            {
                buttonListEnergyConsumption[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new ConsumptionPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }

            i = 0;
            while (i < buttonListEnergyManagement.Count)
            {
                buttonListEnergyManagement[i].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { Navigation.PushModalAsync(new GestaoPage()); }),
                    NumberOfTapsRequired = 1
                });
                i++;
            }
            
            //TODO

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