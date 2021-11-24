using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class VipCarwash : Carwash
    {
        public AutoResetEvent normal = new AutoResetEvent(true);
        public AutoResetEvent VIP = new AutoResetEvent(true);
        int vipWaiting = 0;

        public override void wash(Car c)
        {
            if (c.rand >= 95)
            {
                lock (this)
                {
                    vipWaiting++;
                }
                    normal.Reset();
                    Console.WriteLine("Auto " + c.id + " (VIP) wartet.", 1);
                    VIP.WaitOne();
            } else
            {
                Console.WriteLine("Auto " + c.id + " wartet.", 1);
                normal.WaitOne();
                VIP.Reset();
            }

            Console.WriteLine("Auto " + c.id + " wird gewaschen.", 1);
            Thread.Sleep(1000);
            Console.WriteLine("Auto " + c.id + " ist jetzt sauber.", 1);

            if (c.rand >= 95 && vipWaiting>1)
            {
                vipWaiting--;
                VIP.Set();
            } else if (c.rand >= 95 && vipWaiting == 1)
            {
                vipWaiting--;
                normal.Set();
            } else if (c.rand < 95 && vipWaiting > 0)
            {
                VIP.Set();
            } else
            {
                normal.Set();
            }

        }
    }
}
