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
        private int attack;
        private int attackChance;
        private int awareness;
        private int defense;
        private int defenseChance;
        private int gold;
        private int health;
        private int maxHealth;
        private string name;
        private int speed;

        public int Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
            }
        }

        public int AttackChance
        {
            get
            {
                return attackChance;
            }
            set
            {
                attackChance = value;
            }
        }

        public int Awareness
        {
            get
            {
                return awareness;
            }
            set
            {
                awareness = value;
            }
        }

        public int Defense
        {
            get
            {
                return defense;
            }
            set
            {
                defense = value;
            }
        }

        public int DefenseChance
        {
            get
            {
                return defenseChance;
            }
            set
            {
                defenseChance = value;
            }
        }

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                maxHealth = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

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
            set
            {

            }
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
