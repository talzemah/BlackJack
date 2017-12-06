using BlackJack.Logic;
using System;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class MainMenuForm : Form
    {
        private BlackJackGame game;
        private GameForm[] gameForms = new GameForm[3];
        private GameForm currentForm;

        // Constractor.
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void Btn_newGame_Click(object sender, EventArgs e)
        {
            game = new BlackJackGame(Properties.Settings.Default.InitBalance);
            game.DealEvent += UpdateUIAfterDeal;
            game.PlayerFinishEvent += CurrentPlayerFinishToPlay;
            game.PlayerEndEvent += CurrentPlayerEndGame;
            game.HitEvent += UpdateUIAfterHit;
            game.GameCloseEvent += AnyGameClosed;

            for (int i = 0; i < gameForms.Length; i++)
            {
                gameForms[i] = new GameForm(game, UpdateUINames);
                gameForms[i].Show();
            }
        }

        private void AnyGameClosed(object sender, EventArgs e)
        {
            int closedForm = -1;
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] == sender)
                {
                    gameForms[i] = null;
                    closedForm = i;
                    break;
                }
            }

            game.ActivePlayers--;
            if (closedForm != -1)
                game.Players[closedForm] = null;

            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].UpdatePlayersName();
                    gameForms[i].UpdateBalanceAndBetValue();
                    gameForms[i].UpdateUICards();
                }
            }
        }

        private void UpdateUIAfterHit(object sender, EventArgs e)
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].UpdateUICards();
                }
            }
        }

        private void CurrentPlayerEndGame(object sender, EndEventArgs e)
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].UpdateEndRes(e.Res);
                    gameForms[i].UpdateBalanceAndBetValue();
                }
            }
        }

        private void CurrentPlayerFinishToPlay(object sender, EventArgs e)
        {
            currentForm.UpdateButtonsFinishPlay();

            game.NextPlayer();
            if (UpdateCurrentForm())
            {
                currentForm.UpdateButtonsToPlay();
            }
            else
            {
                // All players are finish to play.
                game.DealerPlay();
                UpdateUIAfterDeal(this, new EventArgs());
                EndGame();
            }
        }

        private void EndGame()
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].EndGame(gameForms[i].GetGameResult());

                    // In case this object = null from previous row.
                    if (gameForms[i] != null)
                        gameForms[i].UpdateBalanceAndBetValue();
                }
            }
        }

        private void UpdateUIAfterDeal(object sender, EventArgs e)
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].UpdateUICards();
                    gameForms[i].UpdateBalanceAndBetValue();
                }
            }
            UpdateCurrentForm();
            currentForm.UpdateButtonsToPlay();
        }

        private bool UpdateCurrentForm()
        {
            GameForm temp = currentForm;
            for (int i = 0; i < game.Players.Length; i++)
            {
                if (game.Players[i] == game.CurrentPlayer)
                {
                    currentForm = gameForms[i];
                }
            }
            return temp != currentForm;
        }

        private void UpdateUINames(object sender, NameEventArgs e)
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].UpdatePlayersName();
                }
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
