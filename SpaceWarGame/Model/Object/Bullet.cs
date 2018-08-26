using System;
using System.Drawing;

namespace SpaceWarGame
{
    /// <summary>
    /// Класс описывающий снаряды
    /// </summary>
    class Bullet:BaseObject
    {
         /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            if (Pos.X < Game.Width) Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// координата положения пули
        /// </summary>
        /// <returns></returns>
        public int GetPozX() => Pos.X;
    }
}
