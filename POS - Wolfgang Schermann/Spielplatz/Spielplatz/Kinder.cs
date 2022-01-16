using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spielplatz
{
    class Kinder
    {
        public int id;
        Rutschen rutsch;
        static Random r = new Random();

        public Kinder(int id, Rutschen rutsch)
        {
            this.id = id;
            this.rutsch = rutsch;
        }

        public void ThreadStart()
        {
            Thread t = new Thread(rutschen);
            t.Start();
        }

        public void rutschen()
        {
            int time = r.Next(5000, 10000);
            Thread.Sleep(time);
            rutsch.slide(this, time);
        }
    }
}
