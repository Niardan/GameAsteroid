using System;
using GameEngineAsteroid;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public  class Asteroid:Enemy
    {
        private readonly EnemyBuilder _builder;
        internal Asteroid(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, float speed,int typeVisualization, EnemyBuilder builder)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint, typeVisualization)
        {
            _builder = builder;
            if (speed <= 0) throw new ArgumentException("Скорость объекта не может быть меньше либо равным нулю.");
            Speed = speed;
        }

        internal override void Destroy()
        {
            CreateFragmentAsteroid();
            base.Destroy();
        }

        protected override void Move()
        {
            SetMove(Speed);
            base.Move();
        }

        private void CreateFragmentAsteroid()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            var numberAsteroid = rand.Next(2,4);
            float step = 180/(numberAsteroid + 1);
            float angle = 0;
            for (int i = 0; i < numberAsteroid; i++)
            {
                angle += step;
                _builder.CreateFragmentAsteroid(CenterPolygonAbsolute, angle+AngleRotate);
            }
        }
    }
}
