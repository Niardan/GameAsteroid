using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineAsteroid;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;

namespace Game
{
    class InitializeGame
    {

        public GameField InitializeGameField(double heightField, double wightField)
        {
            GameField _gameField;
            //создаем игровое поле 
            _gameField = new GameField((float)heightField, (float)wightField, 100);

            //создаем и конфигурируем построителя противников
            _gameField.InitializeEnemyBuilder(5, 10, 7);
            _gameField.AddPolygonAsteroid(GetAsteroidPolygonType1());
            _gameField.AddPolygonAsteroid(GetAsteroidPolygonType2());
            _gameField.AddPolygonFragmentAsteroid(GetFragmentAsteroidPolygonType1());
            _gameField.AddPolygonFragmentAsteroid(GetFragmentAsteroidPolygonType2());
            _gameField.AddPolygonFlyingSaucer(GetFlyingSaucerPolygon());

            //создаем и конфигурируем игрока
            _gameField.InitializeWeapon(GetLaserPolygon(), 30, 5, 200, GetBulletPolygon(), 18, 35);
            _gameField.InitializePlayer(GetPlayerPolygon(), GetPlayerPolygonMoved(), 180, new GamePoint(wightField / 2, heightField / 2), _gameField.Weapon);
            _gameField.Player.AngleRotateStep = 12;
            _gameField.Player.Speed = 2;
            _gameField.Player.TimeInertion = 15;

            return _gameField;
        }

        private GamePoint[] GetPlayerPolygon()
        {
            var points = new GamePoint[]
            {
              new GamePoint(122,160), new GamePoint(117,174), new GamePoint(106,162), new GamePoint(109,152),
                new GamePoint(83,129), new GamePoint(74,129), new GamePoint(65,102), new GamePoint(105,29), new GamePoint(111,40),
                new GamePoint(103,68), new GamePoint(108,68), new GamePoint(132,5), new GamePoint(138,2), new GamePoint(145,7),
                new GamePoint(168,69), new GamePoint(175,68), new GamePoint(165,42), new GamePoint(172,29), new GamePoint(211,100),
                new GamePoint(203,127), new GamePoint(195,128), new GamePoint(169,152), new GamePoint(171,161), new GamePoint(161,175),
                new GamePoint(155,159),
             };
            var polygon = GamePolygon.GetPolygon(points, 180, new GamePoint(0, 0));
            polygon.Scale = 0.4F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetPlayerPolygonMoved()
        {
            var points = new GamePoint[]
            {
               new GamePoint(122,160),new GamePoint(138,220),new GamePoint(155,159),  new GamePoint(122,160), new GamePoint(117,174), new GamePoint(106,162), new GamePoint(109,152),
                new GamePoint(83,129), new GamePoint(74,129), new GamePoint(65,102), new GamePoint(105,29), new GamePoint(111,40),
                new GamePoint(103,68), new GamePoint(108,68), new GamePoint(132,5), new GamePoint(138,2), new GamePoint(145,7),
                new GamePoint(168,69), new GamePoint(175,68), new GamePoint(165,42), new GamePoint(172,29), new GamePoint(211,100),
                new GamePoint(203,127), new GamePoint(195,128), new GamePoint(169,152), new GamePoint(171,161), new GamePoint(161,175),
                new GamePoint(155,159),
             };
            var polygon = GamePolygon.GetPolygon(points, 180, new GamePoint(0, 0));
            polygon.Scale = 0.4F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetLaserPolygon()
        {
            var points = new GamePoint[]
            {
                new GamePoint(0,0), new GamePoint(0,700), new GamePoint(5,700), new GamePoint(5,0),
             };
            return points;
        }
        private GamePoint[] GetAsteroidPolygonType1()
        {
            var points = new GamePoint[]
            {
                new GamePoint(105,216), new GamePoint(20,174), new GamePoint(0,116),
                new GamePoint(49,24), new GamePoint(123,0), new GamePoint(199,51), new GamePoint(220,138),
                new GamePoint(186,195), new GamePoint(150,184),
             };
            var polygon = GamePolygon.GetPolygon(points, 0, new GamePoint(0, 0));
            polygon.Scale = 0.8F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetAsteroidPolygonType2()
        {
            var points = new GamePoint[]
            {
                new GamePoint(125,212), new GamePoint(35,168), new GamePoint(1,105), new GamePoint(60,18), new GamePoint(129,0),
                new GamePoint(220,53), new GamePoint(213,142), new GamePoint(170,167),
            };
            var polygon = GamePolygon.GetPolygon(points, 0, new GamePoint(0, 0));
            polygon.Scale = 0.8F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetFragmentAsteroidPolygonType1()
        {
            var points = new GamePoint[]
            {
                new GamePoint(49,119), new GamePoint(0,77), new GamePoint(2,29), new GamePoint(49,1), new GamePoint(84,4), new GamePoint(109,67),
             };

            var polygon = GamePolygon.GetPolygon(points, 0, new GamePoint(0, 0));
            polygon.Scale = 0.8F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetFragmentAsteroidPolygonType2()
        {
            var points = new GamePoint[]
            {
              new GamePoint(79,101), new GamePoint(39,110), new GamePoint(0,70), new GamePoint(12,22), new GamePoint(31,0),
                new GamePoint(74,1), new GamePoint(93,24), new GamePoint(108,30), new GamePoint(114,71), new GamePoint(83,80),
             };
            var polygon = GamePolygon.GetPolygon(points, 0, new GamePoint(0, 0));
            polygon.Scale = 0.8F;
            return polygon.GetDrawPoints();
        }
        private GamePoint[] GetFlyingSaucerPolygon()
        {
            var points = new GamePoint[]
            {
             new GamePoint(401,322), new GamePoint(389,344), new GamePoint(338,343), new GamePoint(325,323), new GamePoint(293,322),
                new GamePoint(285,342), new GamePoint(233,345), new GamePoint(217,322), new GamePoint(191,322), new GamePoint(180,342),
                new GamePoint(129,346), new GamePoint(113,324), new GamePoint(88,323), new GamePoint(3,253), new GamePoint(86,187),
                new GamePoint(111,187), new GamePoint(193,122), new GamePoint(319,122), new GamePoint(399,185), new GamePoint(425,185),
                new GamePoint(513,254), new GamePoint(430,323)
             };
            var polygon = GamePolygon.GetPolygon(points, 0, new GamePoint(0, 0));
            polygon.Scale = 0.3F;
            return polygon.GetDrawPoints();

        }

        private GamePoint[] GetBulletPolygon()
        {
            var points = new GamePoint[]
            {
                new GamePoint(8,48), new GamePoint(0,8), new GamePoint(4,0), new GamePoint(11,0), new GamePoint(15,6),
            };
            var polygon = GamePolygon.GetPolygon(points, 180, new GamePoint(0, 0));
            return polygon.GetDrawPoints();
        }
    }
}
