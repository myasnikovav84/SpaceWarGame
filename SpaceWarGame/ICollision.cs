using System;
using System.Drawing;


namespace SpaceWarGame
{
    /// <summary>
    /// Интерфейс описывающий попадание одного объекта в другой
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
