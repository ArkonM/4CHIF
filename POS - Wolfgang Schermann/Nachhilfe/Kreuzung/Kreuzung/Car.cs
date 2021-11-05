using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kreuzung
{
    public enum Richtung { Nord, Sued, Ost, West}

    class Car
    {
        public static Random r = new Random();
        public static Kreuzung kreuzung { get; set; }
        public int Id { get;}
        public Richtung richtung { get; }
        public Car(int id)
        {
            this.Id = id;
            richtung = (Richtung)r.Next(4);
        }

        public void Start()
        {
            Thread t = new Thread(drive);
            t.Start();
        }

        void drive()
        {
            Thread.Sleep(r.Next(1000, 10001));
            kreuzung.cross(this);
        }
    }
}
