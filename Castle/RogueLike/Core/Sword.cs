using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents entity for attack increasing
    /// </summary>
    public class Sword : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost
        {
            get
            {
                return 300;
            }
        }

        public int MaxTime
        {
            get
            {
                return 200;
            }
        }

        public string Name
        {
            get
            {
                return "Sword";
            }
        }

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
            current.Gold -= Cost;
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Attack -= 10;
        }
    }
}
