using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace WPFAlarmClock
{
    /// <summary>
    ///
    ///     <MyNamespace:AlarmClockControl/>
    ///
    /// </summary>
    public class AlarmClockControl : Control
    {
        static AlarmClockControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlarmClockControl), new FrameworkPropertyMetadata(typeof(AlarmClockControl)));
        }


        public static readonly RoutedEvent AlarmEvent =
           EventManager.RegisterRoutedEvent("Alarm",
             RoutingStrategy.Bubble, typeof(RoutedEventHandler),
             typeof(AlarmClockControl));

        public event RoutedEventHandler Alarm
        {
            add { base.AddHandler(AlarmEvent, value); }
            remove { base.RemoveHandler(AlarmEvent, value); }
        }

        protected void FireAlarm()
        {
            base.RaiseEvent(new RoutedEventArgs(AlarmEvent));
        }

        protected void RingAlarm()
        {
            SoundPlayer sp = new SoundPlayer(@"c:\windows\media\tada.wav");
            sp.Play();
            FireAlarm();
        }
        public void OnDisplayTimerTick(object o, EventArgs args)
        {

            if (alarmSet)
            {
                time--;
                if (time == 0)
                {
                    RingAlarm();
                    alarmSet = false;
                    displayTimer.Stop();
                }
                int minutes = time / 60;
                int sek = time - minutes * 60;

                CurrentTimeBlock.Text = minutes + " min " + sek + " sek";
            }
        }

        void OnShowSetAlarmDlg(object sender, RoutedEventArgs ea)
        {

            dateTimeDlg = new DateTimeDlg();

            if (dateTimeDlg.ShowDialog() == true)
            {
                CurrentTimeBlock.Text = dateTimeDlg.MinTime + " min " + dateTimeDlg.SekTime + " sek";
                time = dateTimeDlg.MinTime * 60 + dateTimeDlg.SekTime;
            }

        }

        System.Windows.Threading.DispatcherTimer displayTimer;

        private TextBlock CurrentTimeBlock;
        private bool alarmSet = false;
        private int time;
        private DateTimeDlg dateTimeDlg;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CurrentTimeBlock = (TextBlock)this.Template.FindName("PART_CURRENTTIME", this);

            Button bSetAlarmDlg =
                (Button)this.Template.FindName("PART_SETALARMBUTTON", this);

            Button bStartCtd =
                (Button)this.Template.FindName("PART_STARTCTD", this);

            Button bPauseCtd =
                (Button)this.Template.FindName("PART_PAUSECTD", this);

            Button bSetBackCtd =
                (Button)this.Template.FindName("PART_SETBACKCTD", this);

            bSetAlarmDlg.Click += OnShowSetAlarmDlg;

            bStartCtd.Click += BStartCtd_Click;

            bPauseCtd.Click += BPauseCtd_Click;

            bSetBackCtd.Click += BSetBackCtd_Click;

            displayTimer = new System.Windows.Threading.DispatcherTimer();
            displayTimer.Interval = new TimeSpan(0, 0, 0, 1);
            displayTimer.Tick += OnDisplayTimerTick;

        }

        private void BSetBackCtd_Click(object sender, RoutedEventArgs e)
        {
            CurrentTimeBlock.Text = dateTimeDlg.MinTime + " min " + dateTimeDlg.SekTime + " sek";
            time = dateTimeDlg.MinTime * 60 + dateTimeDlg.SekTime;
            alarmSet = true;
        }

        private void BPauseCtd_Click(object sender, RoutedEventArgs e)
        {
            displayTimer.Stop();
            alarmSet = false;
        }

        private void BStartCtd_Click(object sender, RoutedEventArgs e)
        {
            displayTimer.Start();
            alarmSet = true;
        }
    }
}