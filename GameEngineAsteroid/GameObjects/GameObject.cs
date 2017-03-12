using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    //Базовый класс игрового объекта, отвечает за форму объекта
    //и параметры его перемещения по игровому полю.
    public abstract class GameObject
    {
        internal readonly GamePolygon _polygon;
        internal readonly Collider Collider;

        
        private float _speed;
        public float Speed
        {
            set
            {
                if (value <= 0) throw new ArgumentException("Скорость объекта должна быть положительным числом!");
                _speed = value;
            }
            get { return _speed; }
        }
        public float AngleRotate
        {
            protected set { _polygon.AngleRotate = value; }
            get { return _polygon.AngleRotate; }
        }

        public bool OffsetCenterObject => _polygon.OffcetCenter;

        public GamePoint CenterPolygonAbsolute
        {
            internal set { _polygon.CenterPolygonAbsolute = value; }
            get { return _polygon.CenterPolygonAbsolute; }
        }
        public GamePoint CenterPolygonRelative
        {
           protected set { _polygon.CenterPolygonRelative = value; }
            get { return _polygon.CenterPolygonRelative; }
        }
        public GamePoint[] DrawPoints => _polygon.GetDrawPoints();
        public GamePoint[] OriginalPoints
        {
            internal set { _polygon.Points = value; }
            get { return _polygon.Points; }
        }
        public float MaxRadiusObject => _polygon.MaxRadiusObject;

        internal bool IsDestroyed { get; private set; }
        internal virtual event EventHandler<GameObject> Destroyed;
        internal virtual event EventHandler<GameObject> Moved;
        
        protected GamePoint RotatePoint(GamePoint point) => _polygon.RotatePoint(point);
        protected GamePoint[] SetMove(float distance)=> _polygon.SetMove(distance);
        protected GamePoint[] SetMove(float distance,float angleRotate)
        {
            return _polygon.SetMove(distance,angleRotate);
        }
        protected GameObject(GamePoint[] notMovedPolygon, float angleGradus, 
            GamePoint creationGamePoint, bool offcetCenter=false)
        {
            _polygon = !offcetCenter?GamePolygon.GetPolygon(notMovedPolygon, angleGradus, creationGamePoint)
                :GamePolygon.GetPollygonOffcetCenter(notMovedPolygon,angleGradus,creationGamePoint);
            Collider = new Collider(_polygon);
            Speed = 10;
        }
        
        protected virtual void Move()
        {
            Moved?.Invoke(this,this);
        }

        internal virtual void Update() => Move();

        internal virtual void Destroy()
        {
            IsDestroyed = true;
            Destroyed?.Invoke(this,this);
        }

        internal virtual bool IsCollusion(GameObject obj) => false;

    }
}
