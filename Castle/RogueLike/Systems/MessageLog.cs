using RLNET;
using System.Collections.Generic;

namespace RogueLike.Systems
{
    /// <summary>
    /// Represents log
    /// </summary>
    public class MessageLog
    {
        // max size of printed logs
        private static readonly int maxLines = 9;

        // Use a Queue to keep track of the lines of text
        // The first line added to the log will also be the first removed
        private readonly Queue<string> lines;

        public MessageLog()
        {
            lines = new Queue<string>();
        }

        // for test
        public int Count() => lines.Count;

        // Add a line to the MessageLog queue
        public void Add(string message)
        {
            lines.Enqueue(message);

            // When exceeding the maximum number of lines remove the oldest one.
            if (lines.Count > maxLines)
            {
                lines.Dequeue();
            }
        }

        public bool IsDied()
        {
            return lines.Contains("GAME OVER!");
        }

        public bool IsWin()
        {
            return lines.Contains($"{Game.Player.Name} win!");
        }

        // Draw each line of the MessageLog queue to the console
        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = this.lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}
