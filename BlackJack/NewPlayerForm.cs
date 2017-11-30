using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class NewPlayerForm : Form
    {
        public event AddNameEventHandler AddNameEvent;

        public NewPlayerForm()
        {
            InitializeComponent();
        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            String playerName = Tb_name.Text;
            if (String.IsNullOrEmpty(playerName))
            {
                MessageBox.Show("Your name can not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                NameEventArgs args = new NameEventArgs(Tb_name.Text);
                AddNameEvent(this, args);
                Close();
            }
        }

    }
}
