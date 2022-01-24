using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA2_4CHIF
{
    public class SinusSchwingung
    {
        private Schwingung obj;

        public double CalculateValue(double t, int A, int F)
        {
            double res = Math.Sin(2 * Math.PI * t * F) * A;
            AsyncCallback Call = new AsyncCallback(paint);
            //obj.Start(res, Call, null);
            return res;
        }

        public void paint(IAsyncResult res)
        {
            //List<String> list = obj.EndPaint(res);

        }

        void Schwingung(Label l, double dauer, int A, int F)
        {
            double t = 0.0;

            while (t < dauer)
            {
                l.Content = CalculateValue(t, A, F);
                t += 0.01;
                Thread.Sleep(10);
            }
        }
    }
}
