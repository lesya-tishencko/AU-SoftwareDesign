using RogueSharp;
using RLNET;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Core
{
    /// <summary>
    /// Represents game map
    /// </summary>
    public class GameField : Map
    {
        public List<Rectangle> Rooms { get; }
        private readonly List<Enemy> enemies = new List<Enemy>();

        public GameField()
        {
            Rooms = new List<Rectangle>();
        }

        /// <summary>
        /// Redraw game field
        /// </summary>
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                DrawCell(mapConsole, cell);
            }
            int i = 0;

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(mapConsole, this);
                // If player sees enemy add statistics
                if (IsInFov(enemy.X, enemy.Y))
                {
                    enemy.DrawStats(statConsole, i);
                    i++;
                }
            }
        }

        private void DrawCell(RLConsole console, Cell cell)
        {
            if (!cell.IsExplored)
                return;

            if (IsInFov(cell.X, cell.Y))
                // Cell is not wall, floor or other actor
                if (cell.IsWalkable)
                    console.Set(cell.X, cell.Y, new RLColor(129, 121, 107), new RLColor(20, 12, 28), '.');
                else
                    console.Set(cell.X, cell.Y, new RLColor(31, 38, 47), new RLColor(51, 56, 64), '#');
            else
                if (cell.IsWalkable)
                    console.Set(cell.X, cell.Y, new RLColor(71, 62, 45), RLColor.Black, '.');
                else
                    console.Set(cell.X, cell.Y, new RLColor(31, 38, 47), new RLColor(72, 77, 85), '#');
        }

        /// <summary>
        /// Update area which player can see
        /// </summary>
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        /// <summary>
        /// Correct moving actor
        /// </summary>
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            Cell cellTo = GetCell(x, y);
            if (cellTo.IsWalkable)
            {
                Cell cellFrom = GetCell(actor.X, actor.Y);
                SetCellProperties(cellFrom.X, cellFrom.Y, cellFrom.IsTransparent, true, cellFrom.IsExplored);
                SetCellProperties(cellTo.X, cellTo.Y, cellTo.IsTransparent, false, cellTo.IsExplored);
                actor.X = x;
                actor.Y = y;
                if (actor is Player)
                    UpdatePlayerFieldOfView();
                return true;
            }
            return false;
        }
        
        // for tests
        public Cell GetActorPosition(Actor actor) => GetCell(actor.X, actor.Y);
        public int GetEnemiesCount() => enemies.Count();

        /// <summary>
        /// Add player to map
        /// </summary>
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            Cell cell = GetCell(player.X, player.Y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, false, cell.IsExplored);
            UpdatePlayerFieldOfView();
            Game.TimeSystem.Add(player);
        }

        /// <summary>
        /// Add enemy to map
        /// </summary>
        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
            Cell cell = GetCell(enemy.X, enemy.Y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, false, cell.IsExplored);
            Game.TimeSystem.Add(enemy);
        }

        /// <summary>
        /// Remove enemy when it's dead
        /// </summary>
        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            Cell cell = GetCell(enemy.X, enemy.Y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, true, cell.IsExplored);
            Game.TimeSystem.Remove(enemy);
            // Check win
            if (enemies.Count == 0)
                Game.MessageLog.Add($"{Game.Player.Name} win!");
        }

        /// <summary>
        /// Get enemy in set position
        /// </summary>
        public Enemy GetEnemyAt(int x, int y) => enemies.FirstOrDefault(m => m.X == x && m.Y == y);

        /// <summary>
        /// Generate free position for actor
        /// </summary>
        public Point GetPositionForActor(Rectangle room)
        {
            if (HasFreePosition(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y)) return new Point(x, y);
                }
            }
            return null;
        }

        /// <summary>
        /// Check availability free area
        /// </summary>
        public bool HasFreePosition(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
                for (int y = 1; y <= room.Height - 2; y++)
                    if (IsWalkable(x + room.X, y + room.Y)) return true;

            return false;
        }
    }
}
