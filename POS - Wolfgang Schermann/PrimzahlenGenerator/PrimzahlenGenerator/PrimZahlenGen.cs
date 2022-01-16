using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PrimzahlenGenerator
{
    public delegate int PrimHandler(int x);


    internal class PrimZahlenGen 
    {
        PrimHandler del;

        public static int Prim(int max)
        {
            List<int> prims = new List<int>();
            int i = 5;
            prims.Add(2);
            prims.Add(3);
            while (prims.Count < max)
            {
                int maxTeiler = (int)Math.Sqrt(i) + 1;
                int j = 0;
                while (prims.Count < max)
                {
                    int n = prims[j];
                    int rest = (i % n);
                    if (rest == 0)
                        break; //keine Primzahl
                    if (n >= maxTeiler)
                    {
                        prims.Add(i);
                        break;
                    }
                    ++j;
                }
                i += 2;
            }
            return prims[max - 1];
        }

        // Start der asynchronen Ausführung
        public IAsyncResult BeginPrim(int intVar,
                  AsyncCallback callback, object state)
        {
            del = new PrimHandler(Prim);
            // Aufruf der Methode Calculate, die in einem eigenen 
            // Thread ausgeführt wird
            return del.BeginInvoke(intVar, callback, state);
        }
        // Beenden der asynchronen Ausführung
        public int EndPrim(IAsyncResult ar)
        {
            return del.EndInvoke(ar);
        }
    }
}
