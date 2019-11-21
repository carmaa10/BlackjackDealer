using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDealer
{
    class PlayingCard
    {
        #region ENUMS

        public enum Rank
        {
            Ace,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            King,
            Queen
        }

        public enum Suit
        {
            Spades,
            Diamonds,
            Clubs,
            Hearts
        }

        #endregion

        #region FIELDS

        private Suit _suit;
        private Rank _rank;
        private int _value;

        #endregion

        #region PROPERTIES

        public Suit CardSuit
        {
            get { return _suit; }
            set { _suit = value; }
        }

        public Rank CardRank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int CardValue
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public PlayingCard()
        {

        }

        public PlayingCard(Suit suit, Rank rank, int value)
        {
            _suit = suit;
            _rank = rank;
            _value = value;
        }

        #endregion

        #region METHODS

        public string CardInfo()
        {
            string cardInfo;

            cardInfo = $"{_rank} of {_suit}";

            return cardInfo;
        }

        #endregion
    }
}
