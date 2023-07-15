using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Views;
using TickectPremium.Views.Auth;
using Xamarin.Forms;

namespace TickectPremium
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Master = new MenuNav();
            this.Detail = new NavigationPage(new PrincipalPage());
            LoginPage.MasterDet = this;
        }
    }
}
