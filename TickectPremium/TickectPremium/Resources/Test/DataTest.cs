using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TickectPremium.Models;

namespace TickectPremium.Resources.Test
{
    public static class DataTest
    {

        public static ObservableCollection<MatchSeatingArea> GetMatchSeatingAreas()
        {
            ObservableCollection<MatchSeatingArea> matchSeatingAreas = new ObservableCollection<MatchSeatingArea>();

            matchSeatingAreas.Add(new MatchSeatingArea
            {
                Id = 1,
                TotalTickets = 100,
                Price = 10.99,
                IdMatch = 1,
                SeatingArea = new SeatingArea { Id = 1, Name = "VIP" }
            });

            matchSeatingAreas.Add(new MatchSeatingArea
            {
                Id = 2,
                TotalTickets = 200,
                Price = 7.99,
                IdMatch = 1,
                SeatingArea = new SeatingArea { Id = 2, Name = "General" }
            });

            matchSeatingAreas.Add(new MatchSeatingArea
            {
                Id = 3,
                TotalTickets = 50,
                Price = 15.99,
                IdMatch = 2,
                SeatingArea = new SeatingArea { Id = 3, Name = "Premium" }
            });

            matchSeatingAreas.Add(new MatchSeatingArea
            {
                Id = 4,
                TotalTickets = 150,
                Price = 9.99,
                IdMatch = 2,
                SeatingArea = new SeatingArea { Id = 4, Name = "Balcony" }
            });

            matchSeatingAreas.Add(new MatchSeatingArea
            {
                Id = 5,
                TotalTickets = 80,
                Price = 12.99,
                IdMatch = 3,
                SeatingArea = new SeatingArea { Id = 5, Name = "Upper Deck" }
            });

            return matchSeatingAreas;
        }

        public static ObservableCollection<Match> GetMatches()
        {
            ObservableCollection<Match> matches = new ObservableCollection<Match>();

            matches.Add(new Match
            {
                Id = 1,
                LocalTeam = "Equipo Local 1",
                VisitingTeam = "Equipo Visitante 1",
                DateMatch = DateTime.Now.Date,
                Hour = new TimeSpan(14, 30, 0),
                Place = "Estadio A",
                TotalTickets = 1000
            });

            matches.Add(new Match
            {
                Id = 2,
                LocalTeam = "Equipo Local 2",
                VisitingTeam = "Equipo Visitante 2",
                DateMatch = DateTime.Now.Date,
                Hour = new TimeSpan(18, 0, 0),
                Place = "Estadio B",
                TotalTickets = 500
            });

            // Agrega más partidos según tus necesidades

            return matches;
        }
    }
}
