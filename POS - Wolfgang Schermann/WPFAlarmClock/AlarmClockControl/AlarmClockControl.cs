﻿using System;
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
using WPFAlarmClock;

namespace WPFAlarmClock
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AlarmClockControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AlarmClockControl;assembly=AlarmClockControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class AlarmClockControl : Control
    {
        static AlarmClockControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlarmClockControl), new FrameworkPropertyMetadata(typeof(AlarmClockControl)));
        }

        public static readonly DependencyProperty
            AlarmTimeProperty = DependencyProperty.Register(
                    "AlarmTime",
                  typeof(DateTime),
                     typeof(AlarmClockControl),
            new FrameworkPropertyMetadata(
               DateTime.Now, null));

        public DateTime AlarmTime
        {
            get { return (DateTime)base.GetValue(AlarmTimeProperty); }
            set { base.SetValue(AlarmTimeProperty, value); }
        }

        public static readonly DependencyProperty
            AlarmSetProperty = DependencyProperty.Register(
                    "AlarmSet",
                  typeof(bool),
                     typeof(AlarmClockControl),
            new FrameworkPropertyMetadata(
               false,
               FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool AlarmSet
        {
            get { return (bool)base.GetValue(AlarmSetProperty); }
            set { base.SetValue(AlarmSetProperty, value); }
        }

        public static readonly DependencyProperty
            CurrentTimeProperty = DependencyProperty.Register(
                    "CurrentTime",
                  typeof(DateTime),
                     typeof(AlarmClockControl),
            new FrameworkPropertyMetadata(
               DateTime.Now, null));

        public DateTime CurrentTime
        {
            get { return (DateTime)base.GetValue(CurrentTimeProperty); }
            set { base.SetValue(CurrentTimeProperty, value); }
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
            this.CurrentTime = DateTime.Now;

            if (this.AlarmSet == true)
            {
                if (DateTime.Now.Ticks > this.AlarmTime.Ticks)
                {
                    RingAlarm();
                    this.AlarmSet = false;
                }
            }
        }

        void OnShowSetAlarmDlg(object sender, RoutedEventArgs ea)
        {
            DateTimeDlg dateTimeDlg = new DateTimeDlg();

            dateTimeDlg.AlarmTime = this.AlarmTime;
            if (dateTimeDlg.ShowDialog() == true)
            {
                this.AlarmTime = dateTimeDlg.AlarmTime;
            }

        }

        System.Windows.Threading.DispatcherTimer displayTimer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button bSetAlarmDlg =
                (Button)this.Template.FindName("PART_SETALARMBUTTON", this);

            bSetAlarmDlg.Click += OnShowSetAlarmDlg;

            displayTimer = new System.Windows.Threading.DispatcherTimer();
            displayTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            displayTimer.Tick += OnDisplayTimerTick;

            displayTimer.Start();

            // Set up a databinding between the checkbox in the template and the flag...
            CheckBox cbAlarmSet = (CheckBox)this.Template.FindName("PART_CHECKBOXALARMSET", this);
            Binding bindingAlarmSet = new Binding();
            bindingAlarmSet.Source = this;
            bindingAlarmSet.Path = new PropertyPath("AlarmSet");
            cbAlarmSet.SetBinding(CheckBox.IsCheckedProperty, bindingAlarmSet);

            TextBlock tbAlarmSetButtonTextPane =
                (TextBlock)this.Template.FindName("PART_SETALARMBUTTONTEXTPANE", this);
            Binding bindingSetAlarmButtonTextPane = new Binding();
            bindingSetAlarmButtonTextPane.Source = this;
            bindingSetAlarmButtonTextPane.Path = new PropertyPath("AlarmTime");
            tbAlarmSetButtonTextPane.SetBinding(TextBlock.TextProperty, bindingSetAlarmButtonTextPane);

            TextBlock tbCurrentTime =
                (TextBlock)this.Template.FindName("PART_CURRENTDATETIME", this);
            Binding bindingCurrentTime = new Binding();
            bindingCurrentTime.Source = this;
            bindingCurrentTime.Path = new PropertyPath("CurrentTime");
            tbCurrentTime.SetBinding(TextBlock.TextProperty, bindingCurrentTime);
        }
    }
}
