using NUnit.Framework;
using GameEngineAsteroid.GameObjects.GamePrimitives;

namespace Testing.TestPrimitives
{
    class ColliderTest
    {
        private Collider collider1;
        private Collider collider2;
        private Collider collider3;
        [SetUp]
        public void SetUpTestMethod()
        {
            var points = new GamePoint[]
              {
                new GamePoint(10,10),
                new GamePoint(20,10),
                new GamePoint(20,20),
                new GamePoint(10,20),
              };
            var polygon1 = GamePolygon.GetPolygon(points, 0, new GamePoint(10, 10));
            collider1 = new Collider(polygon1);
            var polygon2 = GamePolygon.GetPolygon(points, 0, new GamePoint(12, 12));
            collider2 = new Collider(polygon2);
            var polygon3 = GamePolygon.GetPolygon(points, 0, new GamePoint(25, 25));
            collider3 = new Collider(polygon3);
        }
        [Test]
        public void TestCollision()
        {
            Assert.AreEqual(true,collider1.IsCollusion(collider2));
            Assert.AreEqual(false,collider1.IsCollusion(collider3));
        }
    }
}
