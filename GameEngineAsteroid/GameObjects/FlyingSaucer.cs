using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
   public sealed class FlyingSaucer:Enemy
    {
        private Player _player;

        public FlyingSaucer(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, float speed, Player player)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint)
        {
            _player = player;
            if (speed <= 0) throw new ArgumentException("Скорость объекта не может быть меньше либо равным нулю.");
            Speed = speed;
        }

        protected override void Move()
        {
            GamePoint temp = _player.CenterPolygonAbsolute - this.CenterPolygonAbsolute; 
            GamePoint direction = Normalize(temp);
            CenterPolygonAbsolute += direction * Speed;
            base.Move();
        }
        private GamePoint Normalize(GamePoint p)
        {
            float length = (float)(Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2)));
            return p/length;
        }
    }
}
