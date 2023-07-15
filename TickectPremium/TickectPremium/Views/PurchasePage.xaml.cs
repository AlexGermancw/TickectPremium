using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Controllers;
using TickectPremium.Models;
using TickectPremium.Resources;
using TickectPremium.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PurchasePage : ContentPage
	{
        private MatchController matchController;
		public VMPurchasePage ViewModel { get; set; }
        public PurchasePage (Match _match,List<MatchSeatingArea> _selectedItems, List<BuyTicket> buyTickets, bool btn)
		{
			InitializeComponent ();
            //NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new VMPurchasePage(_match, _selectedItems, buyTickets);
            BindingContext = ViewModel;
            matchController = new MatchController ();
            btnPay.IsVisible = btn;
        }

        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var match = (BindingContext as VMPurchasePage)?.Match;
            Bill bill = new Bill{IdUser = AppData.LoggedInUser.IDUser, };

            int idBill = matchController.InsertBill(bill);

            var detailTickets = (BindingContext as VMPurchasePage)?.DetailTickets;

            bool resultBuy = false;

            foreach(DetailTicket detailTicket in detailTickets)
            {
                BuyTicket buyTicket = new BuyTicket
                {
                    IdMatch = match.Id,
                    seatingArea = detailTicket.SeatingArea,
                    Quantity = detailTicket.Quantity,
                    Total = detailTicket.TotalPrice,
                    IdUser = AppData.LoggedInUser.IDUser,
                    IdBill = idBill,
                };

                resultBuy = matchController.InsertBuyTicket(buyTicket);

                if (!resultBuy)
                {
                    await DisplayAlert("Advertencia", Constant.BuyTicketErrorMessage, "Aceptar");
                }

            }

            if (resultBuy)
            {
                await DisplayAlert("Advertencia", Constant.BuyTicketSuccessMessage, "Aceptar");
                await Navigation.PushAsync(new MainPage());
            }


        }
    }
}