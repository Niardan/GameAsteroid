using System;
using System.Linq;

namespace GameEngineAsteroid.GamePrimitives
{
    public class GamePolygon
    {
        private float _angleRotate;
        private float _scale = 1;
        public GamePoint[] Points { get; internal set; }
        public float MaxRadiusObject { get; private set; }

        public GamePoint CenterPolygonAbsolute { get; set; }
        public GamePoint CenterPolygonRelative{ get; set; }
        public readonly bool OffcetCenter;
        public float AngleRotate
        {
            get { return _angleRotate; }
            set
            {
                if (value > 360) value -= 360;
                if (value < 0) value += 360;
                _angleRotate = value;
            }
        }
        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                SetMaxRadius(CenterPolygonRelative);
            }
        }

        private GamePolygon(GamePoint[] gamePoints, GamePoint centerPolygon, float angleGradus, GamePoint creationGamePoint, bool offcetCenter=false)
        {
            if (gamePoints.Length < 3)
            {
                throw new ArgumentException("Полигон объекта не может иметь менее трех вершин.");
            }
            GamePoint[] polygonGamePoint = new GamePoint[gamePoints.Length];
            for (int i = 0; i < gamePoints.Length; i++)
            {
                polygonGamePoint[i] = gamePoints[i] - centerPolygon;
            }
            Points = polygonGamePoint;
            CenterPolygonAbsolute = creationGamePoint;
            OffcetCenter = offcetCenter;
            CenterPolygonRelative = centerPolygon;
            AngleRotate = angleGradus;
            SetMaxRadius(CenterPolygonRelative);
        }

        //Фабрика полигонов, генерирует полигон  по заданному массиву координат.
        //Объект строится с центром в заданной точке
        public static GamePolygon GetPolygon(GamePoint[] points, float angleRotationGradus, GamePoint creationGamePoint)
        {
            GamePoint centerPolygon = GetCenterPolygon(points);
            return new GamePolygon(points, centerPolygon, angleRotationGradus, creationGamePoint);
        }
        //Фабрика полигонов, генерирует полигон со смещенным центром по заданному массиву координат.
        //Объект строится с центром в заданной точке
        public static GamePolygon GetPollygonOffcetCenter(GamePoint[] gamePoints, float angleGradus, GamePoint creationGamePoint)
        {
            GamePoint centerPolygon = GetOffcetCenterPolygon(gamePoints);
            return new GamePolygon(gamePoints, centerPolygon, angleGradus, creationGamePoint,true);
        }
        //Нахождение центральной точки полигона,  используеся в фабрике.
        private static GamePoint GetCenterPolygon(GamePoint[] gamePoints)
        {
            float minPointX = gamePoints.Min(n => n.X);
            float maxPointX = gamePoints.Max(n => n.X);
            float minPointY = gamePoints.Min(n => n.Y);
            float maxPointY = gamePoints.Max(n => n.Y);
            return new GamePoint((maxPointX - minPointX) / 2 + minPointX, (maxPointY - minPointY) / 2 + minPointY);
        }
        //Нахождение смещенной центральной точки полигона,  используеся в фабрике.
        private static GamePoint GetOffcetCenterPolygon(GamePoint[] gamePoints)
        {
            float minPointX = gamePoints.Min(n => n.X);
            float maxPointX = gamePoints.Max(n => n.X);
            float minPointY = gamePoints.Min(n => n.Y);
            return new GamePoint((maxPointX - minPointX) / 2 + minPointX, minPointY);
        }

        //Расчет расстояния до максимально удаленной точки от центра полигона
        private void SetMaxRadius(GamePoint centerPolygon)
        {
            MaxRadiusObject = (float)Points.Select(n => n * Scale).Max(n => n.DistanceTo(centerPolygon));
        }
        //Поворот полигона вокруг центра
        private GamePoint[] RotatePolygon()
        {
            GamePoint[] rotateGamePoints = new GamePoint[Points.Length];
            for (int i = 0; i < Points.Length; i++)
            {
                rotateGamePoints[i] = RotatePoint(Points[i]);
            }
            return rotateGamePoints;
        }



        //Поворот точки вокруг центра
        internal GamePoint RotatePoint(GamePoint point)
        {
            var angleRad = (AngleRotate * Math.PI) / 180;
            var x = (point.X) * Math.Cos(angleRad) - (point.Y)
                * Math.Sin(angleRad);
            var y = (point.X) * Math.Sin(angleRad) + (point.Y)
                * Math.Cos(angleRad);
            return new GamePoint((float)x, (float)y);
        }
        
        //Перемещение полигона на расстояние
        public GamePoint[] SetMove(float distance)
        {
            return SetMove(distance, AngleRotate);
        }
        public GamePoint[] SetMove(float distance, float angleRotate)
        {
            var angleRad = (angleRotate * Math.PI) / 180;
            CenterPolygonAbsolute = new GamePoint(-Math.Sin(angleRad) * distance + CenterPolygonAbsolute.X
                , Math.Cos(angleRad) * distance + CenterPolygonAbsolute.Y);
            return GetDrawPoints();
        }

        //Возвращение повернутой фигуры в абсолютных координатах
        public GamePoint[] GetDrawPoints()
        {
            GamePoint[] newGamePoints = new GamePoint[Points.Length];

            var rotatePoints = RotatePolygon();
            for (int i = 0; i < newGamePoints.Length; i++)
            {
                newGamePoints[i] = rotatePoints[i] * Scale + CenterPolygonAbsolute;
            }

            return newGamePoints;
        }
    }
}
