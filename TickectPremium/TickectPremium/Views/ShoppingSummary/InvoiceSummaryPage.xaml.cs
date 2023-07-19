using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TickectPremium.Models;
using TickectPremium.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TickectPremium.Views.ShoppingSummary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceSummaryPage : ContentPage
    {
        public VMInvoiceSummary ViewModel { get; set; }
    public InvoiceSummaryPage(ShoppingSummaryDTO shoppingSummary)
        {
            InitializeComponent();
            ViewModel = new VMInvoiceSummary(shoppingSummary);
            BindingContext = ViewModel;
        }
    }
}