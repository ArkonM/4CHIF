using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class BigCarwash : Carwash    //Aufgabe 4
    {

        public AutoResetEvent[] street = new AutoResetEvent[3];
        
        public BigCarwash()
        {
            street[0] = new AutoResetEvent(true);
            street[1] = new AutoResetEvent(true);
            street[2] = new AutoResetEvent(true);
        }

        public override void wash(Car c)
        {
            int i = WaitHandle.WaitAny(street);

            Console.WriteLine("Auto " + c.id + " wird auf Straße " + (i+1) + " gewaschen.", 1);
            Thread.Sleep(1000);
            Console.WriteLine("Auto " + c.id + " ist jetzt sauber und " + (i + 1) + " ist wieder frei", 1);
            street[i].Set();
        }
    }

    /*class BigCarwash : Carwash    //Aufgabe 7
    {

        public AutoResetEvent[] street;
        public int wartend = 0;


        public BigCarwash()
        {
            street = new AutoResetEvent[1];
            street[0] = new AutoResetEvent(true);
        }


        public override void wash(Car c)
        {
            int i;
            wartend++;
            lock (this)
            {
                queueCheck(wartend);

                i = WaitHandle.WaitAny(street);
            }

            Console.WriteLine("Auto " + c.id + " wird auf Straße " + (i + 1) + " gewaschen.", 1);
            Thread.Sleep(1000);
            Console.WriteLine("Auto " + c.id + " ist jetzt sauber und " + (i + 1) + " ist wieder frei", 1);
            street[i].Set();
        }


        public void queueCheck(int i)
        {
            lock (this)
            {
                if (i > (street.Length * 3))
                {
                    street = new AutoResetEvent[street.Length + 1];
                    for (int y = 0; y < street.Length-2; y++)
                    {
                        street[i] = new AutoResetEvent(true);
                    }
                }
            }
        }
    }*/
}
