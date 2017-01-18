using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SHOME
{
    public partial class CreateEvent : ContentPage
    {
        public CreateEvent()
        {
            InitializeComponent();

            // Falta criar um acesso à bd
            // Buscar todas as divisões
            DivisionPicker.Items.Add("Cozinha");
            DivisionPicker.Items.Add("Quarto Principal");
            DivisionPicker.Items.Add("Sala");
            DivisionPicker.Items.Add("Entrada");

            // Buscar todas as aparelhos que estão em cada divisão
            DevicePicker.Items.Add("Luzes");
            DevicePicker.Items.Add("Fechadura");
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
        void OnSave(object sender, EventArgs args)
        {
            if (IsValid())
            {
                var name = lblName;
                var description = lblDescription;
                var date = lblData;
                var timei = TimePickeri;
                var timef = TimePickerf;
                var division = DivisionPicker;
                var device = DevicePicker;

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

