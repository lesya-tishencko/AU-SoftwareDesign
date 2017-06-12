using RLNET;
using RogueSharp.Random;
using System;

namespace RogueLike
{
    public class Game
    {
        public static Core.GameField Map { get; private set; }
        public static Core.Player Player { get; set; }
        public static Core.Inventory Inventory { get; set; }

        private static bool gameProcess = true;
        private static bool renderRequired = true;
        public static Systems.CommandSystem CommandSystem { get; private set; }

        public static Systems.MessageLog MessageLog { get; private set; }
        public static Systems.TimeSystem TimeSystem { get; private set; }

        public static IRandom Random { get; private set; }
        
        private static RLRootConsole rootConsole;
        private static RLConsole mapConsole;
        private static RLConsole messageConsole;
        private static RLConsole statConsole;
        private static RLConsole inventoryConsole;

        public static void Initialize()
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            TimeSystem = new Systems.TimeSystem();
            Player = new Core.Player();
            Systems.MapGenerator mapGenerator = new Systems.MapGenerator(80, 48, 20, 13, 7);
            Map = mapGenerator.CreateMap();
            Map.UpdatePlayerFieldOfView();
            Inventory = new Core.Inventory();
            MessageLog = new Systems.MessageLog();
            CommandSystem = new Systems.CommandSystem();
        }

        public static void Main()
        {
            Initialize();
            string consoleTitle = "Castle roguelike";

            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            rootConsole = new RLRootConsole(fontFileName, 100, 70, 8, 8, 1f, consoleTitle);

            mapConsole = new RLConsole(80, 48);
            messageConsole = new RLConsole(80, 11);
            statConsole = new RLConsole(20, 70);
            inventoryConsole = new RLConsole(80, 11);

            // Set up a handler for RLNET's Update event
            rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            if (CommandSystem.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Up)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Core.Direction.Up);
                    }
                    else if (keyPress.Key == RLKey.Down)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Core.Direction.Down);
                    }
                    else if (keyPress.Key == RLKey.Left)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Core.Direction.Left);
                    }
                    else if (keyPress.Key == RLKey.Right)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Core.Direction.Right);
                    }
                    else if (keyPress.Key == RLKey.W)
                    {
                        didPlayerAct = CommandSystem.PutOnAttr(new Core.Sword());
                    }
                    else if (keyPress.Key == RLKey.S)
                    {
                        didPlayerAct = CommandSystem.PutOnAttr(new Core.Shield());
                    }
                    else if (keyPress.Key == RLKey.M)
                    {
                        didPlayerAct = CommandSystem.PutOnAttr(new Core.Medicine());
                    }
                    else if (keyPress.Key == RLKey.D)
                    {
                        didPlayerAct = CommandSystem.PutOnAttr(new Core.EnemyDetector());
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        rootConsole.Close();
                    }
                }

                if (didPlayerAct)
                {
                    renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
            else
            {
                CommandSystem.ActivateEnemies();
                renderRequired = true;
            }
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit(mapConsole, 0, 0, 80, 48, rootConsole, 0, 11);
            RLConsole.Blit(statConsole, 0, 0, 20, 70, rootConsole, 80, 0);
            RLConsole.Blit(messageConsole, 0, 0, 80, 11, rootConsole, 0, 70 - 11);
            RLConsole.Blit(inventoryConsole, 0, 0, 80, 11, rootConsole, 0, 0);
            rootConsole.Draw();
            if (gameProcess && renderRequired)
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();
                inventoryConsole.Clear();
                Map.Draw(mapConsole, statConsole);
                Player.Draw(mapConsole, Map);
                Inventory.DrawStats(inventoryConsole);
                if (MessageLog.IsDied())
                {
                    mapConsole.Clear();
                    mapConsole.Print(1, 1, "Game over because you dead!", RLColor.White);
                    gameProcess = false;
                }
                if (MessageLog.IsWin())
                {
                    mapConsole.Clear();
                    mapConsole.Print(1, 1, "You win!", RLColor.White);
                    gameProcess = false;
                }
                MessageLog.Draw(messageConsole);
                Player.DrawStats(statConsole);
                renderRequired = false;
            }
        }
    }
}
