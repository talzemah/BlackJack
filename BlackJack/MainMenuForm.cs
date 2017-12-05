using BlackJack.Logic;
using System;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class MainMenuForm : Form
    {
        private BlackJackGame game = new BlackJackGame(Properties.Settings.Default.InitBalance);

        GameForm gameForm1;
        GameForm gameForm2;
        GameForm gameForm3;


        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void Btn_newGame_Click(object sender, EventArgs e)
        {

            gameForm1 = new GameForm(game);
            gameForm2 = new GameForm(game);
            gameForm3 = new GameForm(game);

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
