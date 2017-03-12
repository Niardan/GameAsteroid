using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{

    //Класс "снаряда", выпускаемые игроком "пули".
    public sealed class Bullet : GameObject
    {

        private int LifeTime { get; set; }

        internal Bullet(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, int lifeTime)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint)
        {
            if (lifeTime <= 0) throw new ArgumentException("Время жизни не может быть меньше либо равным нулю.");
            LifeTime = lifeTime;
        }

        protected override void Move()
        {
            SetMove(Speed);
            LifeTime--;
            if (LifeTime == 0)
            {
                Destroy();
            }
            base.Move();
        }

        internal override bool IsCollusion(GameObject obj)
        {
            if (obj is Enemy &&!obj.IsDestroyed&& Collider.IsCollusion(obj.Collider))
            {
                obj.Destroy();
                this.Destroy();
                return true;
            }
            return false;
        }

    }
}
