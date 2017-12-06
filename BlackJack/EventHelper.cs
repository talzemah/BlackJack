using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // Add new name player event.
    public delegate void AddNameEventHandler(object sender, NameEventArgs e);

    public class NameEventArgs : EventArgs
    {
        public NameEventArgs(String newName)
        {
            PlayerName = newName;
        }

        public String PlayerName { get; set; }
    }

    // new deal event
    public delegate void DealEventHandler(object sender, EventArgs e);

    // Player has finish to play event
    public delegate void PlayerFinishToPlayEventHandler(object sender, EventArgs e);


}
