using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class FastCarwash : Carwash
    {
        public AutoResetEvent[] streetClean = new AutoResetEvent[2];
        public AutoResetEvent streetDirty = new AutoResetEvent(true);

        public FastCarwash()
        {
            streetClean[0] = new AutoResetEvent(true);
            streetClean[1] = streetDirty;
        }


        public override void wash(Car c)
        {
            int i;
            if (c.rand > 50)
            {
                streetDirty.WaitOne();
                i = 1;
            }
            else
            {
                i = WaitHandle.WaitAny(streetClean);
            }

            Console.WriteLine("Auto " + c.id + " wird auf Straße " + (i + 1) + " gewaschen und hat einen Dirt gehalt von " + c.rand + ".", 1);
            Thread.Sleep(1000);
            Console.WriteLine("Auto " + c.id + " ist jetzt sauber und " + (i + 1) + " ist wieder frei", 1);

            if (c.rand > 50)
            {
                streetDirty.Set();
            }
            else
            {
                streetClean[i].Set();
            }
        }
    }
}
