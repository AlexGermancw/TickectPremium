using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Models;
using TickectPremium.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuNav : ContentPage
    {
        public MenuNav()
        {
            InitializeComponent();
        }

        private  async void btnBuy_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShoppingSummaryPage());
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            AppData.LoggedInUser = null;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}