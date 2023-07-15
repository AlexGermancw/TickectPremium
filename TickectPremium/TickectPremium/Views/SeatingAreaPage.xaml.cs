using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Models;
using TickectPremium.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeatingAreaPage : ContentPage
    {
        private MatchSeatingArea seatingArea;

        public VMSeatingArea ViewModel { get; set; }

        public SeatingAreaPage(Match match)
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new VMSeatingArea(match);
            BindingContext = ViewModel;
        }

        private void MinusButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var seatingArea = (MatchSeatingArea)button.CommandParameter;

            if (seatingArea.entry > 0)
            {
                seatingArea.entry--;
            }
        }

        private void PlusButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var seatingArea = (MatchSeatingArea)button.CommandParameter;

            if (seatingArea.entry < seatingArea.TotalTickets)
            {
                seatingArea.entry++;
            }
        }

        private void FrameBindingContextChanged(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            seatingArea = (MatchSeatingArea)frame.BindingContext;
        }

        private void btnBuyTicket_Clicked(object sender, EventArgs e)
        {
            var selectedItems = ViewModel.MatchSeatingAreas.Where(item => item.entry > 0).ToList();
            Navigation.PushAsync(new PurchasePage(ViewModel.Match, selectedItems,null,true));
        }
    }

}