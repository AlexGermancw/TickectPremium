using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TickectPremium.Models
{
    public class MatchSeatingArea : INotifyPropertyChanged
    {
        private int _entry;
        public int entry
        {
            get { return _entry; }
            set
            {
                if (_entry != value)
                {
                    _entry = value;
                    OnPropertyChanged(nameof(entry));
                }
            }
        }

        public int Id { get; set; }
        public int TotalTickets { get; set; }
        public double Price { get; set; }
        public int IdMatch { get; set; }
        public SeatingArea SeatingArea { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
