using RLNET;
using System.Collections.Generic;

namespace RogueLike.Core
{
    /// <summary>
    /// Represent attribute entity
    /// </summary>
    public class Inventory
    {
        private static RLColor textColor = new RLColor(222, 238, 214);
        private static RLColor goldColor = new RLColor(218, 212, 94);
        private Dictionary<Interfaces.IInventory, bool> items = new Dictionary<Interfaces.IInventory, bool>();

        public Inventory()
        {
            // add all existing attributes
            items.Add(new EnemyDetector(), false);
            items.Add(new Medicine(), false);
            items.Add(new Sword(), false);
            items.Add(new Shield(), false);
        }

        // Draw inventory statistics
        public void DrawStats(RLConsole inventoryConsole)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (item.Value)
                    // if item was taken
                    inventoryConsole.Print(1, 2 * i, $"Item: {item.Key.Name}, cost: {item.Key.Cost}", goldColor);
                else
                    inventoryConsole.Print(1, 2 * i, $"Item: {item.Key.Name}, cost: {item.Key.Cost}", textColor);
                i++;
            }
        }

        // Put on attribute
        public void Add(string item)
        {
            foreach (var attr in items.Keys)
            {
                if (attr.Name == item && !items[attr])
                {
                    // note like put on
                    items[attr] = true;
                    // set time of wearing
                    ((Interfaces.ITimer)attr).Time = attr.MaxTime;
                    attr.PutOn();
                    return;
                }
            }
        }

        // Decrement time of wearing each attribute
        public void Tick()
        {
            foreach (var attr in items.Keys)
            {
                if (items[attr])
                    ((Interfaces.ITimer)attr).Time -= Game.Player.Speed;
            }
            foreach (var attr in items.Keys)
            {
                if (items[attr] && ((Interfaces.ITimer)attr).Time <= 0)
                {
                    items[attr] = false;
                    attr.TakeOff();
                    return;
                }
            }
        }

        // Check puting on of attribute
        public bool Contains(string item) 
        {
            foreach (var attr in items.Keys)
                if (attr.Name == item) return items[attr];

            return false;
        }
    }
}
