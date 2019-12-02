using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDealer
{
    class Player
    {
        #region FIELDS

        private string _name;
        private int _money;
        private List<PlayingCard> _cards;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = Name; }
        }

        public int Money
        {
            get { return _money; }
            set { _money = Money; }
        }

        public List<PlayingCard> Cards
        {
            get { return _cards; }
            set { _cards = Cards; }
        }

        #endregion

        #region CONSTRUCTORS
        public Player()
        {

        }

        public Player(string playerName, int startMoney)
        {
            _name = playerName;
            _money = startMoney;
            _cards = new List<PlayingCard>();
        }

        #endregion

        #region METHODS



        #endregion
    }
}
