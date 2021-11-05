using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutKassa
{
    class MultiKassa : Kassa
    {
        AutoResetEvent[] kassaArray;

        public MultiKassa(int anzKassa)
        {
            kassaArray = new AutoResetEvent[anzKassa];
            for(int i = 0; i < anzKassa; i++)
            {
                kassaArray[i] = new AutoResetEvent(true);
            }
        }


        public override void checkOut(Kunden k)
        {
            int kassaIndex = WaitHandle.WaitAny(kassaArray);

            Console.WriteLine("Der Kunde " + k.id + " legt die Ware aufs Band " + kassaIndex + ".");
            Thread.Sleep(1000);
            Console.WriteLine("Der Kunde " + k.id + " hat bezahlt.");
            kassaArray[kassaIndex].Set();

        }
    }
}
