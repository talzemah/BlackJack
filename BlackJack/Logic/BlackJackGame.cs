using System;

namespace BlackJack.Logic
{
    public class BlackJackGame
    {
        // Private Deck and Player objects for the current deck, dealer, and player
        private Deck deck;
        private Player dealer;
        private Player[] players = new Player[3];
        private Player currentPlayer;
        private int activePlayers;
        private int readyToDeal;

        // Public properties to return the current player, dealer, and current deck
        public Player Dealer { get { return dealer; } }
        public Deck CurrentDeck { get { return deck; } }
        public Player[] Players { get { return players; } }
        public Player CurrentPlayer { get { return currentPlayer; } }
        public int ActivePlayers { get { return activePlayers; } set { activePlayers = value; } }
        public int ReadyToDeal { get { return readyToDeal; } set { readyToDeal = value; } }

        // Events.
        public event DealEventHandler DealEvent;
        public event PlayerFinishToPlayEventHandler PlayerFinishEvent;
        public event PlayerEndPlayEventHandler PlayerEndEvent;
        public event HitEventHandler HitEvent;
        public event GameCloseEventHandler GameCloseEvent;

        // Constructor for BlackJack Game
        public BlackJackGame(int initBalance)
        {
            // Create a dealer and 3 players with the initial balance.
            dealer = new Player();

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(initBalance);
            }

            currentPlayer = players[0];
            activePlayers = 0;
            readyToDeal = 0;
        }

        public void AnyPlayerClosed(Object sender)
        {
            GameCloseEvent(sender, new EventArgs());
        }

        public void SelectFirstPlayer()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    currentPlayer = players[i];
                    return;
                }
            }
        }

        // Deals a new game. This is invoked through the Deal button in GameForm.cs
        public void DealNewGame()
        {
            // Is first player Turn again.
            SelectFirstPlayer();

            // Reset readyToDeal var
            readyToDeal = 0;

            // Create a new deck and then shuffle the deck
            deck = new Deck();
            deck.Shuffle();

            // Reset the players and the dealer's hands in case this is not the first game
            dealer.NewHand();

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    players[i].NewHand();
                }
            }

            // Deal two cards to each person's hand
            for (int i = 0; i < 2; i++)
            {
                Card tempCard = deck.DrawCard();

                // Set the dealer's second card to be facing down
                if (i == 1)
                    tempCard.IsCardUp = false;
                dealer.Hand.Cards.Add(tempCard);


                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j] != null)
                    {
                        tempCard = deck.DrawCard();
                        players[j].Hand.Cards.Add(tempCard);
                    }
                }
            }

            // Give the player and the dealer a handle to the current deck
            dealer.CurrentDeck = deck;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    players[i].CurrentDeck = deck;
                }
            }

            DealEvent(this, new EventArgs());
        }

        public void NextPlayer()
        {
            if (currentPlayer == players[0])
            {
                currentPlayer = players[1] != null ? players[1] : players[2] != null ? players[2] : players[0];
            }
            else if (currentPlayer == players[1])
            {
                currentPlayer = players[2] != null ? players[2] : players[0] != null ? players[0] : players[1];
                ///currentPlayer = players[2];
            }
            else if (currentPlayer == players[2])
            {
                Player temp = players[0] != null ? players[0] : players[1] != null ? players[1] : players[2];

                if (temp.PlayerStatus != PlayerStatus.FinishPlay )
                {
                    currentPlayer = temp;
                }
            }
        }

        // This method finishes playing the dealer's hand
        public void DealerPlay()
        {
            // Dealer's card that was facing down is turned up when they start playing
            dealer.Hand.Cards[1].IsCardUp = true;

            // Check to see if dealer has a hand that is less than 17
            // If so, dealer should keep hitting until their hand is greater or equal to 17
            while (dealer.Hand.GetSumOfHand() < 17)
            {
                dealer.Hit();
            }

        }

        // Update player's win
        public void PlayerWin(Player p)
        {
            p.Balance += p.Bet * 2;
        }

        public void PlaceBets()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    players[i].PlaceBet();
                }
            }
        }

        public void CurrentPlayerFinishToPlay()
        {
            PlayerFinishEvent(this, new EventArgs());
        }

        public void PlayerEndGame(String res)
        {
            EndEventArgs args = new EndEventArgs(res);
            PlayerEndEvent(this, args);
        }

        public void UpdatePlayerHit()
        {
            HitEvent(this, new EventArgs());
        }
    }
}
