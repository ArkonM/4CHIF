using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedCounter
{
    class Program
    {

        public static int numberCounts = 1000;
        public int id = 0;
        public static int counter = 1;
        public static object key = new object();

        public Program(int id)
        {
            this.id = id;
        }

        Thread CreateThread()
        {
            ThreadStart worker = new ThreadStart(CounterMethod);
            Thread t = new Thread(worker);
            return t;

        }

        static void Main(string[] args)
        {

            int numberThreads = 0;

            int.TryParse(args[0], out numberThreads);
            int.TryParse(args[1], out numberCounts);

            Program[] progs = new Program[numberThreads];
            Thread[] threads = new Thread[numberThreads];

            for (int i = 0; i < numberThreads; i++)
            {
                progs[i] = new Program(i + 2);
                threads[i] = progs[i].CreateThread();
            }

            Stopwatch watch = new Stopwatch();
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
            Console.WriteLine("Time: " + watch.ElapsedMilliseconds + " ms");
            Console.ReadLine();
        }

        void CounterMethod()
        {
            for (int i = 0; i < numberCounts; i++)
            {
                lock (key)
                {
                    if (counter % id == 0)
                    {
                        Console.WriteLine("ID: {0,3} Counter: {1,8} Modulo: {2}", id, counter, counter % id);
                    }
                    counter++;
                }
            }

            /* umschreiben: statt lock -> steht eig das (Monitor...); normalerweise lock verwenden
             * for (int i = 0; i < numberCounts; i++)
            {
                    Monitor.Enter(key);
                    try {
                        if (counter % id == 0)
                        {
                            Console.WriteLine("ID: {0,3} Counter: {1,8} Modulo: {2}", id, counter, counter % id);
                        }
                        counter++;
                     } catch(ThreadAbortException e) {
                     
                     }
                    Monitor.Exit(key);
                }
            }*/
        }
    }

}
