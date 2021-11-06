using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Flughafen
{
    class MediumAirport : Airport
    {
        public AutoResetEvent[] runway;
        public static ManualResetEvent DayNightChange { get; } = new ManualResetEvent(false);
        
        public MediumAirport()
        {
            runway = new AutoResetEvent[2];
            runway[0] = new AutoResetEvent(true);
            runway[1] = new AutoResetEvent(true);
        }

        public override void land(Plane p)
        {
            int usedRunway = WaitHandle.WaitAny(runway);

            DayNightChange.WaitOne();

            Console.WriteLine("Flugzeug " + p.id + " landet auf Landebahn " + (usedRunway + 1));
            Thread.Sleep(1000);
            Console.WriteLine("Flugzeug " + p.id + " ist auf dem Flughafen gelandet.");
            runway[usedRunway].Set();
        }
    }
}
