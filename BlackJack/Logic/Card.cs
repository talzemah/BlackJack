
namespace BlackJack.Logic
{
    public enum CardType
    {
        Diamonds, Spades, Clubs, Hearts
    }

    public enum CardValue
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8,
        Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14
    }

    public class Card
    {
        private readonly CardType cardType;
        private readonly CardValue cardValue;
        private bool isCardUp;

        public CardType CardType { get { return cardType; } }
        public CardValue CardValue { get { return cardValue; } }
        public bool IsCardUp { get { return isCardUp; } set { isCardUp = value; } }

        // Constructor
        public Card(CardType cardType, CardValue cardValue, bool isCardUp)
        {
            this.cardType = cardType;
            this.cardValue = cardValue;
            this.isCardUp = isCardUp;
        }

    }
}
