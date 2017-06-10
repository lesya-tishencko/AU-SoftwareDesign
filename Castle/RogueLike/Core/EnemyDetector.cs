using System;

namespace RogueLike.Core
{
    public class EnemyDetector : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time; 

        public int Cost
        {
            get
            {
                return 120;
            }
        }

        public int MaxTime
        {
            get
            {
                return 50;
            }
        }

        public string Name
        {
            get
            {
                return "EnemyDetector";
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
            current.Awareness += 10;
            current.Gold -= Cost;
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Awareness -= 10;
        }
    }
}
