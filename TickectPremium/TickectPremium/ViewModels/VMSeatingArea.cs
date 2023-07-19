using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TickectPremium.Models;
using Xamarin.Forms;
using TickectPremium.Resources.Test;
using TickectPremium.Controllers;

namespace TickectPremium.ViewModels
{
    public class VMSeatingArea : INotifyPropertyChanged
    {
        #region VARIABLES

        private MatchController matchController;
        private MatchSeatingArea _selectedSeatingArea;
        public Match Match { get; set; }

        private ObservableCollection<MatchSeatingArea> _matchSeatingAreas;
        public ObservableCollection<MatchSeatingArea> MatchSeatingAreas
        {
            get { return _matchSeatingAreas; }
            set
            {
                if (_matchSeatingAreas != value)
                {
                    _matchSeatingAreas = value;
                    OnPropertyChanged(nameof(MatchSeatingAreas));
                }
            }
        }
        public MatchSeatingArea SelectedSeatingArea
        {
            get { return _selectedSeatingArea; }
            set
            {
                if (_selectedSeatingArea != value)
                {
                    _selectedSeatingArea = value;
                    OnPropertyChanged(nameof(SelectedSeatingArea));
                }
            }
        }
        
        #endregion VARIABLES


        public VMSeatingArea() { }

        public VMSeatingArea( Match match)
        {
            matchController = new MatchController();
            Match = match;
            MatchSeatingAreas = matchController.MatchSeatingAreaList(match.Id);
            //MatchSeatingAreas = DataTest.GetMatchSeatingAreas();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
                
    }
}
