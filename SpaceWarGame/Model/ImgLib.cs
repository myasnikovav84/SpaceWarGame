using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarGame
{
    static class ImgLib
    {
        /// <summary>
        /// местонахождения медиа файлов
        /// </summary>
        private static string _fileMedia = @"..\..\Media\";
        private static Dictionary<string, string> image = new Dictionary<string, string>
        {
            { "Ship", _fileMedia + @"\Image\ship.png"},
            { "Asteroid", _fileMedia + @"\Image\Asteroid.png"},
            { "Med", _fileMedia + @"\Image\med.png"}
        };
        private static Dictionary<string, string> imagePlanet = new Dictionary<string, string>
        {
            { "Planet1", _fileMedia + @"\Image\Planet1.png"},
            { "Planet2", _fileMedia + @"\Image\Planet2.png"},
            { "Planet3", _fileMedia + @"\Image\Planet3.png"},
            { "Planet4", _fileMedia + @"\Image\Planet4.png"},
            { "Planet5", _fileMedia + @"\Image\Planet5.png"},
            { "Planet6", _fileMedia + @"\Image\Planet6.png"}
        };
        public static Image GetImage(string key)
        {
            return (image.ContainsKey(key)) ? Image.FromFile(image[key]) : throw new IndexOutOfRangeException();
        }
        public static Image GetImagePlanet(string key)
        {
            return (imagePlanet.ContainsKey(key)) ? Image.FromFile(imagePlanet[key]) : throw new IndexOutOfRangeException();
        }
        public static int CountImgPlanet => imagePlanet.Count;

    }
}
