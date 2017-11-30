using System;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class MainMenuForm : Form
    {
        GameForm gameForm1;
        GameForm gameForm2;
        GameForm gameForm3;


        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void Btn_newGame_Click(object sender, EventArgs e)
        {

            gameForm1 = new GameForm();
            gameForm2 = new GameForm();
            gameForm3 = new GameForm();

            /// Hide();

            gameForm1.Show();
            gameForm2.Show();
            gameForm3.Show();
            /// Show();

        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
