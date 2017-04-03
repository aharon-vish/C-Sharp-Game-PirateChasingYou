using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectDemo
{
    class BadPlayer : GameBase
    {
        int TOP_Direction;
        int Left_Direction;

        public int Left_Direction1
        {
            get { return Left_Direction; }
            set { Left_Direction = value; }
        }
        public int TOP_Direction1
        {
            get { return TOP_Direction; }
            set { TOP_Direction = value; }
        }
        public BadPlayer(int size)
            : base(size)
        {
            Uri uri = new Uri(@"Images\Pirate-Smile-icon.png", UriKind.RelativeOrAbsolute);
            PlayerImage.Source = new BitmapImage(uri);
        }
        
    }


}