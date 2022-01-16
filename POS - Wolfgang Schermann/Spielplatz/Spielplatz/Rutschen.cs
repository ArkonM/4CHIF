using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spielplatz
{
    class Rutschen
    {
        AutoResetEvent[] slides;
        public Rutschen(int rutschen)
        {
            slides = new AutoResetEvent[rutschen];
            for (int i = 0; i < rutschen; i++)
            {
                slides[i] = new AutoResetEvent(true);
            }
        }

        public void slide(Kinder k, int t)
        {
            int rutsche = WaitHandle.WaitAny(slides);

            Console.WriteLine("Kind nummer " + k.id + " besetzt die Rutsche " + (rutsche+1));
            Thread.Sleep(2000);
            Console.WriteLine("Kind nummer " + k.id + " ist gerutscht.");

            slides[rutsche].Set();

        }
    }
}
