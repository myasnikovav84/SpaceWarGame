using System;
using System.Drawing;

namespace SpaceWarGame
{
    public delegate void Message();

    /// <summary>
    /// Базовый класс объектов
    /// </summary>
    abstract class BaseObject:ICollision
    {
        
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected Image Image;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos">тип Point начальная позиция</param>
        /// <param name="dir">тип Point скорость и направление движения</param>
        /// <param name="size">тип Size размер объекта </param>
        /// <param name="image">Изображение объекта</param>
        public BaseObject(Point pos, Point dir, Size size, Image image):this(pos,dir,size)
        {
            Image = image;
        }

        /* реализация интерфейса ICollision, наложение друг на друга 2х объектов*/
        public Rectangle Rect => new Rectangle(Pos,Size);
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Прорисовка объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Rect);
        }

        /// <summary>
        /// Обновление объекта положения
        /// </summary>
        abstract public void Update(); 
    }


}
