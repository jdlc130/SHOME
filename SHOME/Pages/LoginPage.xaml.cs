using System;
using Xamarin.Forms;
using SHOME.Data;

namespace SHOME
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BackgroundImage = "login.png";
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

			var json = await WebServicesData.SyncTask("GET", "login", user.Username, user.Password);

            if (json == "correct")
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MenuPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }


        private bool AreCredentialsCorrect(User user)
        {
            return (user.Username == Constants.Username) && (user.Password == Constants.Password);
        }

		public async void GetUser(User user)
		{
			var aux = 0;
			var json = await WebServicesData.SyncTask("GET", "login", "Leonel", "123456");
			var size = json.Count;

			var teste = json[0];
		
		}

    }
}