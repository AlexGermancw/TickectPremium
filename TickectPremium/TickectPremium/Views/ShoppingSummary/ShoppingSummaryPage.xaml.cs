using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectPremium.Controllers;
using TickectPremium.Models;
using TickectPremium.ViewModels;
using TickectPremium.Views.ShoppingSummary;
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
            var selectedMatch = (sender as Frame)?.BindingContext as ShoppingSummaryDTO;
            if (selectedMatch != null)
            {
                await Navigation.PushAsync(new InvoiceSummaryPage(selectedMatch));
            }
        }

    }
}