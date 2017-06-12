using RLNET;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents weak enemy entity
    /// </summary>
    public class Kobold : Enemy
    {
        public static Kobold Create()
        {
            int health = 10;
            return new Kobold
            {
                Attack = 3,
                AttackChance = 30,
                Awareness = 10,
                Color = RLColor.Brown,
                Defense = 2,
                DefenseChance = 20,
                Gold = 10,
                Health = health,
                MaxHealth = health,
                Name = "Kobold",
                Speed = 14,
                Symbol = 'k'
            };
        }
    }
}
