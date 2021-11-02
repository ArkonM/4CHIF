using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PostBote
{
    class Bewohner
    {
        public string name;
        public Bewohner(string name)
        {
            this.name = name;
        }

        public void warten()
        {
            while (true)
            {
                lock (Postbote.Briefkasten)
                {
                    if (Postbote.post.Contains(name))    //Test ob ICH post bekommen habe
                    {
                        Console.WriteLine(name + " hat Post bekommen.");
                        Postbote.post.Remove(name);     //Post wird gelöscht
                    }
                    else
                    {
                        Monitor.Wait(Postbote.Briefkasten);   //Bei Wait wird das Lock wieder freigegeben
                    }
                }
            }
        }

        public void StartThread()
        {
            ThreadStart ts = new ThreadStart(this.warten);
            Thread t = new Thread(ts);
            t.IsBackground = true;
            t.Start();
        }
    }
}
