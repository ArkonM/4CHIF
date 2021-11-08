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
        public static ManualResetEvent St { get; } = new ManualResetEvent(false);
        public static ManualResetEvent Ld { get; } = new ManualResetEvent(false);
        public static int Anzahl { get; set; } = 0;
        Fluglotse l;
        
        public MediumAirport(Fluglotse l)
        {
            this.l = l;

            runway = new AutoResetEvent[2];
            runway[0] = new AutoResetEvent(true);
            runway[1] = new AutoResetEvent(true);

        }

        public override void land(Plane p)
        {

            int usedRunway;
            if (p.startet)
            {
                usedRunway = WaitHandle.WaitAny(runway);
                lock (this)
                {
                    St.WaitOne();
                    Anzahl++;
                }


                DayNightChange.WaitOne();

                Console.WriteLine("Flugzeug " + p.id + " startet auf Landebahn " + (usedRunway + 1));
                Thread.Sleep(1000);
                Console.WriteLine("Flugzeug " + p.id + " ist von dem Flughafen gestartet.");
                runway[usedRunway].Set();


            }
            else
            {

                usedRunway = WaitHandle.WaitAny(runway);
                lock (this)
                {
                    Ld.WaitOne();
                    Anzahl++;
                }

                DayNightChange.WaitOne();

                Console.WriteLine("Flugzeug " + p.id + " landet auf Landebahn " + (usedRunway + 1));
                Thread.Sleep(1000);
                Console.WriteLine("Flugzeug " + p.id + " ist auf dem Flughafen gelandet.");
                runway[usedRunway].Set();
            }
        }
    }
}
