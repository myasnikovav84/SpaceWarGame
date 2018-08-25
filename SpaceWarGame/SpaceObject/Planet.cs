using System;
using System.Drawing;

namespace SpaceWarGame
{
    /// <summary>
    /// класс планет
    /// </summary>
    class Planet : BaseObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        /// <param name="image">изображение</param>
        public Planet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
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
