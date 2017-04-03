using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo
{
    public class Location
    {
        public double Left { get; set; }
        public double Top { get; set; }

        public Location(double left, double top)
        {
            Left = left;
            Top = top;
        }
    }
}
