using NUnit.Framework;
using GameEngineAsteroid.GameObjects.GamePrimitives;

namespace Testing.TestPrimitives
{
    [TestFixture]
    class PolygonTest
    {
        private GamePoint[] gamePoints;
        private GamePolygon gamePolygon;
        [SetUp]
        public void SetUpTestMethod()
        {
            gamePoints = new GamePoint[]
            {
                new GamePoint(10,10),
                new GamePoint(20,10),
                new GamePoint(20,20),
                new GamePoint(10,20),
            };
            gamePolygon = GamePolygon.GetPolygon(gamePoints, 0, new GamePoint(10, 10));
        }
        [Test]
        public void TestMaxRadius()
        {
            Assert.AreEqual(7.071, gamePolygon.MaxRadius, 0.01);
        }
        [Test]
        public void TestCenterPolygon()
        {
            Assert.AreEqual(new GamePoint(10, 10), gamePolygon.CenterPolygon);
        }
        [Test]
        public void TestCenterPolygonOffcet()
        {
            gamePolygon = GamePolygon.GetPollygonOffcetCenter(gamePoints, 0, new GamePoint(10, 10));
            var newPoints = new GamePoint[]
           {
                new GamePoint(5, 10),
                new GamePoint(15, 10),
                new GamePoint(15, 20),
                new GamePoint(5, 20),
           };
            var drawPoints = gamePolygon.GetDrawPolygon();
            CollectionAssert.AreEqual(newPoints, drawPoints);
        }
        [Test]
        public void TestAngle()
        {
            gamePolygon.SetRotation(380);
            Assert.AreEqual(20, gamePolygon.AngleRotate);
        }
        [Test]
        public void TestScale()
        {
            gamePolygon.Scale = 2;

            var newPoints = new GamePoint[]
            {
                new GamePoint(0, 0),
                new GamePoint(20, 0),
                new GamePoint(20, 20),
                new GamePoint(0, 20),
            };
            var drawPoints = gamePolygon.GetDrawPolygon();
            CollectionAssert.AreEqual(newPoints, drawPoints);
        }
        [Test]
        public void TestMove()
        {
            gamePolygon.SetMove(10);

            var newPoints = new GamePoint[]
            {
                new GamePoint(5,15),
                new GamePoint(15,15),
                new GamePoint(15,25),
                new GamePoint(5,25)
            };
            var drawPoints = gamePolygon.GetDrawPolygon();
            CollectionAssert.AreEqual(newPoints, drawPoints);
            gamePolygon.SetMove(10, 90);
            newPoints = new GamePoint[]
             {
                new GamePoint(-5,15),
                new GamePoint(5,15),
                new GamePoint(5,25),
                new GamePoint(-5,25)
             };
            drawPoints = gamePolygon.GetDrawPolygon();
            CollectionAssert.AreEqual(newPoints, drawPoints);
        }

    }
}