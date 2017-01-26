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
            DivPicker = new Picker(); //Cria o picker de divisões

            DivisionData();
        }

        public static List<Division> Divisions { get; set; } = new List<Division>(); 

        private void Construtor()
        {
            // Imagens dispostas na página
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

            // O 

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
            //Label do Nome
            var nameLb = new Label { Text = " Name", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End};
            NameEntry = new Entry { FontSize = 12 };
            forms.Children.Add(nameLb, 0, 0);
            forms.Children.Add(NameEntry, 0, 1);

            //Label da descriçao 
            var descrLb = new Label { Text = " Description", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            DescrEntry = new Entry { FontSize = 12 };
            forms.Children.Add(descrLb, 0, 2);
            forms.Children.Add(DescrEntry, 0, 3);

            //Label para selecionar a data
            var data = new Label { Text = " Select a date", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            //Picker para escolher a data
            DataPicker = new DatePicker
            {
                MinimumDate = DateTime.Now.Date // a data min é a data de hoje
            };
            forms.Children.Add(data, 0, 4);
            forms.Children.Add(DataPicker, 0, 5);

            //Label para selecionar a hora de inicio
            var startTime = new Label { Text = " Select a start time", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            //Picker para inserir a hora de inicio
            StartTimePicker = new TimePicker();
            forms.Children.Add(startTime, 0, 6);
            forms.Children.Add(StartTimePicker, 0, 7);

            //Label para selecionar a hora de fim
            var endTime = new Label { Text = " Select a end time", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            //Picker para inserir a hora de fim
            EndTimePicker = new TimePicker();
            forms.Children.Add(endTime, 0, 8);
            forms.Children.Add(EndTimePicker, 0, 9);

            // div é a label que apresenta o texto de informação
            var div = new Label { Text = " Select division", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            
            // se o picker das divisões for selecionado
            DivPicker.SelectedIndexChanged += (sender, args) =>
            {
                // Inicialmente ele limpa os valores todos que possam estar no picker dos devices
                DevPicker.Items.Clear();
                // Vai buscar a divisão selecionada pelo utilizador
                var division = Divisions[DivPicker.SelectedIndex];
                // Adiciona ao picker dos devices todos os devices que se encontram na divisão selecionada anteriormente
                foreach (var device in division.devices)
                    DevPicker.Items.Add(device.Name);
                
            };
            forms.Children.Add(div, 0, 10);
            forms.Children.Add(DivPicker, 0, 11);

            //Label que apresenta o texto de informação 
            var dev = new Label { Text = " Select device", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            // Cria um picker para os devices
            DevPicker = new Picker();
            //Quando é selecionado o device
            DevPicker.SelectedIndexChanged += (sender, args) =>
            {
                // guarda o valor do device escolhido
                var ourPickedDevice = DevPicker.Items[DevPicker.SelectedIndex];
                // limpa os valores que possam existir no picker do state 
                StatePicker.Items.Clear();
                // Se o picker device for uma fechadura
                if (ourPickedDevice == "YaleLock")
                {
                    // Estados que são apresentados
                    StatePicker.Items.Add("Open");
                    StatePicker.Items.Add("Close");
                }
                // Se o picker device for luzes
                else if (ourPickedDevice == "Philips Hue")
                {
                    //estados que são apresentados
                    StatePicker.Items.Add("Turn On");
                    StatePicker.Items.Add("Turn Off");
                }
                else
                {
                    // no caso em que seja escolhido um device que não seja possivel agendar um evento
                    DisplayAlert("Device", "Sorry, you can't create an event for this device", "OK");
                }
            };
            forms.Children.Add(dev, 0, 12);
            forms.Children.Add(DevPicker, 0, 13);

            // Label que dá informação sobre o que é o picker
            var state = new Label { Text = " State", FontSize = 14, FontFamily = "Roboto", VerticalTextAlignment = TextAlignment.End };
            // Cria o picker do state 
            StatePicker = new Picker();
            forms.Children.Add(state, 0, 14);
            forms.Children.Add(StatePicker, 0, 15);

            // Criação do botão de salvar
            var saveButton = new Button
            {
                Text = "SAVE",
                FontFamily = "Roboto",
                FontSize = 18,
                VerticalOptions = LayoutOptions.End
            };
            // Quando carregado vai para a função OnSave
            saveButton.Clicked += OnSave;
            forms.Children.Add(saveButton, 0, 16);

            // permite o scroll na página
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

        // Obter as divisões dos webservices
        public async void DivisionData()
        {
            // Get as divisões
            var json = await WebServicesData.SyncTask("GET", "division");

            var index = 0;
            //Enquanto o nº de divisões for maior que 0 retornará os dados seguintes de cada divisão
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
                //Adiciona ao picker da divisao o nome da divisão
                DivPicker.Items.Add(division.Name);
                //Leva para a funcao dos dados dos devices o id da divisao 
                //e a divisao para saber os devices que lá estão
                DevicesData(division.Id, division);

                index++;
            }
            Construtor();
        }
        //// Obter as devices dos webservices tendo em conta a divisão que recebeu
        public async void DevicesData(int id, Division division)
        {
            // Get os devices por divisao 
            var json = await WebServicesData.SyncTask("GET", "ActuatorsDevicesByDivision", id);
            
            var index = 0;
            // Enquanto o numero de devices for maior que 0 retornara os dados seguintes do device 
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
        // Verificacao se os campos estao vazios
        private bool IsValid()
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
                return false;
            if (string.IsNullOrEmpty(DescrEntry.Text))
                return false;
            return true;
        }

        //Funcao que executa quando é selecionado o botao de guardar
        private async void OnSave(object sender, EventArgs args)
        {
            //variavel que guarda o valor introduzido no picker da data
            var dateTime = DataPicker.Date;
            //variavel que guarda o valor que inicia timepicker 
            var time = StartTimePicker.Time;
            // variavel que guarda o valor que termina timepicker
            var timeF = StartTimePicker.Time;
            //variavel que guarda o valor do estado selecionado
            var ourPickedState = StatePicker.Items[StatePicker.SelectedIndex];

            var state = 0;
            //determina o valor do state dependendo do valor que foi selecionado no statePicker
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
            
            //var now = DateTime.Now.ToLocalTime();
            
            var dayOfWeek = dateTime.DayOfWeek; //dia da semana
            var month = dateTime.Month; //mês
            var dayOfMonth = dateTime.Day; //dia do mês
            var hours = time.Hours; //horas
            var minutes = time.Minutes; //minutos
            var year = dateTime.Year; //ano


            var DayOfWeek = 0;
            // ajuda a tornar o dia da semana num inteiro
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
            // guarda o id do device escolhido
            var deviceId = Divisions[DivPicker.SelectedIndex].devices[DevPicker.SelectedIndex].Id;
            // guarda o id do actuator 
            var actuatorId = Divisions[DivPicker.SelectedIndex].devices[DevPicker.SelectedIndex].ActuatorID;
            //dataF retorna a data no formato: segundos minutos hours diadomes mes diadasemana
            var dateF = 0 + " " + minutes + " " + hours + " " + dayOfMonth + " " + month + " " + DayOfWeek;
            //dateE retorna a data no formato ano-mes-dia
            var dateE = year + "-" + month + "-" + dayOfMonth;

            //Se for válido é enviado um pedido ao servidor para inserir o evento
            if (IsValid())
            {
                await WebServicesData.SyncTask("POST", "insertEvent", "toggleDevice", 1, NameEntry.Text,
                DescrEntry.Text, dateE, time, timeF,
                deviceId, actuatorId, state, dateF);

                // se for inserido com sucesso é apresentado um alerta 
                await DisplayAlert("Success", "You are registered!", "Ok");
                //await Navigation.PushAsync(new ListEventPage());
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
