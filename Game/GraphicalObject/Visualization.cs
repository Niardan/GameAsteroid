using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameEngineAsteroid.GameObjects;

namespace Game.GraphicalObject
{
    class Visualization
    {
        private GameObject _gameObject;
        private Canvas _canvas;
        private VisualObject _visualObject;
        
        // public VisualObject VisualObject => _visualObject;


        public GameObject GameObject => _gameObject;

        public Visualization(GameObject gameObject, Canvas canvas, bool blackStyle = true)
        {
            _canvas = canvas;
            _gameObject = gameObject;          
                LoadStyle(blackStyle);
            
        }

        public void LoadStyle(bool blackStyle)
        {
            _visualObject?.Remove();

            if (blackStyle)
            {
                _visualObject = new VisualObjectBlackStyle(_gameObject, _canvas);
            }
            else
            {
                if (_gameObject is Player)
                {
                    _visualObject = new VisualObjectColorStylePlayer(_gameObject, _canvas);
                }
                else
                {
                    _visualObject = new VisualObjectColotStyle(_gameObject, _canvas);
                }
            }
             

        }

      

        public void DrawObject()
        {

            _visualObject.DrawObject();
        }

        public void Remove()
        {
            _visualObject.Remove();
        }
    }
}
