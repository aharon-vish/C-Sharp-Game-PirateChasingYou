using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectDemo
{
    class GoodPlayer : GameBase
    {
        public GoodPlayer(int size)
            : base(size)
        {
            Uri uri = new Uri(@"Images\smiley_pirate_T.png", UriKind.RelativeOrAbsolute);
            PlayerImage.Source = new BitmapImage(uri);


        }
    }


}
