using System;
using System.Linq;
using Xamarin.Forms;
using SHOME.Data;

namespace SHOME
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

	
        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

			// Sign up logic goes here
			bool signUpSucceeded;
			if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) &&
			   !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"))
			{
					var json = await WebServicesData.SyncTask("POST", "insertuser", user.Username, user.Email, user.Password, token.Text , "2017-01-18", "B", "0", "2017-01-31", "1", "1");

				if (json == "Inserido")
					signUpSucceeded = true;
				if(json == "Inserido")
					signUpSucceeded = false;
				else
					signUpSucceeded = false;
			}
			else
			{
				signUpSucceeded = false;
			}




        
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MenuPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
				DisplayAlert("Sign", "Sign up failed", "Ok");
              
            }
        }

		private async void AreDetailsValid(User user)
        {
			
			
				var json = await WebServicesData.SyncTask("POST", "insertuser", user.Username, user.Email, user.Password, "1234656789", "2017-01-18", "B", "0", "2017-01-31", "1","1");
			
        }
    }
}