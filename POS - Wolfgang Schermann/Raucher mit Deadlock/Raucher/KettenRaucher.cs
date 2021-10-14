using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace Raucher
{
    class KettenRaucher
    {
        private Zutaten endloszutat;
        private TextBox status;
        private String name;
        private List<Zutaten> fehlend;

        public KettenRaucher(Zutaten endloszutat, TextBox status, String name)
        {
            this.endloszutat = endloszutat;
            this.status = status;
            this.name = name;
            fehlend = new List<Zutaten>();
        }

        public Thread NeuerThread()
        {
            ThreadStart worker = new ThreadStart(Smoke);
            return new Thread(worker);
        }

        public void Smoke()
        {
            Haendler haendler = MainWindow.haendler;
            while (true)
            {
                
                MainWindow.UpdateText(status, "Hat " + endloszutat, Brushes.Red);
                for (int i = 0; i < 3; i++)
                {
                    Zutaten z = (Zutaten)i;
                    if (z != endloszutat)
                        fehlend.Add(z);
                }

                lock (haendler)
                {
                    while (fehlend.Count > 0)
                    {
                        /* //fixe Reihenfolge der Zutaten
                        if (fehlend[0] == haendler.ErsteZutat())
                        {
                            fehlend.Remove(haendler.ErsteZutat());
                            haendler.NimmZutat(1);
                        }
                        if (fehlend.Count > 0 && fehlend[0] == haendler.ZweiteZutat())
                            {
                            fehlend.Remove(haendler.ZweiteZutat());
                            haendler.NimmZutat(2);
                        }
                        */
                        
                        if (fehlend.Contains(haendler.ErsteZutat()))
                        {
                            fehlend.Remove(haendler.ErsteZutat());
                            haendler.NimmZutat(1);
                        }
                        if (fehlend.Contains(haendler.ZweiteZutat()))
                        {
                            fehlend.Remove(haendler.ZweiteZutat());
                            haendler.NimmZutat(2);
                        }

                        if (fehlend.Count == 1)
                        {
                            String text = "Wartet auf " + fehlend[0];
                            MainWindow.UpdateText(status, text, Brushes.Red);
                        }
                        if (haendler.AnzahlZutaten() == 0)
                            Monitor.PulseAll(haendler);
                        if (fehlend.Count > 0)
                            Monitor.Wait(haendler);
                    }
                }
                MainWindow.UpdateText(status, "Raucht", Brushes.Green);
                int smoketime = MainWindow.random.Next(1000, 5000);
                Thread.Sleep(smoketime);
            }
        }
    }
}
