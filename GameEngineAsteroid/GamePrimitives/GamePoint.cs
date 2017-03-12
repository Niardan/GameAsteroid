using System;

namespace GameEngineAsteroid.GamePrimitives
{
    //Структура координат игровых объектов.
    public struct GamePoint
    {
        public readonly float X;
        public readonly float Y;

        public GamePoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public GamePoint(double x, double y)
        {
            this.X = (float)x;
            this.Y = (float)y;
        }


        public static GamePoint operator -(GamePoint a, GamePoint b)
        {
            return new GamePoint(a.X - b.X, a.Y - b.Y);
        }
        public static GamePoint operator -(GamePoint a, float b)
        {
            return new GamePoint(a.X - b, a.Y - b);
        }
        public static GamePoint operator +(GamePoint a, GamePoint b)
        {
            return new GamePoint(a.X + b.X, a.Y + b.Y);
        }
        public static GamePoint operator +(GamePoint a, float b)
        {
            return new GamePoint(a.X + b, a.Y + b);
        }
        public static GamePoint operator *(GamePoint a, GamePoint b)
        {
            return new GamePoint(a.X * b.X, a.Y * b.Y);
        }
        public static GamePoint operator *(GamePoint a, float b)
        {
            return new GamePoint(a.X * b, a.Y * b);
        }
        public static GamePoint operator /(GamePoint a, GamePoint b)
        {
            return new GamePoint(a.X / b.X, a.Y / b.Y);
        }
        public static GamePoint operator /(GamePoint a, float b)
        {
            return new GamePoint(a.X / b, a.Y / b);
        }


        public static bool operator ==(GamePoint a, GamePoint b)
        {
            return Math.Abs(a.X - b.X) < 0.001 && Math.Abs(a.Y - b.Y) < 0.001;
        }
        public static bool operator !=(GamePoint a, GamePoint b)
        {
            return Math.Abs(a.X - b.X) > 0.001 || Math.Abs(a.Y - b.Y) > 0.001;
        }
        public override bool Equals(object obj)
        {
            if (obj is GamePoint)
            {
                var point = (GamePoint)obj;
                return Math.Abs(this.X - point.X) < 0.001 && Math.Abs(Y - point.Y) < 0.001;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * Y.GetHashCode();
        }

        //Методы возвращают расстояние между двумя точками.
        public static float Distance(GamePoint gamePointA, GamePoint gamePointB)
        {
            return (float)Math.Sqrt((gamePointA.X - gamePointB.X) * (gamePointA.X - gamePointB.X) + (gamePointA.Y - gamePointB.Y) * (gamePointA.Y - gamePointB.Y));
        }
        public float DistanceTo(GamePoint gamePoint)
        {
            return (float)Math.Sqrt((this.X - gamePoint.X) * (this.X - gamePoint.X) + (this.Y - gamePoint.Y) * (this.Y - gamePoint.Y));
        }
    }
}
