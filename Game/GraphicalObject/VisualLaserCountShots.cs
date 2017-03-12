using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GameEngineAsteroid;
using GameEngineAsteroid.GameObjects;

namespace Game.GraphicalObject
{
    internal class VisualLaserCountShots
    {
        private readonly DockPanel _dockPanel;
        private readonly Weapon _weapon;
        private readonly Window _window;

        public VisualLaserCountShots(DockPanel dockPanel, Weapon weapon, Window window)
        {
            _dockPanel = dockPanel;
            _weapon = weapon;
            _window = window;
            _weapon.LaserRecharged += _weapon_LaserRecharged;
            _weapon.Create += _weapon_ShotCreate;
            CountShots();
        }

        private void _weapon_ShotCreate(object sender, GameObject e)
        {
            if(e is Laser) RemoveShot();
        }

        private void _weapon_LaserRecharged(object sender, EventArgs e)
        {
            AddShot();
        }

        public void CountShots()
        {
            int shots = _weapon.LaserNumberShots;
            _dockPanel.Children.Clear();
            for (int i = 0; i < shots; i++)
            {
                _dockPanel.Children.Add(GetBlock());
            }

        }

        public void AddShot()
        {
            _dockPanel.Children.Add(GetBlock());
        }

        public void RemoveShot()
        {
            if (_dockPanel.Children.Count > 0)
                _dockPanel.Children.RemoveAt(0);
        }

        private TextBlock GetBlock()
        {
            var block = new TextBlock {Style = (Style) _window.FindResource("Laser")};
            return block;
        }

    }
}
