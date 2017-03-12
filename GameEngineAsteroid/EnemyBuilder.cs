using System;
using System.Collections.Generic;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid
{
    //Класс построитель противников.
    internal class EnemyBuilder
    {
        public readonly Dictionary<int, GamePoint[]> TypeAsteroids;
        public readonly Dictionary<int, GamePoint[]> TypeFragmentAsteroids;
        public readonly Dictionary<int, GamePoint[]> TypeFlyingSaucer;
        public float SpeedAsteriod { set; get; }
        public float SpeedFragmentAsteroid { set; get; }
        public float SpeedFlyingSaucer { set; get; }
        public event EventHandler<GameObject> Create;
        private readonly Random _rand;
        
        public EnemyBuilder(int speedAstroid, int speedFragmentAsteroid, int speedFlyingSaucer)
        {
            TypeAsteroids= new Dictionary<int, GamePoint[]>();
            TypeFlyingSaucer = new Dictionary<int, GamePoint[]>();
            TypeFragmentAsteroids = new Dictionary<int, GamePoint[]>();
            _rand = new Random((int)DateTime.Now.Ticks);
            SpeedAsteriod = speedAstroid;
            SpeedFlyingSaucer = speedFlyingSaucer;
            SpeedFragmentAsteroid = speedFragmentAsteroid;
            
        }

        public void AddPolygonAsteroid(GamePoint[] points)
        {
            if(points.Length<3) throw new ArgumentException("Полигон объекта не может содержать меньше трех точек");
            TypeAsteroids.Add(TypeAsteroids.Count,points);
        }
        public void AddPolygonFragmentAsteroid(GamePoint[] points)
        {
            if (points.Length < 3) throw new ArgumentException("Полигон объекта не может содержать меньше трех точек");
            TypeFragmentAsteroids.Add(TypeFragmentAsteroids.Count, points);
        }
        public void AddPolygonFlyingSaucer(GamePoint[] points)
        {
            if (points.Length < 3) throw new ArgumentException("Полигон объекта не может содержать меньше трех точек");
            TypeFlyingSaucer.Add(TypeFlyingSaucer.Count, points);
        }
        public Asteroid CreateAsteroid()
        {
            return CreateAsteroid(new GamePoint(0,0), _rand.Next(0, 360));
        }
        public Asteroid CreateAsteroid(GamePoint creationPoint)
        {
            return CreateAsteroid(creationPoint, _rand.Next(0, 360));
        }
        public Asteroid CreateAsteroid(GamePoint creationPoint, float angleMoveGradus)
        {
            if (TypeAsteroids.Count == 1)
            {
                throw new InvalidOperationException("В коллекции нет полигонов для отображения астероидов.");
            }
            var typeAsteroid = _rand.Next(0, TypeAsteroids.Count);
            var points = TypeAsteroids[typeAsteroid];
            var asteroid = new Asteroid(points, angleMoveGradus, creationPoint, SpeedAsteriod,typeAsteroid+1, this);
            Create?.Invoke(this,asteroid);
            return asteroid;
        }

        public FragmentAsteroid CreateFragmentAsteroid()
        {
            var fragment = CreateFragmentAsteroid(new GamePoint(0, 0), _rand.Next(0, 360));
            return fragment;
        }
        public FragmentAsteroid CreateFragmentAsteroid(GamePoint creationGamePoint)
        {
            return CreateFragmentAsteroid(creationGamePoint, _rand.Next(0, 360));
        }
        public FragmentAsteroid CreateFragmentAsteroid(GamePoint creationPoint, float angleMoveGradus)
        {
            if (TypeFragmentAsteroids.Count == 1)
            {
                throw new InvalidOperationException("В коллекции нет полигонов для отображения фрагментов астероидов.");
            }
            var typeAsteroid = _rand.Next(0, TypeFragmentAsteroids.Count);
            var points = TypeFragmentAsteroids[typeAsteroid];
            var fragmentAsteroid = new FragmentAsteroid(points, angleMoveGradus, creationPoint, SpeedFragmentAsteroid,typeAsteroid+1);
            Create?.Invoke(this, fragmentAsteroid);
            return fragmentAsteroid;
        }

        public FlyingSaucer CreateFlyingSaucer(Player player)
        {
            return CreateFlyingSaucer(new GamePoint(0,0),0, player);
        }
        public FlyingSaucer CreateFlyingSaucer(GamePoint creationGamePoint, Player player)
        {
            return CreateFlyingSaucer(creationGamePoint, _rand.Next(0, 360),player);
        }
        public FlyingSaucer CreateFlyingSaucer(GamePoint createGamePoint, float angleMoveGradus,Player player)
        {
            if (TypeFlyingSaucer.Count == 0)
            {
                throw new InvalidOperationException("В коллекции нет полигонов для отображения летающих тарелок.");
            }
            var points = TypeFlyingSaucer[_rand.Next(0, TypeFlyingSaucer.Count)];
            var flyingSaucer = new FlyingSaucer(points, angleMoveGradus, createGamePoint, SpeedFlyingSaucer, player);
            Create?.Invoke(this,flyingSaucer);
            return flyingSaucer;
        }
    }
}
