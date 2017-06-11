using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Attribute for awareness increasing
    /// </summary>
    public class EnemyDetector : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost => 80;

        public char Key => 'D';

        public string Name => "EnemyDetector";

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
            current.Awareness += 10;
            if (_time == 0)
            {
                current.Gold -= Cost;
                _time = 50;
            }
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Awareness -= 10;
        }
    }
}
