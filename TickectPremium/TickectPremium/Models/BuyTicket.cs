using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Models
{
    public class BuyTicket
    {
        public int Id { get; set; }
        public int IdMatch { get; set; }
        public SeatingArea seatingArea { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public int IdUser { get; set; }
        public int IdBill { get; set; }
    }
}
