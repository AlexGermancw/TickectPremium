using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string LocalTeam { get; set; }
        public string VisitingTeam { get; set; }
        public DateTime DateMatch { get; set; }
        public TimeSpan Hour { get; set; }
        public string Place { get; set; }
        public int TotalTickets { get; set; }
    }
}
