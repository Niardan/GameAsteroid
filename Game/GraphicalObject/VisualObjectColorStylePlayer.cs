using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GameEngineAsteroid.GameObjects;

namespace Game.GraphicalObject
{
    class VisualObjectColorStylePlayer:VisualObjectColotStyle
    {
        private bool _PlayerMoved;
        private readonly Player _player;
        private readonly BitmapImage _spritePlayerNotMoved;
        private readonly BitmapImage _spritePlayerMoved;
        public VisualObjectColorStylePlayer(GameObject gameObject, Canvas canvas) : base(gameObject, canvas)
        {
            _player = gameObject as Player;
             _spritePlayerNotMoved = new BitmapImage(new Uri("pack://application:,,,/Resources/PlayerNotMoved.png"));
            _spritePlayerMoved = new BitmapImage(new Uri("pack://application:,,,/Resources/PlayerMoved.png"));
        }

        public override void DrawObject()
        {
            if (_PlayerMoved && _player.IsMoved)
            {
                _PlayerMoved = false;
                _image.Source = _spritePlayerMoved;
                _image.Height = _player.OriginalPoints.Select(n => n.Y).Max() - _player.OriginalPoints.Select(n => n.Y).Min();
                _image.Width = _player.OriginalPoints.Select(n => n.X).Max() - _player.OriginalPoints.Select(n => n.X).Min(); ;
            }
            else if (!_PlayerMoved && !_player.IsMoved)
            {
                _PlayerMoved = true;
                _image.Source = _spritePlayerNotMoved;
                _image.Height = _player.OriginalPoints.Select(n => n.Y).Max() - _player.OriginalPoints.Select(n => n.Y).Min();
                _image.Width = _player.OriginalPoints.Select(n => n.X).Max() - _player.OriginalPoints.Select(n => n.X).Min(); ;
            }
            base.DrawObject();
        }
    }
}
