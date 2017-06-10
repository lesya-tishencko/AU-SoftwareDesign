using RogueSharp;
using System.Text;
using RogueSharp.DiceNotation;

namespace RogueLike.Systems
{
    /// <summary>
    /// Represents command game system
    /// </summary>
    public class CommandSystem
    {
        public bool MovePlayer(Core.Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction)
            {
                case Core.Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Core.Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Core.Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Core.Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }

            if (Game.Map.SetActorPosition(Game.Player, x, y))
            {
                Game.Inventory.Tick();
                return true;
            }

            Core.Enemy monster = Game.Map.GetEnemyAt(x, y);

            // if monster is neighbor - to attack
            if (monster != null)
            {
                Attack(Game.Player, monster);
                Game.Inventory.Tick();
                return true;
            }

            return false;
        }

        // Attack!
        public void Attack(Core.Actor attacker, Core.Actor defender)
        {
            string attackMessage = "";
            string defenseMessage = "";

            // count of hits
            int hit = ResolveAttack(attacker, defender, attackMessage);
            // count of blocks
            int block = ResolveDefense(defender, hit, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage);
            if (!string.IsNullOrWhiteSpace(defenseMessage))
            {
                Game.MessageLog.Add(defenseMessage);
            }

            int damage = hit - block;
            // change health level
            ResolveDamage(defender, damage);
        }

        // Correct calculationg count of hits
        private static int ResolveAttack(Core.Actor attacker, Core.Actor defender, string attackMessage)
        {
            int hit = 0;

            attackMessage += $"{attacker.Name} attacks {defender.Name} and rolls: ";

            // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            // Look at the face value of each single die that was rolled
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage += termResult.Value + ", ";
                if (termResult.Value >= 100 - attacker.AttackChance) hit++;
            }

            return hit;
        }

        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private static int ResolveDefense(Core.Actor defender, int hit, string attackMessage, string defenseMessage)
        {
            int block = 0;

            if (hit > 0)
            {
                attackMessage += $"scoring {hit} hits.";
                defenseMessage += $"  {defender.Name} defends and rolls: ";

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage += termResult.Value + ", ";
                    if (termResult.Value >= 100 - defender.DefenseChance) block++;
                }
                defenseMessage += $"resulting in {block} blocks.";
            }
            else
            {
                attackMessage += "and misses completely.";
            }

            return block;
        }

        // Change actor's health level
        private static void ResolveDamage(Core.Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;
                Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");
                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private static void ResolveDeath(Core.Actor defender)
        {
            if (defender is Core.Player)
            {
                Game.MessageLog.Add($"  {defender.Name} was killed.");
                Game.MessageLog.Add("GAME OVER!");
            }
            else if (defender is Core.Enemy)
            {
                Game.Map.RemoveEnemy((Core.Enemy)defender);
                Game.Player.Gold += defender.Gold;
                Game.MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
            }
        }

        public bool IsPlayerTurn { get; set; }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }
        
        public void ActivateEnemies()
        {
            Interfaces.ITimer @object = Game.TimeSystem.Get();
            if (@object is Core.Player)
            {
                IsPlayerTurn = true;
                Game.TimeSystem.Add(Game.Player);
            }
            else
            {
                Core.Enemy monster = @object as Core.Enemy;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    Game.TimeSystem.Add(monster);
                }

                ActivateEnemies();
            }
        }

        // Try to put on attribute
        public bool PutOnAttr(Interfaces.IInventory item)
        {
            if (Game.Player.Gold >= item.Cost && !Game.Inventory.Contains(item.Name))
            {
                Game.Inventory.Add(item.Name);
                return true;
            }
            return false;
        }

        // Enemies moving
        public void MoveEnemy(Core.Enemy enemy, Cell cell)
        {
            if (!Game.Map.SetActorPosition(enemy, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                {
                    Attack(enemy, Game.Player);
                }
            }
        }
    }
}
