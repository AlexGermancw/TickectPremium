using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TickectPremium.Models;

namespace TickectPremium.ViewModels
{
    public class VMPurchasePage
    {
        public Match Match { get; set; }
        //public List<MatchSeatingArea> MatchSeatingAreas { get; set; }
        public List<DetailTicket> DetailTickets { get; set; }

        public double TotalPriceTicket { get; set; }

        public VMPurchasePage() { }
        public VMPurchasePage(Match _match,List<MatchSeatingArea> _selectedItems, List<BuyTicket> buyTickets)
        {
            Match = _match;
            this.DetailTickets = GetDetailTickets(_selectedItems);
            this.TotalPriceTicket = DetailTickets.Sum(ticket => ticket.TotalPrice);

        }

        private List<DetailTicket> GetDetailTickets(List<MatchSeatingArea> _selectedItems)
        {
            List<DetailTicket> detailTickets = new List<DetailTicket>();

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

        private List<DetailTicket> GetDetailTicketsByBuyticket(List<BuyTicket> buyTickes)
        {
            List<DetailTicket> detailTickets = new List<DetailTicket>();

            foreach (BuyTicket _buyTicket in buyTickes)
            {
                detailTickets.Add(new DetailTicket
                {
                    Id = _buyTicket.Id,
                    Price = _buyTicket.Total/_buyTicket.Quantity,
                    Quantity = _buyTicket.Quantity,
                    SeatingArea = _buyTicket.seatingArea,
                    TotalPrice = _buyTicket.Total
                });
            }

            return detailTickets;
        }
    }
}
