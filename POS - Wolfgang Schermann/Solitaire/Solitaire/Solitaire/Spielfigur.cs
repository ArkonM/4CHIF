using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Solitaire
{
    class Spielfigur
    {

        public int CoordX;
        public int CoordY;
        public Ellipse el;

        Spielfigur(int CoordX, int CoordY, Ellipse el )
        {
            this.CoordX = CoordX;
            this.CoordY = CoordY;
            this.el = el;
        }

        public int getX()
        {
            return CoordX;
        }

        public int getY()
        {
            return CoordY;
        }

        public Ellipse getEl()
        {
            return el;
        }

        public void setX(int Coord)
        {
            this.CoordX = Coord;
        }

        public void setY(int Coord)
        {
            this.CoordY = Coord;
        }

        public void setEl(Ellipse el)
        {
            this.el = el;
        }
    }
}
