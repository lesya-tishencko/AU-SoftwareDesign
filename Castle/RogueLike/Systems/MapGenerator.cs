using RogueSharp;
using RogueSharp.DiceNotation;
using System;
using System.Linq;

namespace RogueLike.Systems
{
    /// <summary>
    /// Represents map generator
    /// </summary>
    public class MapGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly int maxRooms;
        private readonly int roomMaxSize;
        private readonly int roomMinSize;

        private readonly Core.GameField map;
        
        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize)
        {
            this.width = width;
            this.height = height;
            this.maxRooms = maxRooms;
            this.roomMaxSize = roomMaxSize;
            this.roomMinSize = roomMinSize;
            map = new Core.GameField();
        }
        
        /// <summary>
        /// Create game map
        /// </summary>
        public Core.GameField CreateMap()
        {
            map.Initialize(width, height);

            // try to create area for rooms
            for (int r = maxRooms; r > 0; r--)
            {
                int roomWidth = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomHeight = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomXPosition = Game.Random.Next(0, width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, height - roomHeight - 1);
                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                bool isIntersect = map.Rooms.Any(room => newRoom.Intersects(room));
                if (!isIntersect) map.Rooms.Add(newRoom);
            }
            
            // create rooms and set tunnels between its
            for (int r = 0; r < map.Rooms.Count; r++)
            {
                CreateRoom(map.Rooms[r]);
                if (r == 0) continue;

                int previousRoomCenterX = map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = map.Rooms[r].Center.X;
                int currentRoomCenterY = map.Rooms[r].Center.Y;

                // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                if (Game.Random.Next(1, 2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }
            // set start player position
            PlacePlayer();
            // set start player position
            PlaceEnemies();
            return map;
        }

        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
                for (int y = room.Top + 1; y < room.Bottom; y++)
                    map.SetCellProperties(x, y, true, true, false);
        }

        private void PlacePlayer()
        {
            Core.Player player = Game.Player;
            player.X = map.Rooms[0].Center.X;
            player.Y = map.Rooms[0].Center.Y;

            map.AddPlayer(player);
        }
        
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
                map.SetCellProperties(x, yPosition, true, true);
        }
        
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
                map.SetCellProperties(xPosition, y, true, true);
        }

        private void PlaceEnemies()
        {
            foreach (var room in map.Rooms)
            {
                // Each room has a 60% chance of having
                if (Dice.Roll("1D10") < 7)
                {
                    // Generate between 1 and 4
                    var numberOfEnemies = Dice.Roll("1D4");
                    for (int i = 0; i < numberOfEnemies; i++)
                    {
                        // Find a random walkable location in the room to place
                        Point randomRoomLocation = map.GetPositionForActor(room);
                        // It's possible that the room doesn't have space to place
                        if (randomRoomLocation != null)
                        {
                            // What type generate?
                            var typeEnemy = Dice.Roll("3D8");
                            Core.Enemy enemy;
                            if (typeEnemy < 9)
                                enemy = Core.Spirit.Create();
                            else if (typeEnemy == 20)
                                enemy = Core.Barabash.Create();
                            else
                                enemy = Core.Kobold.Create();
                            enemy.X = randomRoomLocation.X;
                            enemy.Y = randomRoomLocation.Y;
                            enemy.TurnsAlerted = 0;
                            map.AddEnemy(enemy);
                        }
                    }
                }
            }
        }
    }
}
