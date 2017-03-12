using System;
using System.Collections.Generic;
using System.Linq;
using GameEngineAsteroid;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public class Player : GameObject
    {
        class Acceleration
        {
            public float Speed { set; get; }
            public float Angle { set; get; }
            public float TimeInertion { set; get; }
            public Acceleration(float speed, float angle, float time)
            {
                Speed = speed;
                Angle = angle;
                TimeInertion = time;
            }
        }
        public Weapon Weapon => _weapon;
        public int TimeInertion { set; get; }
        public float AngleRotateStep { set; get; }
        public GamePoint PointShot
        {
            get
            {
                var pointShot = new GamePoint(CenterPolygonRelative.X,
                    OriginalPoints.Select(n => n.Y).Max()+CenterPolygonRelative.Y);
                return RotatePoint(pointShot)+CenterPolygonAbsolute;
            }
        }
        
        private readonly GamePoint[] _movedPolygon;
        private readonly GamePoint[] _notMovedPolygon;
        private bool _isMoved;
        public bool IsMoved => _isMoved;
        private bool _isRotateLeft;
        private bool _isRotateRight;
        private readonly List<Acceleration> _accelerations;
        private readonly Weapon _weapon;

        public Player(GamePoint[] notMovedPolygon, GamePoint[] movedPolygon, float angleRotateGradus, GamePoint creationGamePoint, Weapon weapon)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint)
        {
            _weapon = weapon;
            _accelerations = new List<Acceleration>();
            _movedPolygon =
                GamePrimitives.GamePolygon.GetPolygon(movedPolygon, angleRotateGradus, creationGamePoint).Points;
            _notMovedPolygon = OriginalPoints;
        }

        internal override bool IsCollusion(GameObject obj)
        {
            if (obj is Enemy && !obj.IsDestroyed && Collider.IsCollusion(obj.Collider))
            {
                this.Destroy();
                return true;
            }
            return false;
        }
        protected override void Move()
        {
            if (_accelerations.Count > 0)
            {
                for (int i = 0; i < _accelerations.Count; i++)
                {
                    SetMove(_accelerations[i].Speed, _accelerations[i].Angle);
                    _accelerations[i].TimeInertion--;
                    if (_accelerations[i].TimeInertion <= 0) _accelerations.Remove(_accelerations[i]);
                }
                _accelerations.RemoveAll(n => n.TimeInertion <= 0);
            }
            base.Move();
        }

        internal override void Update()
        {
            if (_isMoved) _accelerations.Add(new Acceleration(Speed, AngleRotate, TimeInertion));
            if (_isRotateRight) AngleRotate += AngleRotateStep;
            if (_isRotateLeft) AngleRotate -= AngleRotateStep;
            Weapon.Update();
            base.Update();
        }

        public void MoveStart()
        {
            OriginalPoints = _movedPolygon;
            _isMoved = true;
        }

        public void MoveStop()
        {
            OriginalPoints = _notMovedPolygon;
            _isMoved = false;
        }

        public void RotareLeftStart()
        {
            _isRotateRight = false;
            _isRotateLeft = true;

        }
        public void RotareLeftStop()
        {
            _isRotateLeft = false;
        }
        public void RotareRightStart()
        {
            _isRotateLeft = false;
            _isRotateRight = true;
        }
        public void RotareRightStop()
        {
            _isRotateRight = false;
        }


        public void ShotBullet()
        {
            _weapon.ShotBullet(PointShot, AngleRotate);
        }

        public void ShotLaser()
        {
            _weapon.ShotLaser(PointShot, AngleRotate, this);
        }

    }
}
