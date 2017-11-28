using System;
using System.Collections.Generic;

namespace BlackJack.Logic
{
    public class Hand
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();

        public int NumCards { get { return cards.Count; } }
        public List<Card> Cards { get { return cards; } }
    }


    // This class is game-specific.  Creates a BlackJack hand that inherits from class hand
    public class BlackJackHand : Hand
    {
        // This method compares two BlackJack hands
        public int CompareHandsValue(BlackJackHand otherHand)
        {
            if (otherHand != null)
            {
                return this.GetSumOfHand().CompareTo(otherHand.GetSumOfHand());
            }
            else
            {
                throw new ArgumentException("Argument is not a Hand");
            }
        }

        // Gets the total value of a hand from BlackJack values
        public int GetSumOfHand()
        {
            int val = 0;
            int numAces = 0;

            foreach (Card c in cards)
            {
                if (c.IsCardUp)
                {
                    if (c.CardValue == CardValue.Ace)
                    {
                        numAces++;
                        val += 11;
                    }
                    else if (c.CardValue == CardValue.Jack || c.CardValue == CardValue.Queen || c.CardValue == CardValue.King)
                    {
                        val += 10;
                    }
                    else
                    {
                        val += (int)c.CardValue;
                    }
                }
            }

            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }

            return val;
        }
    }
}
