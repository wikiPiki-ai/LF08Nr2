using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF08Nr2.ViewModel
{
    class CoolVacation : ObservableCollection<string>
    {
        public CoolVacation() 
        {
            Add("yourMoin");
            Add("Gluehwein");
            Add("Twilight");
            Add("kussi");
            Add("tetris");
        } 
    }
}
