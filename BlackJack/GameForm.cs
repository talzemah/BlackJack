using BlackJack.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class GameForm : Form
    {
        NewPlayerForm newPlayerForm;

        //Creates a new blackjack game with one player and an inital balance set through the settings designer
        private BlackJackGame game = new BlackJackGame(Properties.Settings.Default.InitBalance);
        private PictureBox[] player_1Cards;
        private PictureBox[] dealerCards;
        private bool isFirstTurn;

        // Constractor
        public GameForm()
        {
            InitializeComponent();
            LoadPictureBox();
            Foo();
            SetUpNewGame();

            newPlayerForm = new NewPlayerForm();
            newPlayerForm.AddNameEvent += UpdatePlayerName;
            Hide();
            newPlayerForm.ShowDialog();
            Show();
        }

        private void UpdatePlayerName(object sender, NameEventArgs e)
        {
            Lbl_p1_name.Text = e.PlayerName;
        }


        // Load the picture box for each hand
        private void LoadPictureBox()
        {
            dealerCards = new PictureBox[] { Pb_dealer_1, Pb_dealer_2, Pb_dealer_3, Pb_dealer_4, Pb_dealer_5, Pb_dealer_6 };
            player_1Cards = new PictureBox[] { Pb_P1_1, Pb_P1_2, Pb_P1_3, Pb_P1_4, Pb_P1_5, Pb_P1_6 };
        }

        // Set up the UI for a new game
        private void SetUpNewGame()
        {
            Lbl_p1_name.Show();
            Lbl_p1_totalSum.Show();
            Lbl_p1_name.Text = Properties.Settings.Default.PlayerName;

            Btn_stand.Enabled = false;
            Btn_hit.Enabled = false;
            Btn_double.Enabled = false;

            Btn_deal.Enabled = true;
            Btn_clear.Enabled = true;

            Btn_10.Enabled = true;
            btn_25.Enabled = true;
            Btn_50.Enabled = true;
            Btn_100.Enabled = true;

            isFirstTurn = true;
            ShowBalanceAndBetValue();
        }

        public void Foo()
        {
            Tb_status.Hide();

            Lbl_dealer_status.Hide();
            Lbl_dealer_cardsSum.Hide();

            Lbl_p1_name.Hide();
            Lbl_p1_totalSum.Hide();
            Lbl_p1_status.Hide();
            Lbl_p1_cardsSum.Hide();

            Lbl_p2_name.Hide();
            Lbl_p2_totalSum.Hide();
            Lbl_p2_status.Hide();
            Lbl_p2_cardsSum.Hide();

            Lbl_p3_name.Hide();
            Lbl_p3_totalSum.Hide();
            Lbl_p3_status.Hide();
            Lbl_p3_cardsSum.Hide();
        }

        // Set the "My Account" value in the UI
        private void ShowBalanceAndBetValue()
        {
            // Update the "My Account" value
            Lbl_p1_totalSum.Text = "$" + game.CurrentPlayer.Balance.ToString();
            Tb_myBet.Text = "$" + game.CurrentPlayer.Bet.ToString();
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
                    ShowBalanceAndBetValue();

                    // Clear the table, set up the UI for playing a game, and deal a new game
                    ClearCardsOnTable();
                    UpdateUIButtons();
                    game.DealNewGame();
                    UpdateUICards();

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

        // Clear all cards on table
        private void ClearCardsOnTable()
        {
            for (int i = 0; i < 6; i++)
            {
                dealerCards[i].Image = null;
                dealerCards[i].Visible = false;

                player_1Cards[i].Image = null;
                player_1Cards[i].Visible = false;
            }
        }

        // Set up the UI for when the game is in play after the player has hit deal game
        private void UpdateUIButtons()
        {
            Btn_deal.Enabled = false;
            Btn_clear.Enabled = false;

            Btn_10.Enabled = false;
            btn_25.Enabled = false;
            Btn_50.Enabled = false;
            Btn_100.Enabled = false;

            Btn_stand.Enabled = true;
            Btn_hit.Enabled = true;
            if (isFirstTurn)
                Btn_double.Enabled = true;

            Tb_status.Hide();
            Lbl_p1_cardsSum.Show();
            Lbl_dealer_cardsSum.Show();
        }

        // Refresh the UI to show appropriate cards
        private void UpdateUICards()
        {
            // Update the value of the player_1 hand
            Lbl_p1_cardsSum.Text = game.CurrentPlayer.Hand.GetSumOfHand().ToString();

            List<Card> pcards = game.CurrentPlayer.Hand.Cards;
            for (int i = 0; i < pcards.Count; i++)
            {
                // Load each card from file
                LoadCard(player_1Cards[i], pcards[i]);
                player_1Cards[i].Visible = true;
                player_1Cards[i].BringToFront();
            }

            // Update the value of the dealer hand
            Lbl_dealer_cardsSum.Text = game.Dealer.Hand.GetSumOfHand().ToString();

            List<Card> dcards = game.Dealer.Hand.Cards;
            for (int i = 0; i < dcards.Count; i++)
            {
                LoadCard(dealerCards[i], dcards[i]);
                dealerCards[i].Visible = true;
                dealerCards[i].BringToFront();
            }
        }

        // Takes the card type and value and loads the corresponding card image
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

                image.Append(Properties.Settings.Default.CardImageExtension);

                string cardImagePath = Properties.Settings.Default.CardImagePath;
                image.Insert(0, cardImagePath);

                //check to see if the card should be faced down or up;
                if (!c.IsCardUp)
                    image.Replace(image.ToString(), Properties.Settings.Default.CardImageSkinPath);

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
                    Tb_status.Text = "Dealer Bust!";
                    game.PlayerWin();
                    break;

                case EndResult.DealerBlackJack:
                    Tb_status.Text = "Dealer BlackJack!";
                    break;

                case EndResult.DealerWin:
                    Tb_status.Text = "Dealer Won!";
                    break;

                case EndResult.PlayerBlackJack:
                    Tb_status.Text = "PlayerBlackJack!";
                    game.CurrentPlayer.Balance += (game.CurrentPlayer.Bet * (decimal)2.5);
                    break;

                case EndResult.PlayerBust:
                    Tb_status.Text = "PlayerBust!";
                    break;

                case EndResult.PlayerWin:
                    Tb_status.Text = "Player Won!";
                    game.PlayerWin();
                    break;

                case EndResult.Push:
                    Tb_status.Text = "Push";
                    game.CurrentPlayer.Balance += game.CurrentPlayer.Bet;
                    break;
            }

            SetUpNewGame();
            ShowBalanceAndBetValue();
            Tb_status.Show();

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
                ShowBalanceAndBetValue();
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
            ShowBalanceAndBetValue();
        }

        // Invoked when the player has finished their turn and clicked the stand button
        private void Btn_stand_Click(object sender, EventArgs e)
        {
            // Dealer should finish playing and the UI should be updated
            game.DealerPlay();
            UpdateUICards();

            // Check who won the game
            EndGame(GetGameResult());
        }

        // Get the game result, This returns an EndResult value
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

            // Check if the ealer wins
            else if (game.Dealer.Hand.CompareHandsValue(game.CurrentPlayer.Hand) > 0)
            {
                endState = EndResult.DealerWin;
            }

            // Check if the result is equals (push)
            else if (game.Dealer.Hand.CompareHandsValue(game.CurrentPlayer.Hand) == 0)
            {
                endState = EndResult.Push;
            }

            // player wins
            else
            {
                endState = EndResult.PlayerWin;
            }

            return endState;
        }

        // Invoked when the hit button is clicked
        private void Btn_hit_Click(object sender, EventArgs e)
        {
            // It is no longer the first turn, set this to false so that the cards will all be facing upwards
            isFirstTurn = false;

            Btn_double.Enabled = false;

            // Hit once and update UI cards
            game.CurrentPlayer.Hit();
            UpdateUICards();

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
                game.CurrentPlayer.DoubleBet();
                UpdateUICards();
                ShowBalanceAndBetValue();

                //Make sure that the player didn't bust
                if (game.CurrentPlayer.HasBust())
                {
                    EndGame(EndResult.PlayerBust);
                }

                // Otherwise, let the dealer finish playing
                else
                {
                    game.DealerPlay();
                    UpdateUICards();
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
