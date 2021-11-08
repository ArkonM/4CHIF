using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA1_4CHIF
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Car.wash = new BigCarwash();
            Manager.wash = new BigCarwash();
            //Manager m = new Manager();
            //m.StartDyNtCycle();

            int autoAnz;
            int.TryParse(args[0], out autoAnz);

            for(int i=0; i < autoAnz; i++)
            {
                Car car = new Car();
                car.id = i;
                car.rand = r.Next(0, 100);
                car.StartThread();
            }

        }
    }
}
