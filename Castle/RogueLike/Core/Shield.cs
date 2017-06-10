namespace RogueLike.Core
{
    /// <summary>
    /// Represets entity for defence increasing
    /// </summary>
    public class Shield : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time; 

        public int Cost
        {
            get
            {
                return 200;
            }
        }

        public int MaxTime
        {
            get
            {
                return 120;
            }
        }

        public string Name
        {
            get
            {
                return "Shield";
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
            current.Defense += 10;
            current.Gold -= Cost;
        }

        public void TakeOff()
        {
            Player current = Game.Player;
            current.Defense -= 10;
        }
    }
}
