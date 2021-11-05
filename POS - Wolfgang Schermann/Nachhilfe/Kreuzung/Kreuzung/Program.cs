using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreuzung
{
    class Program
    {
        static void Main(string[] args)
        {
            int anzahl = 100;
            if(args.Length > 0)
            {
                int.TryParse(args[0], out anzahl);
            }
            SensorAmpel amp = new SensorAmpel();    //SensorAmpel, Ampel, LargeCrossroad
            amp.Start();
            Car.kreuzung = amp;

            for(int i = 0; i < anzahl; i++)
            {
                Car c = new Car(i + 1);
                c.Start();
            }

        }
    }
}
