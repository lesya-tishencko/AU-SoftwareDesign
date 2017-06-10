namespace RogueLike.Core
{
    /// <summary>
    /// Represents micsture for health reduction
    /// </summary>
    public class Medicine : Interfaces.IInventory, Interfaces.ITimer
    {
        private int _time;

        public int Cost
        {
            get
            {
                return 250;
            }
        }

        public int MaxTime
        {
            get
            {
                return 1;
            }
        }

        public string Name
        {
            get
            {
                return "Medicine";
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
            current.Health = current.MaxHealth;
            current.Gold -= Cost;
        }

        public void TakeOff() { }
    }
}
