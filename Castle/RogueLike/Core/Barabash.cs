using RLNET;

namespace RogueLike.Core 
{
    /// <summary>
    /// Represent the strongest enemy
    /// </summary>
    public class Barabash : Enemy
    {
        public static Barabash Create()
        {
            int health = 40;
            return new Barabash
            {
                Attack = 8,
                AttackChance = 60,
                Awareness = 20,
                Color = RLColor.Magenta,
                Defense = 6,
                DefenseChance = 60,
                Gold = 75,
                Health = health,
                MaxHealth = health,
                Name = "Barabash",
                Speed = 24,
                Symbol = 'B'
            };
        }
    }
}
