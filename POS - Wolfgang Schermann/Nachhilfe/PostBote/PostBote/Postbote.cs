using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;  //Threading include

namespace PostBote
{
    class Postbote //"Program" zu "POstbote" umbenannt
    {
        private Random random = new Random();  //Wichtig für Randomzahl
        private static object briefkasten = new object();
        public static object Briefkasten { 
            get 
            {
                return briefkasten;   
            }
        }

        public static List<string> post { get; set; } = new List<string>();    //Eigenschaft Post

        public void shipment()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(random.Next(10000, 15000));  //Random Zahl zwischen 10 und 15 Sekunden
                                                          //Thread.Sleep(TimeSpan.FromSeconds(random.Next(10000, 15000)));    //Timespan um Sekunden automatisch in Millisekunden umzuwandeln

                lock (briefkasten) //Lock ersetzt Monitor.Enter() und Monitor.Exit() -> deswegen auch Monitor.Pulse / lock() hat viele Vorteile
                {
                    Postbote.post.Add("Robert");
                    Postbote.post.Add("Robert");
                    Postbote.post.Add("Peter");

                    Monitor.PulseAll(briefkasten);    //Alle Threads die auf lock(briefkasten) warten, werden geweckt

                }
            }
        }

        static void Main(string[] args)
        {
            Postbote pb = new Postbote();


            ThreadStart worker = new ThreadStart(pb.shipment);
            Thread t = new Thread(worker);
            t.Start();

            Bewohner robert = new Bewohner("Robert");
            Bewohner peter = new Bewohner("Peter");
            Bewohner franz  = new Bewohner("Franz");

            robert.StartThread();
            peter.StartThread();
            franz.StartThread();
        }
    }
}
