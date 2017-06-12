using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents micsture for health reduction
    /// </summary>
    public class Medicine : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost => 120;

        public char Key => 'M';

        public string Name => "Medicine";

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
            current.Health = current.MaxHealth;
            _time = 1;
            current.Gold -= Cost;
        }

        public void TakeOff() { }
    }
}
