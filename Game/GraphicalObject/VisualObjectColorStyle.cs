using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameEngineAsteroid.GameObjects;
using GameEngineAsteroid.GamePrimitives;

namespace Game.GraphicalObject
{
    class VisualObjectColorStyle:VisualObject
    {
        protected Image _image = new Image();
        private GamePoint[] _points;
        private double centerX;
        private double centerY;
        public VisualObjectColorStyle(GameObject gameObject, Canvas canvas) : base(gameObject, canvas)
        {
            var bitmap = new BitmapImage(new Uri("pack://application:,,,/Resources/" + GetSpriteName()));
           _points = _gameObject.OriginalPoints;
            _image.Source = bitmap;
            _image.Stretch = Stretch.Fill;
            _image.Height = _points.Select(n=>n.Y).Max() - _points.Select(n=>n.Y).Min();
            _image.Width = _points.Select(n => n.X).Max() - _points.Select(n => n.X).Min(); ;
            centerX = _gameObject.OffsetCenterObject ? 0 : _image.Width / 2;
            centerY = _gameObject.OffsetCenterObject ? 0 : _image.Height / 2;
            _canvas.Children.Add(_image);
        }

        public override void DrawObject()
        {
            var transform = new RotateTransform(_gameObject.AngleRotate, centerX , centerY);
            _image.RenderTransform = transform;
           Canvas.SetLeft(_image, ImagePosition.X);
           Canvas.SetTop(_image, ImagePosition.Y);
        }

        public override void Remove()
        {
            _canvas.Children.Remove(_image);
        }

        private GamePoint ImagePosition
        {
            get
            {
                var X = _points.Select(n => n.X).Min() + _gameObject.CenterPolygonAbsolute.X;
                var Y = _points.Select(n => n.Y).Min() + _gameObject.CenterPolygonAbsolute.Y;
                return new GamePoint(X, Y);
            }
        }

        private string GetSpriteName()
        {
            if (_gameObject is Player) return "PlayerNotMoved.png";
            if (_gameObject is Bullet) return "Bullet.png";
            if (_gameObject is Laser) return "Laser.png";
            if (_gameObject is FlyingSaucer) return "FlyingSaucer.png";
            if (_gameObject is Asteroid)
            {
                if(((Asteroid)_gameObject).TypeVisualization==1) return "Asteroid_1.png";
                if (((Asteroid)_gameObject).TypeVisualization == 2) return "Asteroid_2.png";
            }
            if (_gameObject is FragmentAsteroid)
            {
                if (((FragmentAsteroid)_gameObject).TypeVisualization == 1) return "FragmentAsteroid_1.png";
                if (((FragmentAsteroid)_gameObject).TypeVisualization == 2) return "FragmentAsteroid_2.png";
            }
            throw new InvalidOperationException("Для данного типа нет спрайта.");
        }
    }
}
