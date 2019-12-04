using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDealer
{
    class Player
    {

        #region ENUMS

        public enum PlayerOutcome
        {
            none,
            bust,
            blackjack,
            pass
        }

        #endregion

        #region FIELDS

        private string _name;
        private int _money;
        private List<PlayingCard> _cards;
        //private bool _isplaying;
        //private int _roundbet;
        //private int _roundtotal;
        //private PlayerOutcome _playerOutcome;

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

        //public bool IsPlaying
        //{
        //    get { return _isplaying; }
        //    set { _isplaying = IsPlaying; }
        //}

        //public int RoundBet
        //{
        //    get { return _roundbet; }
        //    set { _roundbet = RoundBet; }
        //}

        //public int RoundTotal
        //{
        //    get { return _roundtotal; }
        //    set { _roundtotal = RoundTotal; }
        //}

        //public PlayerOutcome RoundPlayerOutcome
        //{
        //    get { return _playerOutcome; }
        //    set { _playerOutcome = RoundPlayerOutcome; }
        //}

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
            //_isplaying = true;
        }

        #endregion

        #region METHODS



        #endregion
    }
}
