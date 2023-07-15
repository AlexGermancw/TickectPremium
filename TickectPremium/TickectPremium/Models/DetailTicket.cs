using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Models
{
    public class DetailTicket
    {
        public int Id { get; set; }
        public SeatingArea SeatingArea { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

    }
}
