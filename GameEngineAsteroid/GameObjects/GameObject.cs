using System;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public abstract class GameObject
    {
        private readonly GamePolygon _polygon;
        internal readonly Collider Collider;
        internal bool IsDestroyed { get; private set; }

        public GamePoint CenterPolygonAbsolute
        {
            set { _polygon.CenterPolygonAbsolute = value; }
            get { return _polygon.CenterPolygonAbsolute; }
        }

        public GamePoint CenterPolygonRelative
        {
           protected set { _polygon.CenterPolygonRelative = value; }
            get { return _polygon.CenterPolygonRelative; }
        }

        public bool OffsetCenterObject => _polygon.OffcetCenter;
        public float AngleRotate
        {
          protected  set { _polygon.AngleRotate = value; }
            get { return _polygon.AngleRotate; }
        }

        protected GamePoint RotatePoint(GamePoint point)
        {
            return _polygon.RotatePoint(point);
        }
        public float Speed { set; get; }
        internal virtual event EventHandler<GameObject> Destroyed;
        internal virtual event EventHandler<GameObject> Moved;
        public GamePoint[] DrawPoints => _polygon.GetDrawPoints();

        public GamePoint[] OriginalPoints
        {
            set { _polygon.Points = value; }
            get { return _polygon.Points; }
        }

        public float MaxRadiusObject => _polygon.MaxRadiusObject;

        protected GamePoint[] SetMove(float distance)
        {
            return _polygon.SetMove(distance);
        }
        protected GamePoint[] SetMove(float distance,float angleRotate)
        {
            return _polygon.SetMove(distance,angleRotate);
        }
        protected GameObject(GamePoint[] notMovedPolygon, float angleGradus, GamePoint creationGamePoint, bool offcetCenter=false)
        {
            _polygon = !offcetCenter?GamePolygon.GetPolygon(notMovedPolygon, angleGradus, creationGamePoint)
                :GamePolygon.GetPollygonOffcetCenter(notMovedPolygon,angleGradus,creationGamePoint);
            Collider = new Collider(_polygon);
            Speed = 10;
        }
        internal virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            Moved?.Invoke(this,this);
        }
       

        internal virtual void Destroy()
        {
            IsDestroyed = true;
            Destroyed?.Invoke(this,this);
        }
      
        //TODO поменять логику работы коллайдеров
        internal virtual bool IsCollusion(GameObject obj)
        {
            return false;
        }

    }
}
