using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Controllers;
using TickectPremium.Models;
using TickectPremium.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShoppingSummaryPage : ContentPage
	{
        public VMShoppingSummary ViewModel { get; set; }
        public ShoppingSummaryPage ()
		{
			InitializeComponent ();
            ViewModel = new VMShoppingSummary();
            BindingContext = ViewModel;
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var item = frame?.BindingContext as ShoppingSummaryDTO;
            MatchController matchController = new MatchController();
            if (item != null)
            {
                var match = matchController.GetMatchByCode(item.IdMatch);
                var tickets = matchController.GetBuyTicketsByInvoiceCode(item.IdBill);
                await Navigation.PushAsync(new PurchasePage(match,null,tickets,false));
            }
        }
    }
}