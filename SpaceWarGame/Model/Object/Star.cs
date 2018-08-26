using System;
using System.Drawing;

namespace SpaceWarGame
{
    /// <summary>
    /// класс звезд
    /// </summary>
    class Star : BaseObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Прорисовка объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// обновление положения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
