using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TickectPremium.Models;
using TickectPremium.Services;

namespace TickectPremium.Controllers
{
    public class MatchController
    {
        private MatchService matchService;

        public MatchController()
        {
            this.matchService = new MatchService();
        }

        public ObservableCollection<Match> MatchList()
        {
            return this.matchService.MatchList();
        }

        public ObservableCollection<MatchSeatingArea> MatchSeatingAreaList(int _idMatch)
        {
            return this.matchService.MatchSeatingAreaList(_idMatch);
        }

        public int InsertBill(Bill bill)
        {
            return this.matchService.InsertBill(bill);
        }

        public bool InsertBuyTicket(BuyTicket buyTicket)
        {
            return this.matchService.InsertBuyTicket(buyTicket);
        }

        public List<ShoppingSummaryDTO> GetDistinctBillCodes(int idUser)
        {
            return this.matchService.GetDistinctBillCodes(idUser);
        }

        public Match GetMatchByCode(int code)
        {
            return this.matchService.GetMatchByCode(code);
        }

        public List<BuyTicket> GetBuyTicketsByInvoiceCode(int invoiceCode)
        {
            return this.matchService.GetBuyTicketsByInvoiceCode(invoiceCode);
        }
    }
}
