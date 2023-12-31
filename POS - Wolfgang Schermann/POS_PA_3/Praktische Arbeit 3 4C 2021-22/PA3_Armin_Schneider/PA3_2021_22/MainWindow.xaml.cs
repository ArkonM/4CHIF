﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace PA3_2021_22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewCD_Click(object sender, RoutedEventArgs e)
        {
            CountdownLibrary.CountdownControl t = new CountdownLibrary.CountdownControl();
            t.Countdown = (int)CountdownValue.Value;
            Countdowns.Items.Add(t);
        }

        private void New(object sender, ExecutedRoutedEventArgs e)
        {
            Countdowns.Items.Clear();
        }

        private void Open(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void Save(object sender, ExecutedRoutedEventArgs e) {
            XmlWriter xmlWriter = XmlWriter.Create("test.xml");

            xmlWriter.WriteStartDocument();
            

            for (int i=0; Countdowns.Items.Count > i; i++)
            {
                xmlWriter.WriteString("30");

            }


            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

        }
    }
}
