using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Models;
using TickectPremium.Resources;
using TickectPremium.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPage : ContentPage
    {
        public VMMatches ViewModel { get; set; }

        public PrincipalPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new VMMatches();
            BindingContext = ViewModel;            
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var match = frame?.BindingContext as Match;
            List<MatchSeatingArea> seatingAreas = new List<MatchSeatingArea>();
            if (match != null)
            {
                await Navigation.PushAsync(new PurchasePage(match,seatingAreas,null,true));
            }
        }
    }
}
