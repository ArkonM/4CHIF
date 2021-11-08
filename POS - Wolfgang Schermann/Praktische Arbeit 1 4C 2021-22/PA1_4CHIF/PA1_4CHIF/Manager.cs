using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class Manager
    {
        public static Carwash wash { get; set; }



        public void StartDyNtCycle()
        {
            Thread t = new Thread(manage);
            t.IsBackground = true;
            t.Start();
        }

        public void manage()
        {
            while (true)
            {
                Carwash.DayNightChange.Set();
                Console.WriteLine("Tag Beginnt");
                Thread.Sleep(5000);


                Carwash.DayNightChange.Reset();
                Console.WriteLine("Nacht Beginnt");
                Thread.Sleep(2000);
            }
        }

    }
}
