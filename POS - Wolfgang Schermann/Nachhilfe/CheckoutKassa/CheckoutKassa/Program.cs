using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKassa
{
    class Program
    {
        static void Main(string[] args)
        {
            int anzKunden = 10;
            if(args.Length > 0)
            {
                int.TryParse(args[0], out anzKunden);
            }
            MultiKassa kassa = new MultiKassa(3);
            for(int i = 0; i < anzKunden; i++)
            {
                Kunden k = new Kunden(i + 1, kassa);
                k.ThreadStarter();
            }
        }
    }
}
