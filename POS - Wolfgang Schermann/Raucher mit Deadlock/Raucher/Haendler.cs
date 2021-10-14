using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace Raucher
{
    public class Haendler
    {
        private List<Zutaten> tisch;
        private TextBox zutat1;
        private TextBox zutat2;
        private ListBox listBox;

        public Haendler(TextBox eins, TextBox zwei, ListBox listBox)
        {
            tisch = new List<Zutaten>();
            tisch.Add(Zutaten.Nichts);
            tisch.Add(Zutaten.Nichts);
            zutat1 = eins;
            zutat2 = zwei;
            this.listBox = listBox;
        }

        public Thread NeuerThread()
        {
            ThreadStart worker = new ThreadStart(Work);
            return new Thread(worker);
        }

        private void NeueZutaten()
        {
            tisch.Clear();
            int zutatNr1 = MainWindow.random.Next(3);
            tisch.Add((Zutaten)zutatNr1);
            int zutatNr2 = zutatNr1;
            while (zutatNr1 == zutatNr2)
                zutatNr2 = MainWindow.random.Next(3);
            tisch.Add((Zutaten)zutatNr2);

            String text = "" + (Zutaten)zutatNr1 + " - " + (Zutaten)zutatNr2;
            try
            {
                listBox.Dispatcher.BeginInvoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      listBox.Items.Add(text);
                      listBox.UpdateLayout();
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            MainWindow.UpdateText(zutat1, ((Zutaten)zutatNr1).ToString(), Brushes.White);
            MainWindow.UpdateText(zutat2, ((Zutaten)zutatNr2).ToString(), Brushes.White);
        }

        public int AnzahlZutaten()
        {
            return tisch[0] == Zutaten.Nichts ? 0 : 1 + tisch[1] == Zutaten.Nichts ? 0 : 1;
        }

        public Zutaten ErsteZutat()
        {
            return tisch[0];
        }

        public Zutaten ZweiteZutat()
        {
            return tisch[1];
        }

        public void NimmZutat(int zutat)
        {
            if(zutat == 1)
            {
                if (tisch[0] == Zutaten.Nichts)
                    return;
                tisch[0] = Zutaten.Nichts;
                MainWindow.UpdateText(zutat1, "", Brushes.White);

            }
            else
            {
                tisch[1] = Zutaten.Nichts;
                MainWindow.UpdateText(zutat2, "", Brushes.White);

            }
        }

        public void NimmZutaten()
        {
            tisch.Clear();
            MainWindow.UpdateText(zutat1, "", Brushes.White);
            MainWindow.UpdateText(zutat2, "", Brushes.White);
        }

        public void Work()
        {
            while(true)
            {
                lock(this)
                {
                    while(AnzahlZutaten() > 0) Monitor.Wait(this);
                    NeueZutaten();
                    Monitor.PulseAll(this);
                }
            }
        }
    }
}
