﻿using BlackJack.Logic;
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

            for (int i = 0; i < gameForms.Length; i++)
            {
                gameForms[i] = new GameForm(game, UpdateUINames);
                gameForms[i].Show();
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

                ///EndGame();
                //game.SelectFirstPlayer();
                //UpdateCurrentForm();
                //currentForm.EndGame(currentForm.GetGameResult());
            }
            

        }

        private void EndGame()
        {
            for (int i = 0; i < gameForms.Length; i++)
            {
                if (gameForms[i] != null)
                {
                    gameForms[i].EndGame(gameForms[i].GetGameResult());
                    System.Threading.Thread.Sleep(1000);
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
