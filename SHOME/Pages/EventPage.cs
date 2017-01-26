using System;
using System.Collections.Generic;
using SHOME.Data;
using Xamarin.Forms;

namespace SHOME
{
    internal class EventPage : ContentPage
    {
        public DatePicker DataPicker;
        public Picker DivPicker, DevPicker, StatePicker;
        public Entry NameEntry, DescrEntry;
        public TimePicker StartTimePicker, EndTimePicker;

        public EventPage()
        {
            DivPicker = new Picker();

            DivisionData();
        }

        public static List<Division> Divisions { get; set; } = new List<Division>();

        private void Construtor()
        {
            var header = new Image
            {
                Source = new FileImageSource
                {
                    File = Device.OnPlatform(
                        "Images/header_events.png",
                        "header_events.png",
                        "Images/header_events.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            var forms = new Grid
            {
                Padding = new Thickness(10, 0, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            var nameLb = new Label { Text = " Name", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End};
            NameEntry = new Entry { FontSize = 12 };
            forms.Children.Add(nameLb, 0, 0);
            forms.Children.Add(NameEntry, 0, 1);

            var descrLb = new Label { Text = " Description", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            DescrEntry = new Entry { FontSize = 12 };
            forms.Children.Add(descrLb, 0, 2);
            forms.Children.Add(DescrEntry, 0, 3);

            var data = new Label { Text = " Select a date", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            DataPicker = new DatePicker
            {
                MinimumDate = DateTime.Now.Date
            };
            forms.Children.Add(data, 0, 4);
            forms.Children.Add(DataPicker, 0, 5);

            var startTime = new Label { Text = " Select a start time", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            StartTimePicker = new TimePicker();
            forms.Children.Add(startTime, 0, 6);
            forms.Children.Add(StartTimePicker, 0, 7);

            var endTime = new Label { Text = " Select a end time", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            EndTimePicker = new TimePicker();
            forms.Children.Add(endTime, 0, 8);
            forms.Children.Add(EndTimePicker, 0, 9);

            var div = new Label { Text = " Select division", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            //DivPicker = new Picker();
            DivPicker.SelectedIndexChanged += (sender, args) =>
            {
                DevPicker.Items.Clear();
                var division = Divisions[DivPicker.SelectedIndex];

                foreach (var device in division.devices)
                    DevPicker.Items.Add(device.Name);
                
            };
            forms.Children.Add(div, 0, 10);
            forms.Children.Add(DivPicker, 0, 11);

            var dev = new Label { Text = " Select device", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            DevPicker = new Picker();
            DevPicker.SelectedIndexChanged += (sender, args) =>
            {
                var ourPickedDevice = DevPicker.Items[DevPicker.SelectedIndex];
                StatePicker.Items.Clear();
                if (ourPickedDevice == "YaleLock")
                {
                    StatePicker.Items.Add("Open");
                    StatePicker.Items.Add("Close");
                }
                else if (ourPickedDevice == "Philips Hue")
                {
                    StatePicker.Items.Add("Turn On");
                    StatePicker.Items.Add("Turn Off");
                }
                else
                {
                    DisplayAlert("Device", "Sorry, you can't create an event for this device", "OK");
                }
            };
            forms.Children.Add(dev, 0, 12);
            forms.Children.Add(DevPicker, 0, 13);

            var state = new Label { Text = " State", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            StatePicker = new Picker();
            forms.Children.Add(state, 0, 14);
            forms.Children.Add(StatePicker, 0, 15);

            var saveButton = new Button
            {
                Text = "SAVE",
                FontFamily = "Roboto",
                FontSize = 18,
                VerticalOptions = LayoutOptions.End
            };
            saveButton.Clicked += OnSave;
            forms.Children.Add(saveButton, 0, 16);


            var scroll = new ScrollView
            {
                Content = forms
            };

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    scroll
                }
            };
        }

        public async void DivisionData()
        {
            var json = await WebServicesData.SyncTask("GET", "division");

            var index = 0;
            while (index < json.Count)
            {
                var jsonvalue = json[index];
                var id = jsonvalue["idDivision"];
                var divisionName = jsonvalue["divisionName"];
                var typeDivision = jsonvalue["typeDivision"];
                var beaconId = jsonvalue["BeaconId"];
                
                var division = new Division(
                    id,
                    divisionName,
                    typeDivision,
                    beaconId.ToString()
                );
                Divisions.Add(division);
                DivPicker.Items.Add(division.Name);
                DevicesData(division.Id, division);

                index++;
            }
            Construtor();
        }

        public async void DevicesData(int id, Division division)
        {
            var json = await WebServicesData.SyncTask("GET", "ActuatorsDevicesByDivision", id);

            var index = 0;

            while (index < json.Count)
            {
                var result = json[index];
                var idDevice = result["idDevice"];
                var idActuator = result["idActuator"];
                var deviceName = result["deviceName"];
                var actuatorName = result["actuatorName"];

                var device = new Devices(
                    idDevice,
                    idActuator,
                    deviceName,
                    actuatorName
                );

                division.AddDivice(device);
                index++;
            }
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
                return false;
            if (string.IsNullOrEmpty(DescrEntry.Text))
                return false;
            return true;
        }

        private async void OnSave(object sender, EventArgs args)
        {
            var dateTime = DataPicker.Date;
            var time = StartTimePicker.Time;
            var timeF = StartTimePicker.Time;
            //var dd = _d.Day;
            var ourPickedState = StatePicker.Items[StatePicker.SelectedIndex];

            var state = 0;
            switch (ourPickedState)
            {
                case "Open":
                    state = 1;
                    break;
                case "Close":
                    state = 0;
                    break;
                case "Turn On":
                    state = 1;
                    break;
                case "Turn Off":
                    state = 0;
                    break;
            }

            var now = DateTime.Now.ToLocalTime();

            var dayOfWeek = dateTime.DayOfWeek;
            var month = dateTime.Month;
            var dayOfMonth = dateTime.Day;
            var hours = time.Hours;
            var minutes = time.Minutes;
            var year = dateTime.Year;


            var DayOfWeek = 0;
            switch (dayOfWeek.ToString())
            {
                case "Monday":
                    DayOfWeek = 1;
                    break;
                case "Tuesday":
                    DayOfWeek = 2;
                    break;
                case "Wednesday":
                    DayOfWeek = 3;
                    break;
                case "Thursday":
                    DayOfWeek = 4;
                    break;
                case "Friday":
                    DayOfWeek = 5;
                    break;
                case "Saturday":
                    DayOfWeek = 6;
                    break;
                case "Sunday":
                    DayOfWeek = 7;
                    break;
            }

            var deviceId = Divisions[DivPicker.SelectedIndex].devices[DevPicker.SelectedIndex].Id;
            var actuatorId = Divisions[DivPicker.SelectedIndex].devices[DevPicker.SelectedIndex].ActuatorID;
            var dateF = 0 + " " + minutes + " " + hours + " " + dayOfMonth + " " + month + " " + DayOfWeek;
            var dateE = year + "-" + month + "-" + dayOfMonth;

            if (IsValid())
            {
                await WebServicesData.SyncTask("POST", "insertEvent", "toggleDevice", 1, NameEntry.Text,
                DescrEntry.Text, dateE, time, timeF,
                deviceId, actuatorId, state, dateF);
                await DisplayAlert("Success", "You are registered!", "Ok");
                await Navigation.PushAsync(new ListEventPage());
            }
        }

        public class Devices
        {
            public Devices(int id, int actuatorID, string name, string type)
            {
                Id = id;
                ActuatorID = actuatorID;
                Name = name;
                Type = type;
            }

            public int Id { get; set; }
            public int ActuatorID { get; set; }
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
