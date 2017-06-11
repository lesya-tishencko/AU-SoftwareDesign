namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents game alive entity
    /// </summary>
    public interface IActor
    {
        /// <summary>
        /// Level of damage caused 
        /// </summary>
        int Attack { get; set; }
        /// <summary>
        /// Probability miss hit
        /// </summary>
        int AttackChance { get; set; }
        /// <summary>
        /// Field of view
        /// </summary>
        int Awareness { get; set; }
        /// <summary>
        /// Level of damage blocked
        /// </summary>
        int Defense { get; set; }
        /// <summary>
        /// probability block hit
        /// </summary>
        int DefenseChance { get; set; }
        /// <summary>
        /// Count of money
        /// </summary>
        int Gold { get; set; }
        /// <summary>
        /// Health level
        /// </summary>
        int Health { get; set; }
        /// <summary>
        /// Max health level
        /// </summary>
        int MaxHealth { get; set; }
        /// <summary>
        /// Actor's name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Actor's speed
        /// </summary>
        int Speed { get; set; }
    }
}
