using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Flughafen
{
    class Fluglotse
    {

        public void StartDyNtCycle()
        {
            Thread t = new Thread(DayNight);
            t.IsBackground = true;
            t.Start();
        }

        public void DayNight()
        {
            while (true)
            {
                MediumAirport.DayNightChange.Set();
                Console.WriteLine("Tag Beginnt");
                Thread.Sleep(4000);

                
                MediumAirport.DayNightChange.Reset();
                Console.WriteLine("Nacht Beginnt");
                Thread.Sleep(2000);
            }
        }
    }
}
