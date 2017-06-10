namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents game alive entity
    /// </summary>
    public interface IActor
    {
        // level of damage caused 
        int Attack { get; set; }
        // probability miss hit
        int AttackChance { get; set; }
        // field of view
        int Awareness { get; set; }
        // level of damage blocked
        int Defense { get; set; }
        // probability block hit
        int DefenseChance { get; set; }
        int Gold { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
    }
}
