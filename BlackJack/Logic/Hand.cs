using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Logic
{
    public class Hand
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();
        public int NumCards { get { return cards.Count; } }
        public List<Card> Cards { get { return cards; } }

        /// <summary>
        /// Checks to see if the hand contains a card of a certain face value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsCard(CardValue item)
        {
            foreach (Card c in cards)
            {
                if (c.CardValue == item)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// This class is game-specific.  Creates a BlackJack hand that inherits from class hand
    /// </summary>
    public class BlackJackHand : Hand
    {
        /// <summary>
        /// This method compares two BlackJack hands
        /// </summary>
        /// <param name="otherHand"></param>
        /// <returns></returns>
        public int CompareFaceValue(object otherHand)
        {
            BlackJackHand aHand = otherHand as BlackJackHand;
            if (aHand != null)
            {
                return this.GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            else
            {
                throw new ArgumentException("Argument is not a Hand");
            }
        }

        /// <summary>
        /// Gets the total value of a hand from BlackJack values
        /// </summary>
        /// <returns>int</returns>
        public int GetSumOfHand()
        {
            int val = 0;
            int numAces = 0;

            foreach (Card c in cards)
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

            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }

            return val;
        }
    }
}
