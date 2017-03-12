using NUnit.Framework;
using GameEngineAsteroid.GameObjects.GamePrimitives;
namespace Testing
{
    [TestFixture]
    public class PointTest
    {
        private GamePoint point1;
        private GamePoint point2;

        [SetUp]
        public void SetUpTestMethod()
        {
             point1 = new GamePoint(10, 16);
             point2 = new GamePoint(5, 4);

        }
        [Test]
        public void TestSubtraction()
        {
            Assert.AreEqual(new GamePoint(5, 12), point1 - point2);
            Assert.AreEqual(new GamePoint(8, 14), point1 - 2);
        }
        [Test]
        public void TestAddition()
        {
            Assert.AreEqual(new GamePoint(15, 20), point1 + point2);
            Assert.AreEqual(new GamePoint(13,19),point1+3 );
        }
        [Test]
        public void TestMultiplication()
        {
            Assert.AreEqual(new GamePoint(50, 64), point1 * point2);
            Assert.AreEqual(new GamePoint(20, 32), point1 * 2);
        }
        [Test]
        public void TestDivision()
        {
            Assert.AreEqual(new GamePoint(2, 4), point1 / point2);
            Assert.AreEqual(new GamePoint(5, 8), point1 / 2);
        }
        [Test]
        public void TestEquality()
        {
            GamePoint point3 = new GamePoint(10,16);
            GamePoint point4 = new GamePoint(10, 2);
            GamePoint point5 = new GamePoint(2, 16);
            Assert.AreEqual(true, point1.Equals(point3));
            Assert.AreEqual(true, point1 == point3);
            Assert.AreEqual(false, point1.Equals(point4));
            Assert.AreEqual(false, point1 == point4);
            Assert.AreEqual(false, point1.Equals(point5));
            Assert.AreEqual(false, point1 == point5);

            Assert.AreEqual(false, point1 != point3);
            Assert.AreEqual(true, point1 != point4);
            Assert.AreEqual(true, point1 != point5);
        }
        [Test]
        public void TestDistance()
        {
            GamePoint point3 = new GamePoint(6, 5);
            GamePoint point4 = new GamePoint(6, 8);
            GamePoint point5 = new GamePoint(2, 5);
            GamePoint point6 = new GamePoint(9, 9);
            Assert.AreEqual(3, point3.DistanceTo(point4));
            Assert.AreEqual(4, point3.DistanceTo(point5));
            Assert.AreEqual(5, point3.DistanceTo(point6));

            Assert.AreEqual(3, GamePoint.Distance(point3, point4));
            Assert.AreEqual(4, GamePoint.Distance(point3, point5));
            Assert.AreEqual(5, GamePoint.Distance(point3, point6));
        
        }
    }
}
