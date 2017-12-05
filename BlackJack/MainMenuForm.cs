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

            gameForm1 = new GameForm(game, UpdateUINames);
            //gameForm1.NewPlayerForm.AddNameEvent += UpdateUINames;

            gameForm2 = new GameForm(game, UpdateUINames);
            //gameForm2.NewPlayerForm.AddNameEvent += UpdateUINames;

            gameForm3 = new GameForm(game, UpdateUINames);
            //gameForm3.NewPlayerForm.AddNameEvent += UpdateUINames;

            /// Hide();

            gameForm1.Show();
            gameForm2.Show();
            gameForm3.Show();

            /// Show();

        }

        private void UpdateUINames(object sender, NameEventArgs e)
        {
            if (gameForm1 != null)
                gameForm1.UpdatePlayersName();
            if (gameForm2 != null)
                gameForm2.UpdatePlayersName();
            if (gameForm3 != null)
                gameForm3.UpdatePlayersName();
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
