using System;
using TickectPremium.Views.Auth;
using TickectPremium.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TickectPremium
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new SeatingAreaPage(null));
            //MainPage = new NavigationPage(new PrincipalPage());


        }

        protected override void OnStart()
        {
            // Inicializar Rg.Plugins.Popup
            //Rg.Plugins.Popup.Init();

            // Resto de código y configuraciones adicionales
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
