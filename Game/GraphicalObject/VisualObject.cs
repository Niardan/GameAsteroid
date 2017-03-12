using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;


namespace Game.GraphicalObject
{
    public abstract class VisualObject
    {

        protected GameObject _gameObject;
        protected Canvas _canvas;
   
        public VisualObject(GameObject gameObject, Canvas canvas)
        {
            _canvas = canvas;
            _gameObject = gameObject;
          
        }

        public abstract void DrawObject();

        public abstract void Remove();
        
        protected PointCollection ConvertGamePoint(GamePoint [] gamePoints)
        {
            PointCollection points =new PointCollection();
            for (int i = 0; i < gamePoints.Length; i++)
            {
                points.Add(new Point(gamePoints[i].X, gamePoints[i].Y)); 
            }
            return points;
        }
    }
}
