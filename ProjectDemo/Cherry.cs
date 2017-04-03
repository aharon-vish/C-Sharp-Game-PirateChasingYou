using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectDemo
{
    class Cherry : GameBase
    {
        public Cherry(int size)
            : base(size)
        {
            Uri uri = new Uri(@"Images\cherry.jpg", UriKind.RelativeOrAbsolute);
            PlayerImage.Source = new BitmapImage(uri);
        }
    }


}
