using System;
using System.Windows.Forms;

/* Мясников А.В.
 * C# уровень 2 Урок 1
 * Домашняя работа
 */
 
namespace SpaceWarGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form()
            {
                //form.Width = 800;
                //form.Height = 600;
                Width = Screen.PrimaryScreen.Bounds.Width/2,
                Height = Screen.PrimaryScreen.Bounds.Height/2            };

            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);

        }
    }
}
