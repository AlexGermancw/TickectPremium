using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TickectPremium.Controllers;
using TickectPremium.Models;

namespace TickectPremium.ViewModels
{
    public class VMInvoiceSummary
    {
        private MatchController MatchController;
        public Match Match { get; set; }
        public ObservableCollection<DetailTicket> DetailTickets { get; set; }
        public double TotalPriceTicket { get; set; }

        public VMInvoiceSummary() { }

        public VMInvoiceSummary(ShoppingSummaryDTO shoppingSummary)
        {
            MatchController = new MatchController();
            Match = MatchController.GetMatchByCode(shoppingSummary.IdMatch);
            this.DetailTickets = GetDetailTicketsByBuyticket(shoppingSummary.IdBill);
            this.TotalPriceTicket = DetailTickets.Sum(ticket => ticket.TotalPrice);
        }

        private ObservableCollection<DetailTicket> GetDetailTicketsByBuyticket(int invoiceCode)
        {
            var ticketList = MatchController.GetBuyTicketsByInvoiceCode(invoiceCode);

            ObservableCollection<DetailTicket> detailTickets = new ObservableCollection<DetailTicket>();

            foreach (BuyTicket _ticket in ticketList)
            {
                detailTickets.Add(new DetailTicket
                {
                    Id = _ticket.Id,
                    Price = _ticket.Total / _ticket.Quantity,
                    Quantity = _ticket.Quantity,
                    SeatingArea = _ticket.seatingArea,
                    TotalPrice = _ticket.Total
                });
            }

            return detailTickets;
        }
    }
}
