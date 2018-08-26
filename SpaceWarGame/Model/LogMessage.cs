using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarGame
{
    static class LogMessage
    {
        static LogMessage()
        {     
        }

        static void Log(string msg, Action<string> Method) => Method(msg);

        public enum ListLog
        {
            Init,
            CreateStar,
            CreateComet,
            Shot,
            Up,
            Down,
            Medication,
            MedicAdd,
            Damage,
            Die,
            TargetHit,
            MedicHit
        }


        public static void SetLog(ListLog listLog)
        {
            string _msg;
            switch (listLog)
            {
                case ListLog.Init: _msg = "Init game"; break;
                case ListLog.CreateStar: _msg = "Создание звезд и планет"; break;
                case ListLog.CreateComet: _msg = "Создание астероидов"; break;
                case ListLog.Shot: _msg = "Выстрел!"; break;
                case ListLog.Up: _msg = "Вверх!"; break;
                case ListLog.Down: _msg = "Вниз!"; break;
                case ListLog.Medication: _msg = "Использование аптечки"; break;
                case ListLog.MedicAdd: _msg = "Новая аптечка"; break;
                case ListLog.Damage: _msg = "Попадание метеорита в корабль"; break;
                case ListLog.Die: _msg = "Крушение корабля"; break;
                case ListLog.TargetHit: _msg = "Попадание в цель! Так держать!"; break;
                case ListLog.MedicHit: _msg = "Попадание в аптечку"; break;
                default: _msg = "Неизвестный код сообщения"; break;
            }

            Log(_msg, SetLogConsole);
            Log(_msg, SetLogFile);
        }


        /// <summary>
        /// запись лога в консоль
        /// </summary>
        /// <param name="msg"></param>
        private static void SetLogConsole(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Запись лога в файл, пока не доделал
        /// </summary>
        /// <param name="msg"></param>
        private static void SetLogFile(string msg)
        {
            System.IO.File.WriteAllText("data.dat", msg);
        }

    }
}
