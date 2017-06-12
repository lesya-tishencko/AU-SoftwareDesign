using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents entity for attack increasing
    /// </summary>
    public class Sword : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost => 240;

        public char Key => 'W';

        public string Name => "Sword";

        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        public void PutOn()
        {
            Player current = Game.Player;
            current.Attack += 10;
            if (_time == 0)
            {
                current.Gold -= Cost;
                _time = 200;
            }
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Attack -= 10;
        }
    }
}
