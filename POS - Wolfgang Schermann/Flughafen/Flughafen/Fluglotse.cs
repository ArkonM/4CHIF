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



        public void StartLandCycle()
        {
            Thread t = new Thread(StartLand);
            t.IsBackground = true;
            t.Start();
        }

        public void StartLand()
        {
            while (true)
            {
                if(MediumAirport.Anzahl == 0) {
                    MediumAirport.Ld.Reset();
                    MediumAirport.St.Set();
                }
                else if (MediumAirport.Anzahl == 4)
                {
                    MediumAirport.St.Reset();
                    MediumAirport.Ld.Set();
                }
                if(MediumAirport.Anzahl == 9) { MediumAirport.Anzahl = 0; }
            }
        }
    }
}
