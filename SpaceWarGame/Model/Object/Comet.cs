using System;
using System.Drawing;

namespace SpaceWarGame
{
    class Comet : BaseObject
    {
        private Boolean _flagMed;
        /// <summary>
        /// Признак что не астероид, а аптечка
        /// </summary>
        public Boolean FlagMed => _flagMed;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        /// <param name="image">Изображение объекта</param>
        public Comet (Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
            _flagMed = false;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        /// <param name="image">Изображение объекта</param>
        /// <param name="flagMed">Признак что не астероид, а аптечка</param>
        public Comet(Point pos, Point dir, Size size, Image image, Boolean flagMed) : base(pos, dir, size, image)
        {
            _flagMed = flagMed;
        }

        /// <summary>
        /// обновление положения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
