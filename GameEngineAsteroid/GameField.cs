using System;
using System.Collections.Generic;
using System.Linq;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid
{
    public class GameField
    {
        private List<GameObject> _gameObjects;

        private Player _player;
        private Weapon _weapon;
        private EnemyBuilder _enemyBuilder;
        private readonly Random _rand;
        private readonly int _timeRespawnEnemy;
        private int _timeSpawn;

        public Player Player => _player;
        public Weapon Weapon => _weapon;

        public float HeightField { get; set; }

        public float WightField { get; set; }

        public event EventHandler<GameObject> Create;
        public event EventHandler<GameObject> Destroy;
        public event EventHandler<GameObject> Moved;
       

        public GameField(float heightField, float wightField, int timeRespawnEnemy)
        {
            HeightField = heightField;
            WightField = wightField;
            _timeRespawnEnemy = timeRespawnEnemy;
            _timeSpawn = timeRespawnEnemy;
            _gameObjects = new List<GameObject>();
            _rand = new Random((int)DateTime.Now.Ticks);
        }

    

        private void GameObject_Create(object sender, GameObject e)
        {
            _gameObjects.Add(e);
            e.Destroyed += GameObjectDestroyed;
            Create?.Invoke(this,e);
            e.Moved += GameObject_Moved; ;
        }

        private void GameObject_Moved(object sender, GameObject e)
        {
            if (e.CenterPolygonAbsolute.X + e.MaxRadiusObject < 0-1)
            {
                e.CenterPolygonAbsolute = new GamePoint(WightField+e.MaxRadiusObject,e.CenterPolygonAbsolute.Y);
            }
            if (e.CenterPolygonAbsolute.X - e.MaxRadiusObject > WightField+1)
            {
                e.CenterPolygonAbsolute = new GamePoint(0 - e.MaxRadiusObject, e.CenterPolygonAbsolute.Y);
            }
            if (e.CenterPolygonAbsolute.Y + e.MaxRadiusObject < 0-1)
            {
                e.CenterPolygonAbsolute = new GamePoint(e.CenterPolygonAbsolute.X, HeightField + e.MaxRadiusObject-1);
            }
            if (e.CenterPolygonAbsolute.Y - e.MaxRadiusObject > HeightField+1)
            {
                e.CenterPolygonAbsolute = new GamePoint(e.CenterPolygonAbsolute.X, 0 - e.MaxRadiusObject);
            }

            Moved?.Invoke(this,e);
        }

        private void GameObjectDestroyed(object sender, GameObject e)
        {
            Destroy?.Invoke(this, e);
            _gameObjects.Remove(e);
            e.Moved -= GameObject_Moved;
            e.Destroyed -= GameObjectDestroyed;
        }

        public void Update()
        {
            _timeSpawn--;
            if (_timeSpawn <= 0)
            {
                SpawnEnemy();
                _timeSpawn = _timeRespawnEnemy;
            }
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update();
            }
            var enemyCollection = _gameObjects.Where(n => n is Enemy).ToList();
            var notEnemyCollection = _gameObjects.Where(n => !(n is Enemy)).ToList();
            foreach (GameObject notEnemy in notEnemyCollection)
            {
                foreach (var enemy in enemyCollection)
                {
                    if(notEnemy.IsCollusion(enemy))
                        break;
                }
            }
        }

        public void InitializeWeapon(GamePoint[] laserPolygonGamePoints, int laserLifeTime, int laserMaxNumberShots, int laserReloadTime,
        GamePoint[] bulletPolygonPonts, int bulletLifeTime,float bulletSpeed)
        {
            _weapon = new Weapon(laserPolygonGamePoints, laserLifeTime, laserMaxNumberShots, laserReloadTime, bulletPolygonPonts, bulletLifeTime, bulletSpeed);
            _weapon.Create += GameObject_Create;
        }
        public void InitializePlayer(GamePoint[] notMovedPolygon, GamePoint[] movedPolygon, float angleRotateGradus, GamePoint creationGamePoint, Weapon weapon)
        {
            _player = new Player(notMovedPolygon, movedPolygon, angleRotateGradus,creationGamePoint,weapon);
            GameObject_Create(this, _player);
        }



        
        public void InitializeEnemyBuilder(int speedAstroid, int speedFragmentAsteroid, int speedFlyingSaucer)
        {
            _enemyBuilder = new EnemyBuilder(speedAstroid, speedFragmentAsteroid, speedFlyingSaucer);
            _enemyBuilder.Create += GameObject_Create;
        }
        public void AddPolygonAsteroid(GamePoint[] gamePoints)
        {
            _enemyBuilder.AddPolygonAsteroid(gamePoints);
        }
        public void AddPolygonFragmentAsteroid(GamePoint[] gamePoints)
        {
           _enemyBuilder.AddPolygonFragmentAsteroid(gamePoints);
        }
        public void AddPolygonFlyingSaucer(GamePoint[] gamePoints)
        {
            _enemyBuilder.AddPolygonFlyingSaucer(gamePoints);
        }

        private void CreateEnemy(Enemy enemy)
        {
            int side = _rand.Next(0, 4);
            switch (side)
            {
                case 0:
                    enemy.CenterPolygonAbsolute = new GamePoint(0 - enemy.MaxRadiusObject, _rand.Next(0,(int)HeightField));
                    break;
                case 1:
                    enemy.CenterPolygonAbsolute = new GamePoint(_rand.Next(0, (int)WightField), 0 - enemy.MaxRadiusObject);
                    break;
                case 2:
                    enemy.CenterPolygonAbsolute = new GamePoint(WightField + enemy.MaxRadiusObject, _rand.Next(0, (int)HeightField));
                    break;
                case 3:
                    enemy.CenterPolygonAbsolute = new GamePoint(_rand.Next(0, (int)WightField), HeightField + enemy.MaxRadiusObject);
                    break;
            }
           
        }
     
      
        private void SpawnEnemy()
        {
            int enemyType = _rand.Next(0,3);
            switch (enemyType)
            {
                case 0:
                    CreateEnemy(_enemyBuilder.CreateAsteroid());
                    break;
                case 1:
                    CreateEnemy(_enemyBuilder.CreateFragmentAsteroid());
                    break;
                case 2:
                    CreateEnemy(_enemyBuilder.CreateFlyingSaucer(_player));
                    break;
            }
        }
    }
}
