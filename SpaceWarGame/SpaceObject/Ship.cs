using System.Drawing;
using System;
namespace SpaceWarGame
{
    /// <summary>
    /// класс описывающий корабль
    /// </summary>
    class Ship : BaseObject
    {
        #region Energy 
        private int _energyMax = 100;
        /// <summary>
        /// Максимальное количество энергии
        /// </summary>
        public int EnergyMax => _energyMax;
        private int _energy;
        /// <summary>
        /// Энергия (жизнь)
        /// </summary>
        public int Energy => _energy;
        /// <summary>
        /// уменьшение энергии (жизни корабля)
        /// </summary>
        /// <param name="n"></param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        #endregion

        #region Medic
        private int _medic = 1;
        /// <summary>
        /// количество аптечек
        /// </summary>
        public int Medic => _medic;
        /// <summary>
        /// Использование аптечки
        /// </summary>
        public void Medication()
        {
            if (_medic > 0 && _energy < _energyMax)
            {
                _medic--;
                if (_energy + 10 > _energyMax)
                    _energy = _energyMax;
                else
                    _energy += 10;
            }
        }
        /// <summary>
        /// Добавление новой аптечки
        /// </summary>
        public void MedidAdd()
        {
            _medic++;
        }
        #endregion

        #region Score
        private int _score=0;
        /// <summary>
        /// Счет
        /// </summary>
        public int Score => _score;
        /// <summary>
        /// Добавленние очков
        /// </summary>
        /// <param name="score"></param>
        public void ScoreAdd(int score)
        {
            _score += score;
        }
        /// <summary>
        /// Уменьшение очков
        /// </summary>
        /// <param name="score"></param>
        public void ScoreDrop(int score)
        {
            _score -= score;
            if (_score < 0) _score = 0;
        }
        #endregion

        #region BaseObject
        /// <summary>
        /// Конструтор
        /// </summary>
        /// <param name="pos">начальное положение</param>
        /// <param name="dir">приращение (Скорость)</param>
        /// <param name="size">размер</param>
        public Ship(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
            _energy = EnergyMax;
        }

        public override void Update()
        {
        }
        #endregion

        #region Управление кораблем
        /// <summary>
        /// Движение вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Движение корабля вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// убит корабль
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }
        #endregion

        #region Текущее состояние
        /// <summary>
        /// вывод информации о корабле на экран
        /// </summary>
        public void ShipInfo()
        {
            Game.Buffer.Graphics.DrawString( this.ToString() , SystemFonts.DefaultFont, Brushes.White, 0,0);
        }
        public override string ToString()
        {
            return $"Energy: {this.Energy}\nScore: {this.Score}\nMedicine chest: {this.Medic} (F1)";
        }
        #endregion

        /// <summary>
        /// событие убит корабль
        /// </summary>
        public static event Message MessageDie;

    }
}