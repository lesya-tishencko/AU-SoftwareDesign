using RLNET;
using System;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents enemy entity
    /// </summary>
    public class Enemy : Actor
    {
        private static RLColor damageColor = new RLColor(68, 82, 79);
        private static RLColor remainColor = new RLColor(29, 45, 42);
        private static RLColor textColor = new RLColor(222, 238, 214);

        public void DrawStats(RLConsole statConsole, int position)
        {
            // draw statistics under player statistics
            int yPosition = 15 + (position * 2);
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // draw level of health
            statConsole.SetBackColor(3, yPosition, width, 1, damageColor);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, remainColor);
            statConsole.Print(2, yPosition, $": {Name}", textColor);
        }

        // counts of alerts (used when enemy sees player and make decision figth with him)
        public int TurnsAlerted { get; set; }

        public virtual void PerformAction(Systems.CommandSystem commandSystem)
        {
            var behavior = new Systems.Behaviour();
            behavior.Act(this, commandSystem);
        }
    }
}
