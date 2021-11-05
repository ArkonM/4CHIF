using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutKassa
{
    class Kassa
    {

        public virtual void checkOut(Kunden k)
        {
            lock (this)
            {

                Console.WriteLine("Der Kunde " + k.id + " legt die Ware aufs Band.");
                Thread.Sleep(1000);
                Console.WriteLine("Der Kunde " + k.id + " hat bezahlt.");
            }
        }
    }
}
