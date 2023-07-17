using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TickectPremium.Controllers;
using TickectPremium.Models;
using TickectPremium.Resources;
using TickectPremium.Resources.Test;
using Xamarin.Forms;

namespace TickectPremium.ViewModels
{
    public class VMMatches
    {

        private MatchController matchController;
        private ObservableCollection<Match> matches;
        public ObservableCollection<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }

        public VMMatches()
        {
            matchController = new MatchController();
            //Matches = matchController.MatchList();
            Matches = DataTest.GetMatches();

        }        

    }
}

