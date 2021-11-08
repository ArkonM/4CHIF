using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class Car
    {
        Random r = new Random();
        public int id { get; set; }
        public int rand { get; set; }
        public static Carwash wash { get; set; }


        public void StartThread()
        {
            Thread t = new Thread(drive);
            t.Start();
        }

        public void drive()
        {
            Thread.Sleep(r.Next(1000, 10000));
            wash.wash(this);
        }
    }
}
