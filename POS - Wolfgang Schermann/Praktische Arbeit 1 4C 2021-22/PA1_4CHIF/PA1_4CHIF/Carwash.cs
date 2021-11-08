using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{

    class Carwash
    {
        public static ManualResetEvent DayNightChange { get; } = new ManualResetEvent(false);

        public virtual void wash(Car c)
        {
            lock (this)
            {
                DayNightChange.WaitOne();

                Console.WriteLine("Auto " + c.id + " wird gewaschen.", 1);
                Thread.Sleep(1000);
                Console.WriteLine("Auto " + c.id + " ist jetzt sauber.", 1);
            }
        }
    }
}
