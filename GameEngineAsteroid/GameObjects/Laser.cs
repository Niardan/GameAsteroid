using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public sealed class Laser : GameObject
    {
        private Player _player;
        public int LifeTime { get; set; }

        public Laser(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, int lifeTime, Player player)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint, true)
        {
            if (lifeTime <= 0) throw new ArgumentException("Время жизни не может быть меньше либо равным нулю.");
            LifeTime = lifeTime;
            _player = player;
        }

        protected override void Move()
        {
            AngleRotate=_player.AngleRotate;
            CenterPolygonAbsolute = _player.PointShot;
            LifeTime--;
            if (LifeTime == 0)
            {
               Destroy();
            }
            base.Move();
        }

        internal override bool IsCollusion(GameObject obj)
        {
            if (obj is Enemy && Collider.IsCollusion(obj.Collider))
            {
                obj.Destroy();
                return true;
            }
            return false;
        }

    }
}

