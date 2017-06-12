using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represets entity for defence increasing
    /// </summary>
    public class Shield : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost => 180;

        public char Key => 'S';

        public string Name => "Shield";

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
            current.Defense += 10;
            if (_time == 0)
            {
                current.Gold -= Cost;
                _time = 120;
            }
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Defense -= 10;
        }
    }
}
