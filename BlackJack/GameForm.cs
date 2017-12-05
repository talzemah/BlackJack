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
        private NewPlayerForm newPlayerForm;
        public NewPlayerForm NewPlayerForm { get { return newPlayerForm; } }

        //Creates a new blackjack game with one player and an inital balance set through the settings designer
        ////private BlackJackGame game = new BlackJackGame(Properties.Settings.Default.InitBalance);
        private BlackJackGame game;
        private PictureBox[] dealerCards;

        private PictureBox[] player_1Cards;
        private PictureBox[] player_2Cards;
        private PictureBox[] player_3Cards;

        private Player refPlayer;

        // Constractor
        public GameForm(BlackJackGame theGame, AddNameEventHandler addNameEventHandler)
        {
            game = theGame;
            InitializeComponent();

            LoadPictureBox();
            InitializeUI();
            SetUpNewGame();

            // Get player name.
            newPlayerForm = new NewPlayerForm();
            newPlayerForm.AddNameEvent += EnterPlayerName;
            newPlayerForm.AddNameEvent += addNameEventHandler;
            Hide();
            newPlayerForm.ShowDialog();
            Show();
        }

        private void EnterPlayerName(object sender, NameEventArgs e)
        {
            game.CurrentPlayer.Name = e.PlayerName;
            UpdatePlayersName();
            refPlayer = game.CurrentPlayer;
            FixLocation();

            game.NextPlayer();

            game.ActivePlayer++;
        }

        private void FixLocation()
        {
            Point p = new Point();
            if (refPlayer == game.Player_1)
            {
                p.X = 360;
                p.Y = 0;
            }
            else if (refPlayer == game.Player_2)
            {
                p.X = -95;
                p.Y = 250;
            }
            else
            {
                p.X = 710;
                p.Y = 250;
            }

            this.Location = p;
        }

        public void UpdatePlayersName()
        {
            Lbl_p1_name.Text = game.Player_1.Name;
            Lbl_p2_name.Text = game.Player_2.Name;
            Lbl_p3_name.Text = game.Player_3.Name;
        }

        // Load the picture box for each hand
        private void LoadPictureBox()
        {
            dealerCards = new PictureBox[] { Pb_dealer_1, Pb_dealer_2, Pb_dealer_3, Pb_dealer_4, Pb_dealer_5, Pb_dealer_6, Pb_dealer_7, Pb_dealer_8 };
            player_1Cards = new PictureBox[] { Pb_P1_1, Pb_P1_2, Pb_P1_3, Pb_P1_4, Pb_P1_5, Pb_P1_6, Pb_P1_7, Pb_P1_8 };
            player_2Cards = new PictureBox[] { Pb_P2_1, Pb_P2_2, Pb_P2_3, Pb_P2_4, Pb_P2_5, Pb_P2_6, Pb_P2_7, Pb_P2_8 };
            player_3Cards = new PictureBox[] { Pb_P3_1, Pb_P3_2, Pb_P3_3, Pb_P3_4, Pb_P3_5, Pb_P3_6, Pb_P3_7, Pb_P3_8 };
        }

        // Set up the UI for a new game
        private void SetUpNewGame()
        {
            Lbl_p1_name.Show();
            Lbl_p1_totalSum.Show();

            Lbl_p2_name.Show();
            Lbl_p2_totalSum.Show();

            Lbl_p3_name.Show();
            Lbl_p3_totalSum.Show();

            Btn_stand.Enabled = false;
            Btn_hit.Enabled = false;
            Btn_double.Enabled = false;

            Btn_deal.Enabled = true;
            Btn_clear.Enabled = true;

            Btn_10.Enabled = true;
            btn_25.Enabled = true;
            Btn_50.Enabled = true;
            Btn_100.Enabled = true;

            game.DealNewGame();
            ///isFirstTurn = true;
            ShowBalanceAndBetValue();
        }

        public void InitializeUI()
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
            Lbl_p1_totalSum.Text = "$" + game.Player_1.Balance.ToString();
            Tb_myBet.Text = "$" + (refPlayer != null ? refPlayer.Bet.ToString() : "0");

            Lbl_p2_totalSum.Text = "$" + game.Player_2.Balance.ToString();
            Lbl_p3_totalSum.Text = "$" + game.Player_3.Balance.ToString();
        }

        // Invoked when the deal button is clicked
        private void Btn_deal_Click(object sender, EventArgs e)
        {
            try
            {
                // If the current bet is equal to 0, ask the player to place a bet
                if ((refPlayer.Bet == 0) && (refPlayer.Balance > 0))
                {
                    MessageBox.Show(refPlayer.Name + " must place a bet before the dealer deals.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    game.ReadyToDeal++;
                    Btn_deal.Enabled = false;
                    Btn_clear.Enabled = false;

                    if (game.ReadyToDeal < game.ActivePlayer)
                    {
                        return;
                    }

                    // Place the bet
                    game.PlaceBet();
                    //refPlayer.PlaceBet();

                    ShowBalanceAndBetValue();

                    // Clear the table, set up the UI for playing a game, and deal a new game
                    ClearCardsOnTable();
                    UpdateUIButtons();
                    game.DealNewGame();
                    UpdateUICards();

                    // Check see if the current player has blackjack
                    if (refPlayer.HasBlackJack())
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
            for (int i = 0; i < dealerCards.Length; i++)
            {
                dealerCards[i].Image = null;
                dealerCards[i].Visible = false;

                player_1Cards[i].Image = null;
                player_1Cards[i].Visible = false;

                player_2Cards[i].Image = null;
                player_2Cards[i].Visible = false;

                player_3Cards[i].Image = null;
                player_3Cards[i].Visible = false;
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
            if (refPlayer.IsFirstTurn)
                Btn_double.Enabled = true;

            Tb_status.Hide();
            Lbl_dealer_cardsSum.Show();

            Lbl_p1_cardsSum.Show();
            Lbl_p2_cardsSum.Show();
            Lbl_p3_cardsSum.Show();
        }

        // Refresh the UI to show appropriate cards
        private void UpdateUICards()
        {
            // Update the value of the dealer hand
            Lbl_dealer_cardsSum.Text = game.Dealer.Hand.GetSumOfHand().ToString();

            List<Card> dcards = game.Dealer.Hand.Cards;
            for (int i = 0; i < dcards.Count; i++)
            {
                LoadCard(dealerCards[i], dcards[i]);
                dealerCards[i].Visible = true;
                dealerCards[i].BringToFront();
            }

            // Update the value of the player_1 hand
            Lbl_p1_cardsSum.Text = game.Player_1.Hand.GetSumOfHand().ToString();

            List<Card> p_1Cards = game.Player_1.Hand.Cards;
            for (int i = 0; i < p_1Cards.Count; i++)
            {
                // Load each card from file
                LoadCard(player_1Cards[i], p_1Cards[i]);
                player_1Cards[i].Visible = true;
                player_1Cards[i].BringToFront();
            }

            // Update the value of the player_2 hand
            Lbl_p2_cardsSum.Text = game.Player_2.Hand.GetSumOfHand().ToString();

            List<Card> p_2Cards = game.Player_2.Hand.Cards;
            for (int i = 0; i < p_2Cards.Count; i++)
            {
                // Load each card from file
                LoadCard(player_2Cards[i], p_2Cards[i]);
                player_2Cards[i].Visible = true;
                player_2Cards[i].BringToFront();
            }

            // Update the value of the player_3 hand
            Lbl_p3_cardsSum.Text = game.Player_3.Hand.GetSumOfHand().ToString();

            List<Card> p_3Cards = game.Player_3.Hand.Cards;
            for (int i = 0; i < p_3Cards.Count; i++)
            {
                // Load each card from file
                LoadCard(player_3Cards[i], p_3Cards[i]);
                player_3Cards[i].Visible = true;
                player_3Cards[i].BringToFront();
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
                    game.PlayerWin(refPlayer);
                    break;

                case EndResult.DealerBlackJack:
                    Tb_status.Text = "Dealer BlackJack!";
                    break;

                case EndResult.DealerWin:
                    Tb_status.Text = "Dealer Won!";
                    break;

                case EndResult.PlayerBlackJack:
                    Tb_status.Text = refPlayer.Name + " BlackJack!";
                    refPlayer.Balance += (refPlayer.Bet * (decimal)2.5);
                    break;

                case EndResult.PlayerBust:
                    Tb_status.Text = refPlayer.Name + " Bust!";
                    break;

                case EndResult.PlayerWin:
                    Tb_status.Text = refPlayer.Name + " Won!";
                    game.PlayerWin(refPlayer);
                    break;

                case EndResult.Push:
                    Tb_status.Text = refPlayer.Name + " Push";
                    refPlayer.Balance += refPlayer.Bet;
                    break;
            }

            SetUpNewGame();
            ShowBalanceAndBetValue();
            Tb_status.Show();

            // Check if the current player is out of money
            if (refPlayer.Balance == 0)
            {
                MessageBox.Show("Out of Money. Please create a new game to play again.");
                this.Close();
            }
        }

        // This method updates the current bet by a specified bet amount
        private void Bet(decimal betValue)
        {
            try
            {
                // Update the bet amount
                refPlayer.IncreaseBet(betValue);

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
            refPlayer.ClearBet();
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
            else if (game.Dealer.Hand.CompareHandsValue(refPlayer.Hand) > 0)
            {
                endState = EndResult.DealerWin;
            }

            // Check if the result is equals (push)
            else if (game.Dealer.Hand.CompareHandsValue(refPlayer.Hand) == 0)
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
            refPlayer.IsFirstTurn = false;

            Btn_double.Enabled = false;

            // Hit once and update UI cards
            refPlayer.Hit();
            UpdateUICards();

            // Check to see if player has bust
            if (refPlayer.HasBust())
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
                refPlayer.DoubleBet();
                UpdateUICards();
                ShowBalanceAndBetValue();

                //Make sure that the player didn't bust
                if (refPlayer.HasBust())
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
