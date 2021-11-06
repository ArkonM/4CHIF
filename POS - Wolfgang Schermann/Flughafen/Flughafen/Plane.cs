using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Flughafen
{
    class Plane
    {
        public int id;
        MediumAirport air;
        Random r;

        public Plane(int id, MediumAirport air)
        {
            this.id = id;
            this.air = air;
            r = new Random();
        }

        public void ThreadStart()
        {
            Thread t = new Thread(fly);
            t.Start();
        }

        void fly()
        {
            Thread.Sleep(r.Next(1000,10000));
            air.land(this);
        }
    }
}
