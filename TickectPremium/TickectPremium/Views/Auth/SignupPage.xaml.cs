using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Controllers;
using TickectPremium.Models;
using TickectPremium.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
        #region VARIABLES

        UserController userController;

        #endregion VARIABLES

        public SignupPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            userController = new UserController ();
            ErrorMessageLabel.Text = Constant.ErrorMessagePassword;
        }

        #region ENTRY FOCUS
        private async void OnEntryFocused(object sender, EventArgs e)
        {
            Entry entry = (Entry)sender;
            Label placeholderLabel = GetPlaceholderLabel(entry);

            if (string.IsNullOrEmpty(entry.Text))
            {
                placeholderLabel.IsVisible = true;
                await placeholderLabel.FadeTo(1, 250);
                await placeholderLabel.TranslateTo(0, 0, 250, Easing.SinOut);
            }
        }

        private async void OnEntryUnfocused(object sender, EventArgs e)
        {
            Entry entry = (Entry)sender;
            Label placeholderLabel = GetPlaceholderLabel(entry);

            if (string.IsNullOrEmpty(entry.Text))
            {
                await placeholderLabel.FadeTo(0, 250);
                await placeholderLabel.TranslateTo(0, 0, 250, Easing.SinOut);
                placeholderLabel.IsVisible = false;
                placeholderLabel.TranslationY = 0; // Restaurar la posición original en caso de reutilización
            }

            #region VALIDATE EQUALS ENTRY PASSWORDS
            if (entry == PasswordEntry || entry == RepeatPasswordEntry)
            {
                string password = PasswordEntry.Text;
                string repeatPassword = RepeatPasswordEntry.Text;
               
                ErrorMessagePasswordLabel.IsVisible = !Util.AreStringsEqual(password, repeatPassword);
            }
            #endregion
        }

        #endregion ENTRY FOCUS

        #region FUNCTIONS
        private Label GetPlaceholderLabel(Entry entry)
        {
            if (entry == NameEntry)
            {
                return PlaceholderLabelName;
            }
            else if (entry == LastNameEntry)
            {
                return PlaceholderLabelLastName;
            }
            else if (entry == EmailEntry)
            {
                return PlaceholderLabelEmail;
            }
            else if (entry == PhoneEntry)
            {
                return PlaceholderLabelPhone;
            }
            else if (entry == UserEntry)
            {
                return PlaceholderLabelUser;
            }
            else if (entry == PasswordEntry)
            {
                return PlaceholderLabelPassword;
            }
            else if (entry == RepeatPasswordEntry)
            {
                return PlaceholderLabelRepeatPassword;
            }
            else
            {
                return null;
            }
        }

        private async void Register()
        {
            UserDTO newUser = new UserDTO
            {
                Name = NameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                Username = UserEntry.Text,
                Password = PasswordEntry.Text
            };

            if (!newUser.IsNullOrEmpty() && Util.AreStringsEqual(PasswordEntry.Text,RepeatPasswordEntry.Text))
            {
                if (userController.UserRegistrer(newUser))
                {
                    await DisplayAlert("Éxito", Constant.RegistrationSuccessMessage, "Aceptar");
                    ClearEntries();
                }
                else
                {
                    await DisplayAlert("Advertencia", Constant.RegistrationErrorMessage, "Aceptar");

                }
            }
            else if (!Util.AreStringsEqual(PasswordEntry.Text, RepeatPasswordEntry.Text))
            {
                await DisplayAlert("Advertencia", Constant.ErrorMessagePassword, "Aceptar");
            }
            else
            {
                await DisplayAlert("Advertencia", Constant.ErrorMessageCompletingEntry, "Aceptar");
            }

        }

        private void ClearEntries()
        {
            NameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PhoneEntry.Text = string.Empty;
            UserEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            RepeatPasswordEntry.Text = string.Empty;
        }

        #endregion FUNCTIONS

        #region PAGE ACTIONS
        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            Register();
            await Navigation.PopAsync();
        }

        #endregion PAGE ACTIONS

    }
}