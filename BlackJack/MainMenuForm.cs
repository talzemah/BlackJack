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
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void Btn_newGame_Click(object sender, EventArgs e)
        {
            // Show the game form
            using (GameForm gameForm = new GameForm())
            {
                Hide();
                gameForm.ShowDialog();
                Show();
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
