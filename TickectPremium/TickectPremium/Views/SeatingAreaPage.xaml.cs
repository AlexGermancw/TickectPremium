using Rg.Plugins.Popup.Services;
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
    public partial class SeatingAreaPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        //private MatchSeatingArea seatingArea;
        public event EventHandler<MatchSeatingArea> SeatingAreaAdded;
        public VMSeatingArea ViewModel { get; set; }

        public SeatingAreaPage(Match match)
        {
            InitializeComponent();
            ViewModel = new VMSeatingArea(match);
            BindingContext = ViewModel;
            CloseWhenBackgroundIsClicked = true;
        }

        private void MinusButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.SelectedSeatingArea != null && ViewModel.SelectedSeatingArea.entry > 0)
            {
                ViewModel.SelectedSeatingArea.entry--;
            }
        }

        private void PlusButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.SelectedSeatingArea != null && ViewModel.SelectedSeatingArea.entry < ViewModel.SelectedSeatingArea.TotalTickets)
            {
                ViewModel.SelectedSeatingArea.entry++;
            }
        }

        private void btnAddSeatingArea_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.SelectedSeatingArea != null && ViewModel.SelectedSeatingArea.entry > 0)
            {
                // Disparar el evento y pasar el objeto seleccionado como argumento
                SeatingAreaAdded?.Invoke(this, ViewModel.SelectedSeatingArea);

                // Cerrar el popup
                PopupNavigation.Instance.PopAsync();
            }
        }
    }

}