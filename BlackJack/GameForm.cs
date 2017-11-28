using BlackJack.Logic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public partial class GameForm : Form
    {

        #region Fields
        //Creates a new blackjack game with one player and an inital balance set through the settings designer
        private BlackJackGame game = new BlackJackGame(Properties.Settings.Default.InitBalance);
        private PictureBox[] playerCards;
        private PictureBox[] dealerCards;
        private bool firstTurn;
        #endregion

        public GameForm()
        {
            InitializeComponent();
            LoadCardSkinImages();
            SetUpNewGame();
        }

        // Load the Deck Card Images
        private void LoadCardSkinImages()
        {
            dealerCards = new PictureBox[] { Pb_dealer_1, Pb_dealer_2, Pb_dealer_3, Pb_dealer_4, Pb_dealer_5, Pb_dealer_6 };
            playerCards = new PictureBox[] { Pb_P1_1, Pb_P1_2, Pb_P1_3, Pb_P1_4, Pb_P1_5, Pb_P1_6 };
        }

        // Set up the UI for a new game
        private void SetUpNewGame()
        {
            ///photoPictureBox.ImageLocation = Properties.Settings.Default.PlayerImage;
            ///photoPictureBox.Visible = true;
            gameOverTextBox.Hide();

            Lbl_p1_name.Text = "Tal Zemah"; /// Properties.Settings.Default.PlayerName;

            Btn_deal.Enabled = true;
            Btn_stand.Enabled = false;
            Btn_hit.Enabled = false;
            Btn_double.Enabled = false;
            Btn_clear.Enabled = true;

            Btn_10.Enabled = true;
            btn_25.Enabled = true;
            Btn_50.Enabled = true;
            Btn_100.Enabled = true;

            if (Lbl_p1_totalCards.Text.Equals("00"))
            {
                Lbl_p1_totalCards.Hide();
            }
            
            Lbl_p1_status.Hide();
            firstTurn = true;
            ShowBankValue();
        }

        // Set the "My Account" value in the UI
        private void ShowBankValue()
        {
            // Update the "My Account" value
            Lbl_p1_totalSum.Text = "$" + game.CurrentPlayer.Balance.ToString();
            ///Lbl_p1_totalSum.Text = "$" + 3000;
            Tb_myBet.Text = "$" + game.CurrentPlayer.Bet.ToString();
            ///Tb_myBet.Text = "$" + 0;
        }


        // Invoked when the deal button is clicked
        private void Btn_deal_Click(object sender, EventArgs e)
        {
            try
            {
                // If the current bet is equal to 0, ask the player to place a bet
                if ((game.CurrentPlayer.Bet == 0) && (game.CurrentPlayer.Balance > 0))
                {
                    MessageBox.Show("You must place a bet before the dealer deals.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Place the bet
                    game.CurrentPlayer.PlaceBet();
                    ShowBankValue();

                    // Clear the table, set up the UI for playing a game, and deal a new game
                    ClearTable();
                    SetUpGameInPlay();
                    game.DealNewGame();
                    UpdateUIPlayerCards();

                    // Check see if the current player has blackjack
                    if (game.CurrentPlayer.HasBlackJack())
                    {
                        EndGame(EndResult.PlayerBlackJack);
                    }
                }
            }
            catch (Exception NotEnoughMoneyException)
            {
                MessageBox.Show(NotEnoughMoneyException.Message);
            }
        }

        // Clear the dealer and player cards
        private void ClearTable()
        {
            for (int i = 0; i < 6; i++)
            {
                dealerCards[i].Image = null;
                dealerCards[i].Visible = false;

                playerCards[i].Image = null;
                playerCards[i].Visible = false;
            }
        }

        // Set up the UI for when the game is in play after the player has hit deal game
        private void SetUpGameInPlay()
        {
            Btn_10.Enabled = false;
            btn_25.Enabled = false;
            Btn_50.Enabled = false;
            Btn_100.Enabled = false;

            Btn_clear.Enabled = false;
            Btn_stand.Enabled = true;
            Btn_hit.Enabled = true;

            gameOverTextBox.Hide();

            Lbl_p1_totalCards.Show();
            Btn_deal.Enabled = false;

            if (firstTurn)
                Btn_double.Enabled = true;
        }

        // Refresh the UI to update the player cards
        private void UpdateUIPlayerCards()
        {
            // Update the value of the hand
            Lbl_p1_totalCards.Text = game.CurrentPlayer.Hand.GetSumOfHand().ToString();

            List<Card> pcards = game.CurrentPlayer.Hand.Cards;
            for (int i = 0; i < pcards.Count; i++)
            {
                // Load each card from file
                LoadCard(playerCards[i], pcards[i]);
                playerCards[i].Visible = true;
                playerCards[i].BringToFront();
            }

            List<Card> dcards = game.Dealer.Hand.Cards;
            for (int i = 0; i < dcards.Count; i++)
            {
                LoadCard(dealerCards[i], dcards[i]);
                dealerCards[i].Visible = true;
                dealerCards[i].BringToFront();
            }
        }

        // Takes the card value and loads the corresponding card image from file
        private void LoadCard(PictureBox pb, Card c)
        {
            try
            {
                StringBuilder image = new StringBuilder();

                switch (c.CardType)
                {
                    case CardType.Diamonds:
                        image.Append("_of_diamonds");
                        break;
                    case CardType.Hearts:
                        image.Append("_of_hearts");
                        break;
                    case CardType.Spades:
                        image.Append("_of_spades");
                        break;
                    case CardType.Clubs:
                        image.Append("_of_clubs");
                        break;
                }

                switch (c.CardValue)
                {
                    case CardValue.Ace:
                        image.Insert(0, "ace");
                        break;
                    case CardValue.King:
                        image.Insert(0, "king");
                        break;
                    case CardValue.Queen:
                        image.Insert(0, "queen");
                        break;
                    case CardValue.Jack:
                        image.Insert(0, "jack");
                        break;
                    case CardValue.Ten:
                        image.Insert(0, "10");
                        break;
                    case CardValue.Nine:
                        image.Insert(0, "9");
                        break;
                    case CardValue.Eight:
                        image.Insert(0, "8");
                        break;
                    case CardValue.Seven:
                        image.Insert(0, "7");
                        break;
                    case CardValue.Six:
                        image.Insert(0, "6");
                        break;
                    case CardValue.Five:
                        image.Insert(0, "5");
                        break;
                    case CardValue.Four:
                        image.Insert(0, "4");
                        break;
                    case CardValue.Three:
                        image.Insert(0, "3");
                        break;
                    case CardValue.Two:
                        image.Insert(0, "2");
                        break;
                }

                ///image.Append(".png");
                image.Append(Properties.Settings.Default.CardGameImageExtension);
                string cardGameImagePath = Properties.Settings.Default.CardGameImagePath;
                string cardGameImageSkinPath = Properties.Settings.Default.CardGameImageSkinPath;
                image.Insert(0, cardGameImagePath);
                //check to see if the card should be faced down or up;
                if (!c.IsCardUp)
                    image.Replace(image.ToString(), cardGameImageSkinPath);

                ///pb.Image = new Bitmap(image.ToString());
                pb.BackgroundImage = new Bitmap(image.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Card images are not loading correctly.  Make sure all card images are in the right location.");
            }
        }


        // Takes an EndResult value and shows the resulting game ending in the UI
        private void EndGame(EndResult endState)
        {
            switch (endState)
            {
                case EndResult.DealerBust:
                    gameOverTextBox.Text = "Dealer Bust!";
                    game.PlayerWin();
                    break;
                case EndResult.DealerBlackJack:
                    gameOverTextBox.Text = "Dealer BlackJack!";
                    game.PlayerLose();
                    break;
                case EndResult.DealerWin:
                    gameOverTextBox.Text = "Dealer Won!";
                    game.PlayerLose();
                    break;
                case EndResult.PlayerBlackJack:
                    gameOverTextBox.Text = "BlackJack!";
                    game.CurrentPlayer.Balance += (game.CurrentPlayer.Bet * (decimal)2.5);
                    game.CurrentPlayer.Wins += 1;
                    break;
                case EndResult.PlayerBust:
                    gameOverTextBox.Text = "You Bust!";
                    game.PlayerLose();
                    break;
                case EndResult.PlayerWin:
                    gameOverTextBox.Text = "You Won!";
                    game.PlayerWin();
                    break;
                case EndResult.Push:
                    gameOverTextBox.Text = "Push";
                    game.CurrentPlayer.Push += 1;
                    game.CurrentPlayer.Balance += game.CurrentPlayer.Bet;
                    break;
            }
            // Update the "My Record" values
            ///winTextBox.Text = game.CurrentPlayer.Wins.ToString();
            ///lossTextBox.Text = game.CurrentPlayer.Losses.ToString();
            ///tiesTextBox.Text = game.CurrentPlayer.Push.ToString();

            SetUpNewGame();
            ShowBankValue();
            gameOverTextBox.Show();
            // Check if the current player is out of money
            if (game.CurrentPlayer.Balance == 0)
            {
                MessageBox.Show("Out of Money.  Please create a new game to play again.");
                this.Close();
            }
        }

        // This method updates the current bet by a specified bet amount
        private void Bet(decimal betValue)
        {
            try
            {
                // Update the bet amount
                game.CurrentPlayer.IncreaseBet(betValue);

                // Update the "My Bet" and "My Account" values
                ShowBankValue();
            }
            catch (Exception NotEnoughMoneyException)
            {
                MessageBox.Show(NotEnoughMoneyException.Message);
            }
        }

        // Place a bet for 10 dollars
        private void Btn_10_Click(object sender, EventArgs e)
        {
            Bet(10);
        }

        // Place a bet for 25 dollars
        private void btn_25_Click(object sender, EventArgs e)
        {
            Bet(25);
        }

        // Place a bet for 50 dollars
        private void Btn_50_Click(object sender, EventArgs e)
        {
            Bet(50);
        }

        // Place a bet for 100 dollars
        private void Btn_100_Click(object sender, EventArgs e)
        {
            Bet(100);
        }

        // Clear the bet
        private void Btn_clear_Click(object sender, EventArgs e)
        {
            game.CurrentPlayer.ClearBet();
            ShowBankValue();
        }

        // Invoked when the player has finished their turn and clicked the stand button
        private void Btn_stand_Click(object sender, EventArgs e)
        {
            // Dealer should finish playing and the UI should be updated
            game.DealerPlay();
            UpdateUIPlayerCards();

            // Check who won the game
            EndGame(GetGameResult());
        }

        // Get the game result.  This returns an EndResult value
        private EndResult GetGameResult()
        {
            EndResult endState;
            // Check for blackjack
            if (game.Dealer.Hand.NumCards == 2 && game.Dealer.HasBlackJack())
            {
                endState = EndResult.DealerBlackJack;
            }
            // Check if the dealer has bust
            else if (game.Dealer.HasBust())
            {
                endState = EndResult.DealerBust;
            }
            else if (game.Dealer.Hand.CompareFaceValue(game.CurrentPlayer.Hand) > 0)
            {
                //dealer wins
                endState = EndResult.DealerWin;
            }
            else if (game.Dealer.Hand.CompareFaceValue(game.CurrentPlayer.Hand) == 0)
            {
                // push
                endState = EndResult.Push;
            }
            else
            {
                // player wins
                endState = EndResult.PlayerWin;
            }
            return endState;
        }

        // Invoked when the hit button is clicked
        private void Btn_hit_Click(object sender, EventArgs e)
        {
            // It is no longer the first turn, set this to false so that the cards will all be facing upwards
            firstTurn = false;
            // Hit once and update UI cards
            game.CurrentPlayer.Hit();
            UpdateUIPlayerCards();

            // Check to see if player has bust
            if (game.CurrentPlayer.HasBust())
            {
                EndGame(EndResult.PlayerBust);
            }
        }

        // Invoked when the double down button is clicked
        private void Btn_double_Click(object sender, EventArgs e)
        {
            try
            {
                // Double the player's bet amount
                game.CurrentPlayer.DoubleDown();
                UpdateUIPlayerCards();
                ShowBankValue();

                //Make sure that the player didn't bust
                if (game.CurrentPlayer.HasBust())
                {
                    EndGame(EndResult.PlayerBust);
                }
                else
                {
                    // Otherwise, let the dealer finish playing
                    game.DealerPlay();
                    UpdateUIPlayerCards();
                    EndGame(GetGameResult());
                }
            }
            catch (Exception NotEnoughMoneyException)
            {
                MessageBox.Show(NotEnoughMoneyException.Message);
            }
        }

        

    }
}
