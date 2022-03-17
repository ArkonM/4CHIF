using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String URL;
        Boolean playing = false;
        Boolean firstOne = true;

        List<String> playlist = new List<String>();
        int playingVideo = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (playingVideo > 0)
            {
                playingVideo--;
                video.Source = new Uri(playlist[playingVideo]);
                video.Play();
                StartStop.Content = "Pause";
            } else
            {
                video.Pause();
                playing = false;
                StartStop.Content = "Continue";
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (playingVideo < playlist.Count()-1)
            {
                playingVideo++;
                video.Source = new Uri(playlist[playingVideo]);
                video.Play();
                StartStop.Content = "Pause";
            } else
            {
                video.Pause();
                playing = false;
                StartStop.Content = "Continue";
            }
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (!playing)
            {
                video.Play();
                playing = true;
                StartStop.Content = "Pause";
            } else
            {
                video.Pause();
                playing = false;
                StartStop.Content = "Continue";
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            URL = dialog.InitialDirectory + dialog.FileName;
            if (firstOne)
            {
                video.Source = new Uri(URL);
                firstOne = false;
                video.Play();
                Thread.Sleep(10);
                video.Pause();
            }
            playlist.Add(URL);
        }

        private void ProgessBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video.Position = TimeSpan.FromSeconds(ProgessSlider.Value);
        }

        private void ProgressBar_Value()
        {
            /*
            TimeSpan _position;
            _position = video.NaturalDuration.TimeSpan;
            ProgessSlider.Minimum = 0;
            ProgessSlider.Maximum = _position.TotalSeconds;
            */
        }
    }
}
