using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectDemo
{
    public class GameBase
    {
        protected int Size { get; set; }
        public Location Location { get; set; }
        public Image PlayerImage { get; set; }

        public GameBase(int size)
        {
            Size = size;
            PlayerImage = new Image();
            PlayerImage.Height = Size;
            PlayerImage.Width = Size;
        }
    }
}
