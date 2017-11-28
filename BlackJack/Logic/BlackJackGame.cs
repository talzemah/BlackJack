﻿namespace BlackJack.Logic
{
    public class BlackJackGame
    {
        // private Deck and Player objects for the current deck, dealer, and player
        private Deck deck;
        private Player dealer;
        private Player player;

        // public properties to return the current player, dealer, and current deck
        public Player CurrentPlayer { get { return player; } }
        public Player Dealer { get { return dealer; } }
        public Deck CurrentDeck { get { return deck; } }

        // Constructor for BlackJack Game
        public BlackJackGame(int initBalance)
        {
            // Create a dealer and one player with the initial balance.
            dealer = new Player();
            player = new Player(initBalance);
        }

        // Deals a new game.  This is invoked through the Deal button in GameForm.cs
        public void DealNewGame()
        {
            // Create a new deck and then shuffle the deck
            deck = new Deck();
            deck.Shuffle();

            // Reset the player and the dealer's hands in case this is not the first game
            player.NewHand();
            dealer.NewHand();

            // Deal two cards to each person's hand
            for (int i = 0; i < 2; i++)
            {
                Card tempCard = deck.Draw();
                player.Hand.Cards.Add(tempCard);

                tempCard = deck.Draw();
                // Set the dealer's second card to be facing down
                if (i == 1)
                    tempCard.IsCardUp = false;

                dealer.Hand.Cards.Add(tempCard);
            }

            // Give the player and the dealer a handle to the current deck
            player.CurrentDeck = deck;
            dealer.CurrentDeck = deck;
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
        public void PlayerWin()
        {
            player.Balance += player.Bet * 2;
        }
    }
}
