using System;
using System.Collections.Generic;

namespace BlackJack.Logic
{
    public class Deck
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();

        // Returns the card at the given position
        public Card this[int position] { get { return (Card)cards[position]; } }
        

        // One complete deck (52 cards)
        public Deck()
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards.Add(new Card(type, value, true));
                }
            }
        }

        // Draws one card and removes it from the deck
        public Card Draw()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        // Shuffles the cards in the deck
        public void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int index1 = i;
                int index2 = random.Next(cards.Count);
                SwapCard(index1, index2);
            }
        }

        // Swaps the placement of two cards
        private void SwapCard(int index1, int index2)
        {
            Card card = cards[index1];
            cards[index1] = cards[index2];
            cards[index2] = card;
        }

    }
}
