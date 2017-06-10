using RogueSharp;
using System.Linq;

namespace RogueLike.Systems
{
    /// <summary>
    /// Represents acts of alive entyties
    /// </summary>
    public class Behaviour : Interfaces.IActive
    {
        // Describe figth action by enemy
        public bool Act(Core.Enemy enemy, CommandSystem commandSystem)
        {
            Core.GameField map = Game.Map;
            Core.Player player = Game.Player;
            FieldOfView enemyFov = new FieldOfView(map);

            // if enemy doesn't see you yet, check can it see
            if (enemy.TurnsAlerted == 0)
            {
                enemyFov.ComputeFov(enemy.X, enemy.Y, enemy.Awareness, true);
                if (enemyFov.IsInFov(player.X, player.Y))
                {
                    Game.MessageLog.Add($"{enemy.Name} wish to fight {player.Name}");
                    enemy.TurnsAlerted = 1;
                }
            }

            // if enemy knows about player he tries to fight
            if (enemy.TurnsAlerted > 0)
            {
                Cell enemyCell = map.GetCell(enemy.X, enemy.Y);
                map.SetCellProperties(enemyCell.X, enemyCell.Y, enemyCell.IsTransparent, true, enemyCell.IsExplored);

                Cell playerCell = map.GetCell(player.X, player.Y);
                map.SetCellProperties(playerCell.X, playerCell.Y, playerCell.IsTransparent, true, playerCell.IsExplored);

                PathFinder pathFinder = new PathFinder(map);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(
                    map.GetCell(enemy.X, enemy.Y),
                    map.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    // if enemy can't moving to player
                    Game.MessageLog.Add($"{enemy.Name} wait player.");
                }
                
                map.SetCellProperties(enemyCell.X, enemyCell.Y, enemyCell.IsTransparent, false, enemyCell.IsExplored);
                map.SetCellProperties(playerCell.X, playerCell.Y, playerCell.IsTransparent, false, playerCell.IsExplored);

                if (path != null)
                {
                    try
                    {
                        commandSystem.MoveEnemy(enemy, path.Steps.First());
                    }
                    catch (NoMoreStepsException)
                    {
                        // if enemy can't moving to player
                        Game.MessageLog.Add($"{enemy.Name} can't catch player.");
                    }
                }
                enemy.TurnsAlerted++;
            }

            // ability to forget about player
            if (enemy.TurnsAlerted > 20)
                enemy.TurnsAlerted = 0;
            return true;
        }
    }
}
