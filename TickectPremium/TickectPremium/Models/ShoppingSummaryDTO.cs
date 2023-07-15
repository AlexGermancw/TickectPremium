using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Models
{
    public class ShoppingSummaryDTO
    {
        public int IdMatch { get; set; }
        public int IdBill { get; set; }
        public string LocalTeam { get; set; }
        public string VisitingTeam { get; set; }
    }
}
