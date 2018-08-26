using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace SpaceWarGame
{
    /// <summary>
    /// Класс реализации игры
    /// </summary>
    static class Game
    {
        #region таймеры
        /// <summary>
        /// базовый таймер, для планет, звезд
        /// </summary>
        private static Timer _timerBase = new Timer();

        /// <summary>
        /// таймер для астероида, используется для увеичесния скорости астероида
        /// </summary>
        private static Timer _timerComet = new Timer();

        /// <summary>
        /// Таймер для пуль
        /// </summary>
        private static Timer _timerBullet= new Timer();

        /// <summary>
        /// интервал базового таймера
        /// </summary>
        private static int _baseInervalTimer = 100;
        #endregion



        /// <summary>
        /// количество астероидов
        /// </summary>
        private static int _countComet = 5;

        /// <summary>
        /// количество аптечек
        /// </summary>
        private static int _countMed = 2;

        /// <summary>
        /// Уровень игры
        /// </summary>
        private static int _level = 1;

        /// <summary>
        /// Максимальное количество уровней
        /// </summary>
        private static int _maxLevel = 10;

        /// <summary>
        /// Базовое приращение объектов, чтобы через друз друга не перескакивали
        /// </summary>
        private static int _dirBase = 5;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static List<BaseObject> _objs = new List<BaseObject>();
        private static List<Comet> _comet = new List<Comet>();
        private static List<Bullet> _bullet1 = new List<Bullet>();

        private static Random _rnd = new Random();

        /// <summary>
        /// Корабль
        /// </summary>
        private static Ship _ship = 
            new Ship(new Point(10, 400), new Point(5, 5), new Size(100, 40), ImgLib.GetImage("Ship"));

        static Game()
        {
        }

        public static void Init (Form form)
        {
            // Графическое устройство для вывода графики   
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения 
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой 
            // Запоминаем размеры формы 
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом. 
            // для того, чтобы рисовать в буфере 
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            _timerBase.Interval = _baseInervalTimer;
            _timerComet.Interval = _baseInervalTimer;
            _timerBullet.Interval = _baseInervalTimer / _maxLevel;

            _timerBase.Start();
            _timerComet.Start();
            _timerBullet.Start();

            //события
            _timerBase.Tick += TimerBase_Tick;
            _timerComet.Tick += TimerComet_Tick;
            _timerBullet.Tick += TimerBullet_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;

            LogMessage.SetLog(LogMessage.ListLog.Init);
        }

        /// <summary>
        /// Прорисовка объектов
        /// </summary>
        public static void Draw()
        {
            // очищение графического поля и заливка черным цветом
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject e in _objs) e?.Draw();
            foreach(Comet e in _comet) e?.Draw();
            foreach (Bullet e in _bullet1) e?.Draw();

            _ship?.Draw();
            _ship?.ShipInfo();

            Buffer.Render(); //запись из буфера на форму
        }

        #region Load инициализация объектов

        /// <summary>
        /// инициализация объектов
        /// </summary>
        public static void Load()
        {
            LoadStarPlanet();
            LoadCometMed();
        }

        /// <summary>
        /// инициализация звезд и планет
        /// </summary>
        private static void LoadStarPlanet()
        {
            int size;
            for (int i = 0; i < 20; i++)
            {
                size = _rnd.Next(1, 5);
                _objs.Add(new Star(new Point(_rnd.Next(0, Width), _rnd.Next(0, Height)),
                    new Point(i / 4, 0), new Size(size, size)));
            }

            for (int i = 0; i < 4; i++)
            {
                size = _rnd.Next(40, 60);
                int indx = _rnd.Next(1, ImgLib.CountImgPlanet);
                _objs.Add(new Planet(
                     new Point(_rnd.Next(Width / (i + 1), Width / (i + 1)), _rnd.Next(0, Height))
                    , new Point((_objs.Count - 4) / 8, 0)
                    , new Size(size, size)
                    , ImgLib.GetImagePlanet("Planet" + indx)));
            }

            LogMessage.SetLog(LogMessage.ListLog.CreateStar);
        }

        /// <summary>
        /// инициализация астероидов и аптечек
        /// </summary>
        private static void LoadCometMed()
        {
            int size;

            for (int i = 0; i < _countComet; i++)
            {
                size = _rnd.Next(20, 40);
                _comet.Add(new Comet(new Point(_rnd.Next(0, Width), _rnd.Next(0, Height)), 
                    new Point(_rnd.Next(-1 * _dirBase, _dirBase), _rnd.Next(-1 * _dirBase, _dirBase)), 
                    new Size(size, size), ImgLib.GetImage("Asteroid")));
            }
            for (int i = 0; i < _countMed; i++)
            {
                _comet.Add(new Comet(new Point(_rnd.Next(0, Width), _rnd.Next(0, Height))
                    , new Point(_rnd.Next(-1*_dirBase, 0), 0), new Size(30, 30)
                    , ImgLib.GetImage("Med"), true));
            }
            LogMessage.SetLog(LogMessage.ListLog.CreateStar);
        }

        #endregion

        /// <summary>
        /// Действия по нажатию клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Space:
                    {
                        _bullet1.Add(new Bullet(new Point(_ship.Rect.X + 50, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
                        LogMessage.SetLog(LogMessage.ListLog.Shot);
                        break;
                    }
                case Keys.Up:
                    {
                        _ship.Up();
                        LogMessage.SetLog(LogMessage.ListLog.Up);
                        break;
                    }
                case Keys.Down:
                    {
                        _ship.Down();
                        LogMessage.SetLog(LogMessage.ListLog.Down);
                        break;
                    }
                case Keys.F1:
                    {
                        _ship.Medication();
                        LogMessage.SetLog(LogMessage.ListLog.Medication);
                        break;
                    }
            }
        }

        #region Update Обновление положения объектов
        /// <summary>
        /// Обновление базовых объектов, таких как звезды, планеты
        /// </summary>
        private static void UpdateBaseObject()
        {
            foreach (BaseObject e in _objs) e?.Update();
        }

        /// <summary>
        /// обновление положения комет
        /// </summary>
        private static void UpdateComet()
        {
            int Count = 0;
            foreach (Comet e in _comet)
            {
                if (e == null) continue;
                e?.Update();
                Count++;
            }

            if (Count == 0) NextLevel();

            #region Проверка на попадание кометы в корабль
            for (int i = 0; i < _comet.Count; i++)
            {
                if (_comet[i] != null && _ship.Collision(_comet[i]))
                {
                    if (!_comet[i].FlagMed)
                    {
                        _ship?.EnergyLow(_rnd.Next(1, 10 * _level));
                        LogMessage.SetLog(LogMessage.ListLog.Damage);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (_ship.Energy <= 0)
                        {
                            _ship?.Die();
                            LogMessage.SetLog(LogMessage.ListLog.Die);
                        }
                    }
                    else
                    {
                        _ship?.MedidAdd();
                        LogMessage.SetLog(LogMessage.ListLog.MedicAdd);
                    }
                    _comet[i] = null;
                }
            }
            #endregion
        }

        /// <summary>
        /// обновление положения пули
        /// </summary>
        private static void UpdateBullet()
        {
            for (int i = _bullet1.Count - 1; i >= 0; i--)
            {
                if (_bullet1[i] == null || _bullet1[i].GetPozX() > Width)
                {
                    _bullet1.Remove(_bullet1[i]);
                    continue;
                }
                _bullet1[i].Update();

                #region Проверка на попадание пуль в аптечку или комету

                for (int j = 0; j < _comet.Count; j++)
                {
                    if (_comet[j] != null && _bullet1[i].Collision(_comet[j]))
                    {
                         if (!_comet[j].FlagMed)
                        {
                            System.Media.SystemSounds.Hand.Play();
                            _ship.ScoreAdd(1 * _level);
                            LogMessage.SetLog(LogMessage.ListLog.TargetHit);
                        }
                        else
                        {
                            System.Media.SystemSounds.Beep.Play();
                            _ship.ScoreDrop(5);
                            LogMessage.SetLog(LogMessage.ListLog.MedicHit);
                        }
                        _bullet1[i] = null;
                        _comet[j] = null;
                        break;
                    }
                }

                #endregion
            }
        }

        #endregion


        private static void TimerBase_Tick (object sender, EventArgs e)
        {
            Draw();
            UpdateBaseObject();
        }

        private static void TimerComet_Tick (object sender, EventArgs e)
        {
            Draw();
            UpdateComet();
        }

        private static void TimerBullet_Tick (object sender, EventArgs e)
        {
            Draw();
            UpdateBullet();
        }

        private static void Finish()
        {
            _timerBase.Stop();
            _timerBullet.Stop();
            _timerComet.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,
            FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        private static void NextLevel()
        {
            _countComet++;
            _level++;
            _timerComet.Interval = _baseInervalTimer/ _level;
            _comet = new List<Comet>();
            _bullet1 = new List<Bullet>();
            LoadCometMed();


        }

    }
}
