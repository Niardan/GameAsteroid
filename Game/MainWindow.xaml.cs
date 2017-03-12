using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Game.GraphicalObject;
using GameEngineAsteroid;
using GameEngineAsteroid.GameObjects;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameField _gameField;
        private Player _player;
        private DispatcherTimer _timer;
        private List<Visualization> visualGameObjects;
        private bool _menu;
        private bool _blackStyle = true;
        private int _score = 0;
        private VisualLaserCountShots _laserCountShots;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            _timer.Tick += Timer_Tick;


        }

        public void LoadGame()
        {
           //Инициализирцем игру и подписываемся на игровые события
             InitializeGame init = new InitializeGame();
            _gameField = init.InitializeGameField(this.Height, this.Width);
            _gameField.ObjectCreate += GameFieldObjectCreate;
            _gameField.ObjectDestroy += GameFieldObjectDestroy;

            //получаем игрока
            _player = _gameField.Player;

            //генерируем коллекцию визуальных отображений
            GameFieldCanvas.Children.Clear();
            visualGameObjects = new List<Visualization>();
            visualGameObjects.Add(new Visualization(_player, GameFieldCanvas, _blackStyle));
            _laserCountShots = new VisualLaserCountShots(LaserPanel, _player.Weapon, this);

            //очки
            _score = 0;
            Score.Text = _score.ToString();
            
            //настраиваем графику и меню
            ChangeGraphic(_blackStyle);
            GameMenu.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Collapsed;
            NewGameButton.Visibility = Visibility.Collapsed;
            GameOverPanelScore.Visibility = Visibility.Collapsed;
            ContinueButton.Visibility = Visibility.Visible;
            MenuHeader.Text = "ПАУЗА";
            _menu = false;

            //запускаем таймер обновлений
            _timer.Start();
        }
        private void GameFieldObjectDestroy(object sender, GameObject e)
        {
            if (e is Asteroid)
                _score += 200;
            if (e is FragmentAsteroid)
                _score += 400;
            if (e is FlyingSaucer)
                _score += 300;
            if (e is Player)
            {
                GameOver();
            }
            var obj = visualGameObjects.First(n => n.GameObject == e);
            Score.Text = _score.ToString();
            obj.Remove();
            visualGameObjects.Remove(obj);
        }

        private void GameFieldObjectCreate(object sender, GameObject e)
        {
            visualGameObjects.Add(new Visualization(e, GameFieldCanvas, _blackStyle));
        }


        private void Timer_Tick(object sender, EventArgs e)
        {

            Update();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _player.MoveStart();
                    break;
                case Key.Left:
                    _player.RotareLeftStart();
                    break;
                case Key.Right:
                    _player.RotareRightStart();
                    break;
                case Key.Space:
                    _player.ShotBullet();
                    break;
                case Key.C:
                    _player.ShotLaser();
                    break;
                case Key.Escape:
                    Pause();
                    break; ;

            }
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _player.MoveStop();
                    break;
                case Key.Left:
                    _player.RotareLeftStop();
                    break;
                case Key.Right:
                    _player.RotareRightStop();
                    break;

            }
        }

        private void Pause()
        {
            if (!_menu)
            {
                _timer.Stop();
                _menu = true;
                GameMenu.Visibility = Visibility.Visible;
            }
            else
            {
                GameMenu.Visibility = Visibility.Collapsed;
                _menu = false;
                _timer.Start();
            }
        }

        private void GameOver()
        {
            ContinueButton.Visibility = Visibility.Collapsed;
            NewGameButton.Visibility = Visibility.Visible;
            ScoreGameOver.Visibility = Visibility.Visible;
            GameOverPanelScore.Visibility = Visibility.Visible;
            ScoreGameOver.Text = _score.ToString();
            MenuHeader.Text = "ИГРА ОКОНЧЕНА";
            Pause();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonChangeGraphic_OnClick(object sender, RoutedEventArgs e)
        {
            _blackStyle = !_blackStyle;
            ChangeGraphic(_blackStyle);

        }

        private void ChangeGraphic(bool blackStyle)
        {
            this.Resources = blackStyle
            ? new ResourceDictionary() { Source = new Uri("pack://application:,,,/StyleBlack.xaml") }
            : new ResourceDictionary() { Source = new Uri("pack://application:,,,/StyleColor.xaml") };
            if (_gameField != null)
            {
                foreach (var n in visualGameObjects)
                {
                    n.LoadStyle(blackStyle);
                }
                _laserCountShots.CountShots();
                Update();
            }
        }

        private void Update()
        {
            _gameField.Update();
            foreach (var obj in visualGameObjects)
            {
                obj.DrawObject();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_gameField != null)
            {
                _gameField.HeightField = (float)e.NewSize.Height;
                _gameField.WightField = (float)e.NewSize.Width;
            }
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }
        
        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            Pause();
        }
    }
}
