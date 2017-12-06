using System;
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
                NameEventArgs args = new NameEventArgs(playerName);
                AddNameEvent(this, args);
                Close();
            }
        }

    }
}
