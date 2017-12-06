using System;
using System.Collections.Generic;

namespace BlackJack.Logic
{
    public class Player
    {
        // Objects to store player information
        private decimal balance;
        private BlackJackHand hand;
        private decimal bet;
        private string name;
        private Deck currentDeck;
        private PlayerStatus playerStatus;
        private bool isFirstTurn;

        // Creates a list of cards
        private List<Card> cards = new List<Card>();

        public Deck CurrentDeck { get { return currentDeck; } set { currentDeck = value; } }
        public BlackJackHand Hand { get { return hand; } }
        public string Name { get { return name; } set { name = value; } }
        public decimal Bet { get { return bet; } set { bet = value; } }
        public decimal Balance { get { return balance; } set { balance = value; } }
        public PlayerStatus PlayerStatus { get { return playerStatus; } set { playerStatus = value; } }
        public bool IsFirstTurn { get { return isFirstTurn; } set { isFirstTurn = value; } }

        // Creates a player with a default balance account (i.e. it doesn't matter what the dealer's balance is)
        public Player() : this(-1) { }

        //  Creates a player with a new hand and new balance
        public Player(int newBalance)
        {
            // Sets the player's name that is displayed in the UI.
            this.hand = new BlackJackHand();
            this.balance = newBalance;
            this.playerStatus = PlayerStatus.FirstTurn;
            this.isFirstTurn = true;
        }

        // Increases the bet amount each time a bet is added to the hand
        // Invoked through the betting coins in the BlackJackForm.cs UI
        public void IncreaseBet(decimal amount)
        {
            // Check to see if the user has enough money to make this bet
            if ((balance - (bet + amount)) >= 0)
            {
                // Add money to the bet
                bet += amount;
            }
            else
            {
                throw new Exception("You do not have enough money to make this bet.");
            }
        }

        // Places the bet and subtracts the amount from "My Account"
        public void PlaceBet()
        {
            // Check to see if the user has enough money to place this bet
            if ((balance - bet) >= 0)
            {
                balance = balance - bet;
            }
            else
            {
                throw new Exception("You do not have enough money to place this bet.");
            }
        }

        // Reset the hand
        public void NewHand()
        {
            this.isFirstTurn = true;
            this.hand = new BlackJackHand();
        }

        // Set the bet value back to 0
        public void ClearBet()
        {
            bet = 0;
        }

        // Check if the hand has BlackJack
        public bool HasBlackJack()
        {
            if (hand.GetSumOfHand() == 21 && hand.Cards.Count == 2)
                return true;
            else return false;
        }

        // Check if the hand has bust
        public bool HasBust()
        {
            if (hand.GetSumOfHand() > 21)
                return true;
            else return false;
        }

        // Draw a card from the deck and add it to the hand
        public void Hit()
        {
            Card tempCard = currentDeck.DrawCard();
            hand.Cards.Add(tempCard);
        }

        // Player has chosen to double down, double the player's bet and hit once
        public void DoubleBet()
        {
            IncreaseBet(Bet);

            // Only decrease the balance by half of the current bet
            balance = balance - (bet / 2);
            //Hit();
        }

    }
}
