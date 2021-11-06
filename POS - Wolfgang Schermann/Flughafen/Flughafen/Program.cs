using System;


namespace Flughafen
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 50;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out x);
            }
            MediumAirport a = new MediumAirport();
            Fluglotse l = new Fluglotse();
            l.StartDyNtCycle();

            for(int i=0; i < x; i++)
            {
                Plane p = new Plane(i + 1, a);
                p.ThreadStart();
            }
            

        }
    }
}
