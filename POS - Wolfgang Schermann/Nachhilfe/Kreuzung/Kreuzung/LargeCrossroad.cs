using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kreuzung
{
    class LargeCrossroad : Kreuzung
    {
        int ostwest = 0;
        int nordsued = 0;
        public override void cross(Car car)
        {
            lock (this)
            {
                if(car.richtung == Richtung.Nord || car.richtung == Richtung.Sued)
                {
                    while (ostwest > 0) Monitor.Wait(this);
                    nordsued++;
                }
                else
                {
                    while (nordsued > 0) Monitor.Wait(this);
                    ostwest++;
                }
            }

                Console.WriteLine("Das Auto " + car.Id + " fährt aus " + car.richtung + " in die Kreuzung");
                Thread.Sleep(1000);
                Console.WriteLine("Das Auto " + car.Id + " verlässt die Kreuzung");
            lock (this)
            {
                if (car.richtung == Richtung.Nord || car.richtung == Richtung.Sued)
                {

                    nordsued--;
                    if (nordsued == 0) Monitor.PulseAll(this);
                }
                else
                {
                    ostwest--;
                    if (ostwest == 0) Monitor.PulseAll(this);
                }
            }
        }
    }
}
