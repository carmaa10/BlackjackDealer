using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDealer
{
    class Dealer
    {
        #region ENUMS

        public enum BettingStyle
        {
            everyManForHimself,
            threeMusketeers
        }

        #endregion

        #region FIELDS

        private List<PlayingCard> _cards;
        private int _roundtotal;

        #endregion

        #region PROPERTIES

        public List<PlayingCard> Cards
        {
            get { return _cards; }
            set { _cards = Cards; }
        }

        public int RoundTotal
        {
            get { return _roundtotal; }
            set { _roundtotal = RoundTotal; }
        }

        #endregion

        #region CONSTRUCTORS

        public Dealer()
        {
            _cards = new List<PlayingCard>();
            _roundtotal = 0;
        }

        #endregion
    }
}
