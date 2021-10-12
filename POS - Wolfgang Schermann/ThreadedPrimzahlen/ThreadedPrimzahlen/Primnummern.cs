using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ThreadedPrimzahlen
{


    namespace Primzahlen
    {
        class Program
        {

            private static List<int> liste;
            private static int[] minimums;
            private static int min = 3;
            public int id = 0;

            private static int maxPrim = 0;
            private static int number = 0;
            private static int tests = 0;
            private static int last = 0;

            private int tests;
            int aktuell;
            int id;
            int startValue = 3;
            Thread t;

            public Program(int id, int startValue)
            {
                this.id = id;
                this.startValue = startValue;
            }

            static void Main(string[] args)
            {

                int.TryParse(args[0], out numberThreads);
                int.TryParse(args[1], out numberCounts);

                Stopwatch watch = new Stopwatch();
                
                Program[] progs = new Program[numberThreads];
                Thread[] threads = new Thread[numberThreads];

                for (int i = 0; i < numberThreads; i++)
                {
                    progs[i] = new Program(i + 2, startValue + 1);
                    threads[i] = progs[i].CreateThread();
                }

                watch.Start();

                for (int i = 0; i < numberThreads; i++)
                {
                    threads[i].Start();
                }

                for (int i = 0; i < numberThreads; i++)
                {
                    threads[i].Join();
                }



                watch.Stop();

                Console.WriteLine("Es wurden {0} Primzahlen gefunden", number);
                Console.WriteLine("Die höchste gefundene Primzahl ist {0}", maxPrim);
                Console.WriteLine("Die Laufzeit betrug {0:F0} Millisekungen", watch.ElapsedMilliseconds);
                Console.WriteLine("Es wurden {0} Vergleiche durchgeführt", tests);
                Console.ReadLine();
            }

         /*   private static void Prim(int max, out int maxPrim, out int number, out int tests)
            {
                List<int> prims = new List<int>();
                int i = 5;
                tests = 0;
                prims.Add(2);
                prims.Add(3);
                while (i < max)
                {
                    int maxTeiler = (int)Math.Sqrt(i) + 1;
                    int j = 0;
                    while (true)
                    {
                        int n = prims[j];
                        int rest = (aktuell % n);
                        ++tests;
                        if (rest == 0)
                            break; //keine Primzahl
                        if (n >= maxTeiler)
                        {
                            AddValue(aktuell);
                            break;
                        }
                        ++j;
                    }
                    aktuell += 2*Threads;
                }
                SumTests += tests;
            } */
            

            Thread CreateThread()
            {
                ThreadStart worker = new ThreadStart(Berechne);
                Thread t = new Thread(worker);
                return t;

            }

            //private void updateMin
            //private void AddValue(int value)



            private void Berechne()
            {
                int i = startValue;
                tests = 0;
                prims.Add(2);
                prims.Add(3);
                while (i < max)
                {
                    int maxTeiler = (int)Math.Sqrt(i) + 1;
                    int j = 0;
                    while (true)
                    {
                        int n = prims[j];
                        int rest = (aktuell % n);
                        ++tests;
                        if (rest == 0)
                            break; //keine Primzahl
                        if (n >= maxTeiler)
                        {
                            AddValue(aktuell);
                            break;
                        }
                        ++j;
                    }
                    aktuell += 2*Threads;
                }
                SumTests += tests;
//                number = prims.Count;
//                maxPrim = prims[number - 1];
            }



        }
    }
}
