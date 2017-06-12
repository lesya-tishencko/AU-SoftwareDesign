using RLNET;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents player entity
    /// </summary>
    public class Player : Actor
    {
        static RLColor playerColor = new RLColor(222, 238, 214);
        static RLColor textColor = new RLColor(222, 238, 214);
        static RLColor goldColor = new RLColor(218, 212, 94);

        public Player()
        {
            Attack = 2;
            AttackChance = 50;
            Awareness = 15;
            Color = playerColor;
            Defense = 2;
            DefenseChance = 40;
            Gold = 0;
            Health = 120;
            MaxHealth = 120;
            Name = "Rogue";
            Speed = 10;
            Symbol = '@';
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", textColor);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", textColor);
            statConsole.Print(1, 5, $"Awareness:  {Awareness}", textColor);
            statConsole.Print(1, 7, $"Attack:  {Attack} ({AttackChance}%)", textColor);
            statConsole.Print(1, 9, $"Defense: {Defense} ({DefenseChance}%)", textColor);
            statConsole.Print(1, 11, $"Gold:    {Gold}", goldColor);
        }
    }
}
