using System;

namespace BlackJack.Logic
{
    public class BlackJackGame
    {   
        // Private Deck and Player objects for the current deck, dealer, and player
        private Deck deck;
        private Player dealer;
        private Player player_1;
        private Player player_2;
        private Player player_3;
        private Player[] players = new Player[3];

        private Player currentPlayer;
        private int activePlayer;
        private int readyToDeal;

        // Public properties to return the current player, dealer, and current deck
        public Player Dealer { get { return dealer; } }
        public Deck CurrentDeck { get { return deck; } }
        public Player Player_1 { get { return player_1; } }
        public Player Player_2 { get { return player_2; } }
        public Player Player_3 { get { return player_3; } }
        public Player[] Players { get { return players; } }

        public Player CurrentPlayer { get { return currentPlayer; } }
        public int ActivePlayer { get { return activePlayer; } set { activePlayer = value; } }
        public int ReadyToDeal { get { return readyToDeal; } set { readyToDeal = value; } }

        // Constructor for BlackJack Game
        public BlackJackGame(int initBalance)
        {
            // Create a dealer and one player with the initial balance.
            dealer = new Player();
            player_1 = new Player(initBalance);
            player_2 = new Player(initBalance);
            player_3 = new Player(initBalance);

            currentPlayer = Player_1;
            activePlayer = 0;
            readyToDeal = 0;
        }

        // Deals a new game. This is invoked through the Deal button in GameForm.cs
        public void DealNewGame()
        {
            // Reset readyToDeal var
            readyToDeal = 0;

            // Create a new deck and then shuffle the deck
            deck = new Deck();
            deck.Shuffle();

            // Reset the player and the dealer's hands in case this is not the first game
            dealer.NewHand();

            player_1.NewHand();
            player_2.NewHand();
            player_3.NewHand();
            

            // Deal two cards to each person's hand
            for (int i = 0; i < 2; i++)
            {
                Card tempCard = deck.DrawCard();
                player_1.Hand.Cards.Add(tempCard);

                tempCard = deck.DrawCard();
                player_2.Hand.Cards.Add(tempCard);

                tempCard = deck.DrawCard();
                player_3.Hand.Cards.Add(tempCard);

                tempCard = deck.DrawCard();
                // Set the dealer's second card to be facing down
                if (i == 1)
                    tempCard.IsCardUp = false;

                dealer.Hand.Cards.Add(tempCard);
            }

            // Give the player and the dealer a handle to the current deck
            player_1.CurrentDeck = deck;
            player_2.CurrentDeck = deck;
            player_3.CurrentDeck = deck;
            dealer.CurrentDeck = deck;
        }

        public void NextPlayer()
        {
            if (currentPlayer == Player_1)
            {
                currentPlayer = Player_2;
            }
            else if (currentPlayer == Player_2)
            {
                currentPlayer = Player_3;
            }
            else
            {
                currentPlayer = Player_1;
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

        public void PlaceBet()
        {
            Player_1.PlaceBet();
            Player_2.PlaceBet();
            Player_3.PlaceBet();
        }
    }
}
