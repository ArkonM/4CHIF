using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Solitaire
{
    public class EllipseInfo
    {

        public Ellipse ellipse { get; set; }

        public int x { get; set; }

        public int y { get; set; }


        public EllipseInfo(Ellipse ellip, int col, int row)
        {
            ellipse = ellip;
            x = col;
            y = row;
        }

    }
}
