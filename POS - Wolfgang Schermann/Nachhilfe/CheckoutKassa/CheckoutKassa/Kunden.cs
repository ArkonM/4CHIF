using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutKassa
{
    class Kunden
    {

        public int id;
        Random r;
        Kassa kassa;

        public Kunden(int id, Kassa kassa)
        {
            this.id = id;
            this.kassa = kassa;
            r = new Random();
        }

        public void ThreadStarter()
        {
            Thread t = new Thread(shop);
            t.Start();
        }

        void shop()
        {
            int zeit = r.Next(1000, 20000);
            Thread.Sleep(zeit);
            kassa.checkOut(this);
        }
    }
}
