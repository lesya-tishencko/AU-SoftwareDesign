using RogueSharp;
using RLNET;
using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents alive entity with behaviour
    /// </summary>
    public class Actor : Interfaces.IActor, Interfaces.IDrawable, Interfaces.ITimer
    {
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Awareness { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Gold { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Time
        {
            get
            {
                return Speed;
            }
            set { }
        }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
                return;

            if (map.IsInFov(X, Y))
                console.Set(X, Y, Color, new RLColor(20, 12, 28), Symbol);
            else
                console.Set(X, Y, new RLColor(71, 62, 45), RLColor.Black, '.');
        }
    }
}
