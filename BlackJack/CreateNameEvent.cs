using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public delegate void AddNameEventHandler(object sender, NameEventArgs e);

    public class NameEventArgs : EventArgs
    {
        public NameEventArgs(String newName)
        {
            PlayerName = newName;
        }

        public String PlayerName
        {
            get; set;
        }
    }


}
