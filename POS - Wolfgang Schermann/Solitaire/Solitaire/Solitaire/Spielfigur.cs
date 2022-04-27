using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    class Spielfigur
    {

        public int CoordX;
        public int CoordY;

        Spielfigur(int CoordX, int CoordY)
        {
            this.CoordX = CoordX;
            this.CoordY = CoordY;
        }

        public int getX()
        {
            return CoordX;
        }

        public int getY()
        {
            return CoordY;
        }

        public void setX(int Coord)
        {
            this.CoordX = Coord;
        }

        public void setY(int Coord)
        {
            this.CoordY = Coord;
        }
    }
}
