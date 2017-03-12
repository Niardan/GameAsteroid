using System;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid
{
    public class Weapon
    {
        public event EventHandler<GameObject> Create;
        public event EventHandler LaserRecharged; 
        public Weapon(GamePoint[] laserPolygonGamePoints, int laserLifeTime, int laserMaxNumberShots, int laserReloadTime,
            GamePoint[] bulletPolygonPonts, int bulletLifeTime, float bulletSpeed)
        {
            LaserPolygonGamePoints = laserPolygonGamePoints;
            LaserLifeTime = laserLifeTime;
            LaserMaxNumberShots = laserMaxNumberShots;
            LaserNumberShots = LaserMaxNumberShots;
            LaserReloadTime = laserReloadTime;
            LaserReload = 0;
            BulletPolygonPonts = bulletPolygonPonts;
            BulletLifeTime = bulletLifeTime;
            BulletSpeed = bulletSpeed;
        }

        private GamePoint[] LaserPolygonGamePoints { get; }
        private int LaserLifeTime { get; }
        private int LaserMaxNumberShots { get; }

        private int LaserReloadTime { get; }
        private int LaserReload { set; get; }
        private GamePoint[] BulletPolygonPonts { get; }
        private float BulletSpeed { get; }
        private int BulletLifeTime { get; }

        public int LaserNumberShots { private set; get; }
        public Laser ShotLaser(GamePoint creationPoint, float angleRotateGradus, Player player)
        {
            if (LaserNumberShots > 0)
            {
                LaserNumberShots--;
                var laser = new Laser(LaserPolygonGamePoints, angleRotateGradus, creationPoint, LaserLifeTime, player);
                Create?.Invoke(this,laser);
                return laser;
            }
            else
            {
                return null;
            }
        }
        public Bullet ShotBullet(GamePoint creationPoint, float angleRotateGradus)
        {
            var bullet = new Bullet(BulletPolygonPonts, angleRotateGradus, creationPoint, BulletLifeTime);
            bullet.Speed = BulletSpeed;
            Create?.Invoke(this,bullet);
            return bullet;
        }

        public void Update()
        {
            if (LaserNumberShots < LaserMaxNumberShots)
            {
                LaserReload++;
                if (LaserReload >= LaserReloadTime)
                {
                    LaserReload = 0;
                    LaserNumberShots++;
                    LaserRecharged?.Invoke(this,EventArgs.Empty);
                }
            }
        }
    }
}
