using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueSharp.Random;
using System;

namespace UnitTestRogueLike
{
    [TestClass]
    public class UnitTestCastle
    {
        [TestMethod]
        public void TestCreating()
        {
            RogueLike.Game.Initialize();

            Assert.IsTrue(RogueLike.Game.Map.Rooms.Count > 0);
            Assert.IsFalse(RogueLike.Game.Map.GetActorPosition(RogueLike.Game.Player).IsWalkable);
            Assert.IsTrue(RogueLike.Game.Map.GetEnemiesCount() > 0);
        }

        [TestMethod]
        public void TestMoving()
        {
            RogueLike.Game.Initialize();
            RogueSharp.Cell current = RogueLike.Game.Map.GetActorPosition(RogueLike.Game.Player);
            if (RogueLike.Game.CommandSystem.MovePlayer(RogueLike.Core.Direction.Up))
                Assert.AreEqual(current.Y - 1, RogueLike.Game.Map.GetActorPosition(RogueLike.Game.Player).Y);
            else
                Assert.IsFalse(RogueLike.Game.Map.GetCell(current.X, current.Y + 1).IsWalkable);
            current = RogueLike.Game.Map.GetActorPosition(RogueLike.Game.Player);
            if (RogueLike.Game.CommandSystem.MovePlayer(RogueLike.Core.Direction.Left))
                Assert.AreEqual(current.X - 1, RogueLike.Game.Map.GetActorPosition(RogueLike.Game.Player).X);
            else
                Assert.IsFalse(RogueLike.Game.Map.GetCell(current.X - 1, current.Y).IsWalkable);
        }

        [TestMethod]
        public void TestAttributes()
        {
            RogueLike.Game.Initialize();

            RogueLike.Game.Player.Gold = 120 + 200;
            RogueLike.Game.Inventory.Add("EnemyDetector");
            Assert.AreEqual(25, RogueLike.Game.Player.Awareness);

            RogueLike.Game.Inventory.Add("Shield");
            Assert.AreEqual(12, RogueLike.Game.Player.Defense);

            Assert.AreEqual(0, RogueLike.Game.Player.Gold);
            for (int i = 0; i < 13; ++i)
                RogueLike.Game.Inventory.Tick();

            Assert.AreEqual(2, RogueLike.Game.Player.Defense);
        }

        [TestMethod]
        public void TestLog()
        {
            RogueLike.Systems.MessageLog log = new RogueLike.Systems.MessageLog();
            for (int i = 0; i < 10; ++i)
                log.Add("check" + i.ToString());

            Assert.AreEqual(9, log.Count());

            log.Add("GAME OVER!");
            Assert.IsTrue(log.IsDied());
        }
    }
}
