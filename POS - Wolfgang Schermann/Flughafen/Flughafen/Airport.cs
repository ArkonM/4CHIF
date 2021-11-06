using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Flughafen
{
    class Airport
    {

        public virtual void land(Plane p)
        {
            lock (this)
            {
                Console.WriteLine("Flugzeug " + p.id + " landet auf dem Flughafen.");
                Thread.Sleep(1000);
                Console.WriteLine("Flugzeug " + p.id + " ist auf dem Flughafen gelandet.");
            }
        }
    }
}
