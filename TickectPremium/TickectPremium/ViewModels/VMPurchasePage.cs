using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TickectPremium.Models;

namespace TickectPremium.ViewModels
{
    public class VMPurchasePage : INotifyPropertyChanged
    {
        public Match Match { get; set; }
        //public List<MatchSeatingArea> MatchSeatingAreas { get; set; }
        public ObservableCollection<DetailTicket> DetailTickets { get; set; }

        private double _totalPriceTicket;
        public double TotalPriceTicket
        {
            get { return _totalPriceTicket; }
            set
            {
                if (_totalPriceTicket != value)
                {
                    _totalPriceTicket = value;
                    OnPropertyChanged(nameof(TotalPriceTicket));
                }
            }
        }

        public VMPurchasePage() { }
        public VMPurchasePage(Match _match,List<MatchSeatingArea> _selectedItems, List<BuyTicket> buyTickets)
        {
            Match = _match;
            this.DetailTickets = GetDetailTickets(_selectedItems);
            this.TotalPriceTicket = DetailTickets.Sum(ticket => ticket.TotalPrice);

        }

        private ObservableCollection<DetailTicket> GetDetailTickets(List<MatchSeatingArea> _selectedItems)
        {
            ObservableCollection<DetailTicket> detailTickets = new ObservableCollection<DetailTicket>();

            foreach (MatchSeatingArea _matchSeatingArea in _selectedItems)
            {
                detailTickets.Add(new DetailTicket
                {
                    Id = _matchSeatingArea.Id,
                    Price = _matchSeatingArea.Price,
                    Quantity = _matchSeatingArea.entry,
                    SeatingArea = _matchSeatingArea.SeatingArea,
                    TotalPrice = _matchSeatingArea.Price * _matchSeatingArea.entry
                });
            }
            return detailTickets;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
