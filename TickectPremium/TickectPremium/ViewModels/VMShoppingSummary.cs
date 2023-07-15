using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TickectPremium.Controllers;
using TickectPremium.Models;

namespace TickectPremium.ViewModels
{
    public class VMShoppingSummary
    {
        private MatchController matchController;

        public List<ShoppingSummaryDTO> Items { get; set; }

        public VMShoppingSummary()
        {
            matchController = new MatchController();
            Items = matchController.GetDistinctBillCodes(AppData.LoggedInUser.IDUser);
        }

    }

}
