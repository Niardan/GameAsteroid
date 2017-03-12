using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public sealed class FragmentAsteroid:Enemy
    {
        
        public FragmentAsteroid(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, float speed,int typeVisualization)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint, typeVisualization)
        {
            if (speed <= 0) throw new ArgumentException("Скорость объекта не может быть меньше либо равным нулю.");
            Speed = speed;
        }

        protected override void Move()
        {
            SetMove(Speed);
            base.Move();
        }
    }
}
