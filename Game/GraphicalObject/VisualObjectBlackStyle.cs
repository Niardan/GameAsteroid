using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameEngineAsteroid.GameObjects;

namespace Game.GraphicalObject
{
    class VisualObjectBlackStyle:VisualObject
    {
        private Polygon _polygon;
        public VisualObjectBlackStyle(GameObject gameObject, Canvas canvas) : base(gameObject, canvas)
        {
            _polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 3
            };
            _canvas.Children.Add(_polygon);
        }
        public override void DrawObject()
        {

            _polygon.Points = ConvertGamePoint(_gameObject.DrawPoints);

        }
        public override void Remove()
        {
            _canvas.Children.Remove(_polygon);
        }
    }
}
