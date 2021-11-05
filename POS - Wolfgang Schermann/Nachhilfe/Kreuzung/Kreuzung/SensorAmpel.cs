using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kreuzung
{
    class SensorAmpel : Kreuzung
    {
        public void Start()
        {
            Thread t = new Thread(lights);
            t.IsBackground = true;
            t.Start();
        }

        public override void cross(Car car)
        {

            if (car.richtung == Richtung.Nord || car.richtung == Richtung.Sued)
            {
                _nordSued.WaitOne();
            }
            else
            {
                WaitHandle.SignalAndWait(_sensor, _ostWest);
            }

            Console.WriteLine("Das Auto " + car.Id + " fährt aus " + car.richtung + " in die Kreuzung");
            Thread.Sleep(1000);
            Console.WriteLine("Das Auto " + car.Id + " verlässt die Kreuzung");
        }

        ManualResetEvent _nordSued = new ManualResetEvent(false);
        ManualResetEvent _ostWest = new ManualResetEvent(false);

        ManualResetEvent _sensor = new ManualResetEvent(false);

        void lights()
        {
            while (true)
            {
                Console.WriteLine("----------- Der Nord-Süd Verkehr darf fahren.");
                _nordSued.Set();
                Thread.Sleep(3000);
                _sensor.WaitOne();
                Console.WriteLine("----------------- Der Ost-West Sensor wurde Ausgelöst.");
                _nordSued.Reset();
                Console.WriteLine("-------- Der Nord-Süd Verkehr muss stehen bleiben.");
                Thread.Sleep(2000);

                Console.WriteLine("----------- Der Ost-West Verkehr darf fahren.");
                _ostWest.Set();
                Thread.Sleep(3000);
                _ostWest.Reset();
                Console.WriteLine("-------- Der Ost-West Verkehr muss stehen bleiben.");
                _sensor.Reset();
                Thread.Sleep(2000);
            }
        }
    }
}
