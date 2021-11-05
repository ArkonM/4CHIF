using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kreuzung
{
    class Kreuzung
    {
        public virtual void cross(Car car)
        {
            lock (this)
            {
                Console.WriteLine("Das Auto " + car.Id + " fährt aus " + car.richtung + " in die Kreuzung");
                Thread.Sleep(1000);
                Console.WriteLine("Das Auto " + car.Id + " verlässt die Kreuzung");
            }
        }
    }
}
