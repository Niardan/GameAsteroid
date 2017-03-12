using System;
using System.Globalization;
using GameEngineAsteroid.GamePrimitives;

namespace GameEngineAsteroid.GameObjects
{
    public abstract class Enemy : GameObject
    {
        public readonly int TypeVisualization;
        protected Enemy(GamePoint[] notMovedPolygon, float angleRotateGradus, GamePoint creationGamePoint, int typeVisualization=1)
            : base(notMovedPolygon, angleRotateGradus, creationGamePoint)
        {
            TypeVisualization = typeVisualization;
        }
    }
}
