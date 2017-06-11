using RLNET;

namespace RogueLike.Core
{
    public class Spirit : Enemy
    {
        /// <summary>
        /// Represents one of the enemy
        /// </summary>
        /// <returns></returns>
        public static Spirit Create()
        {
            int health = 15;
            return new Spirit
            {
                Attack = 4,
                AttackChance = 50,
                Awareness = 12,
                Color = RLColor.Cyan,
                Defense = 3,
                DefenseChance = 40,
                Gold = 40,
                Health = health,
                MaxHealth = health,
                Name = "Spirit",
                Speed = 18,
                Symbol = 's'
            };
        }
    }
}
