using Rg.Plugins.Popup.Services;
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

        private async void btnAddTicket_Clicked(object sender, EventArgs e)
        {
            var match = (BindingContext as VMPurchasePage)?.Match;

            var seatingAreaPage = new SeatingAreaPage(match);

            // Suscribirse al evento SeatingAreaAdded
            seatingAreaPage.SeatingAreaAdded += OnSeatingAreaAdded;

            await PopupNavigation.Instance.PushAsync(seatingAreaPage);
        }

        private void OnSeatingAreaAdded(object sender, MatchSeatingArea seatingArea)
        {
            if (ViewModel != null)
            {
                DetailTicket detailTicket = new DetailTicket
                {
                    Id = seatingArea.Id,
                    Price = seatingArea.Price,
                    Quantity = seatingArea.entry,
                    SeatingArea = seatingArea.SeatingArea,
                    TotalPrice = seatingArea.Price * seatingArea.entry
                };

                var existingDetailTicket = ViewModel.DetailTickets.FirstOrDefault(item => item.Id == seatingArea.Id);

                if (existingDetailTicket != null)
                {
                    ViewModel.DetailTickets.Remove(existingDetailTicket);
                    var quantity = existingDetailTicket.Quantity + seatingArea.entry;
                    existingDetailTicket.Quantity = quantity;
                    existingDetailTicket.TotalPrice = quantity * seatingArea.Price;
                    ViewModel.DetailTickets.Add(existingDetailTicket);
                }
                else
                {
                    ViewModel.DetailTickets.Add(detailTicket);
                }

                ViewModel.TotalPriceTicket = ViewModel.DetailTickets.Sum(ticket => ticket.TotalPrice);

            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (PopupNavigation.Instance.PopupStack.Any())
            {
                PopupNavigation.Instance.PopAsync();
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}