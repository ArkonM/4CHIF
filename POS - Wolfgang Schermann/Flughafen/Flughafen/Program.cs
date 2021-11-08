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
            Fluglotse l = new Fluglotse();
            l.StartDyNtCycle();
            l.StartLandCycle();
            MediumAirport a = new MediumAirport(l);

            int y = 0;
            for(int i=0; i < x; i++)
            {
                if (y < 5)
                {
                    Plane p = new Plane(i + 1, a, true);
                    p.ThreadStart();
                    y++;
                } else
                {
                    Plane p = new Plane(i + 1, a, false);
                    p.ThreadStart();
                    if (y == 9) y = 0;
                    y++;
                }
            }
            

        }
    }
}
