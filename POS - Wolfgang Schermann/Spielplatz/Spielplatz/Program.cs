using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spielplatz
{
    class Program
    {
        static void Main(string[] args)
        {
            int anzKinder=10;
            int rutschen = 2;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out anzKinder);
                if (args.Length > 1)
                {
                    int.TryParse(args[1], out rutschen);
                }
            }
            Rutschen slid = new Rutschen(rutschen);

            for(int i=0; i<anzKinder; i++)
            {
                Kinder k = new Kinder(i + 1, slid);
                k.ThreadStart();
            }
        }
    }
}
