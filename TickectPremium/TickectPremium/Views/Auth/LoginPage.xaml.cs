using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Controllers;
using TickectPremium.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region VARIABLES
        public static MasterDetailPage MasterDet { get; set; }

        UserController userController = new UserController();

        #endregion VARIABLES

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            RegisterLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnRegisterLabelTapped)
            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (AppData.LoggedInUser != null)
            {
                Navigation.PushAsync(new MainPage());
            }
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            bool answer = false;

            if(username != null && password != null)
            {
                answer = userController.Authenticate(new LoginModel { Username = username, Password = password });
            }

            if (answer && AppData.LoggedInUser!=null)
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                ErrorMessageLabel.IsVisible = true;
            }
            
        }

        private void OnRegisterLabelTapped()
        {
            Navigation.PushAsync(new SignupPage());
        }
    }
}