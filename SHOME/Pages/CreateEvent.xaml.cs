using System;
using System.Collections.Generic;
using SHOME.Data;

using Xamarin.Forms;

namespace SHOME
{
	public partial class CreateEvent : ContentPage
	{
		public static List<Division> Divisions { get; set; } = new List<Division>();


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




		//TODO se fizer sentido e for mais prático fazes aqui o foreach em vez de mandar o iterator.
		public async void DivisionData()
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
				DivisionPicker.Items.Add(division.Name);
				DevicesData(division.Id, division);
				//var deviceLock = new Devices("lock", "lock");
				//var deviceLights = new Devices("Luzes", "lights");
				//var deviceCctv = new Devices("Camaras", "cctv");

				aux++;
				//TODO FOREACH para adicionar devices
			}



		}

		public async void DevicesData(int id, Division division)
		{
			var json = await WebServicesData.SyncTask("GET", "ActuatorsDevicesByDivision", id);
			var size = json.Count;
			var aux = 0;

			while (size > aux)
			{
				var result = json[aux];

				var device = new Devices(
					result["idDevice"],
					result["idActuator"],
					result["deviceName"],
					result["actuatorName"]
				);

				division.AddDivice(device);
				aux++;
			}


		}
		public CreateEvent()
		{
			//public List<Division> Divisionss = ContentMenu.get
			DivisionData();
			InitializeComponent();

			DivisionPicker.SelectedIndexChanged += (sender, args) =>
			{
				var ourPickedItem = DivisionPicker.Items[DivisionPicker.SelectedIndex];
				var division = Divisions[DivisionPicker.SelectedIndex];

				DevicePicker.Items.Clear();

				foreach (var device in division.devices)
				{
					DevicePicker.Items.Add(device.Name);
				}


				//DisplayAlert("Power", ourPickedItem, "OK");
				//var teste  = Divisions[ourPickedItem];
			};
			// Falta criar um acesso à bd
			// Buscar todas as divisões
			//DivisionPicker.Items.Add("Cozinha");
			//DivisionPicker.Items.Add("Quarto Principal");
			//DivisionPicker.Items.Add("Sala");
			//DivisionPicker.Items.Add("Entrada");

			// Buscar todas as aparelhos que estão em cada divisão
			//DevicePicker.Items.Add("Luzes");
			//DevicePicker.Items.Add("Fechadura");
		}







		public async void GetTemperature()
		{

			var aux = 0;
			var json = await WebServicesData.SyncTask("GET", "division");
			var size = json.Count;



		}




		private bool IsValid()
		{
			if (string.IsNullOrEmpty(entName.Text))
			{
				return false;
			}

			if (string.IsNullOrEmpty(entDescription.Text))
			{
				return false;
			}

			return true;
		}


		private void OnBindingContextChanged(object sender, EventArgs eventArgs)
		{
			var dateTime = DataPicker;
			var _d = dateTime.Date;

		}




		void OnSave(object sender, EventArgs args)
		{
			var dateTime = DataPicker.Date;
			var Time = TimePickeri.Time;
			var TimeF = TimePickerf.Time;
			//var dd = _d.Day;



			DateTime now = DateTime.Now.ToLocalTime();

			//var _DayOfWeek = now.DayOfWeek;
			//var _ss = now.Month.ToString();
			//var dds = now.DayOfYear;

			var _DayOfWeek = dateTime.DayOfWeek;
			var _Month = dateTime.Month;
			var _DayOfMonth = dateTime.Day;
			var hours = Time.Hours;
			var minutes = Time.Minutes;
			var year = dateTime.Year;




			var DayOfWeek = 0;

			switch (_DayOfWeek.ToString())
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

			var DeviceID = Divisions[DivisionPicker.SelectedIndex].devices[DevicePicker.SelectedIndex].Id;
			var ActuatorID = Divisions[DivisionPicker.SelectedIndex].devices[DevicePicker.SelectedIndex].ActuatorID;
			var dateF = 0 + " " + minutes + " " + hours + " " + _DayOfMonth + " " + _Month + " " + DayOfWeek;
			var datee = year + "-" + _Month + "-" + _DayOfMonth;


			WebServicesData.SyncTask("POST", "insertEvent", 1, entName.Text, entDescription.Text, datee, Time, TimeF, DeviceID, ActuatorID, 1, dateF);


			DisplayAlert("Power", _DayOfWeek.ToString(), "OK");

			if (IsValid())
			{
				var name = lblName;
				var description = lblDescription;
				var date = lblData;
				var timei = TimePickeri;
				var timef = TimePickerf;
				var division = DivisionPicker;
				var device = DevicePicker;




				//	var t = DateTime.Parse(timei);
				try
				{

					DisplayAlert("Success", "You are registered!", "Ok");
					Navigation.PushAsync(new ListEventPage());
				}
				catch (Exception e)
				{
					DisplayAlert("Error", "Error on saving the registry", "Ok");
				}
			}

		}
	}
}
