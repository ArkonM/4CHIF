using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WPF_VideoPlayer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Video> playListCollection, playHistoryListCollection;
        private bool isPlaying = true;
        private int videoIdx;

        System.Windows.Threading.DispatcherTimer displayTimer;

        public MainWindow()
        {
            InitializeComponent();
            playListCollection = new ObservableCollection<Video>();
            playHistoryListCollection = new ObservableCollection<Video>();

            /*            playListCollection.CollectionChanged += PlayListCollection_CollectionChanged;
                        playHistoryListCollection.CollectionChanged += PlayHistoryListCollection_CollectionChanged;*/

            PlayListBox.ItemsSource = playListCollection;
            PlayHistoryListBox.ItemsSource = playHistoryListCollection;

            displayTimer = new System.Windows.Threading.DispatcherTimer();
            displayTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            displayTimer.Tick += OnDisplayTimerTick;
            displayTimer.Start();

        }

        private void OnDisplayTimerTick(object sender, EventArgs e)
        {
            ProgressSlider.Value = VideoElement.Position.TotalSeconds;
        }

        private void PlayHistoryListCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PlayHistoryListBox.Items.Clear();
            foreach (Video video in playHistoryListCollection)
            {
                PlayHistoryListBox.Items.Add(video.Name);
            }
        }

        private void PlayListCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            PlayListBox.Items.Clear();
            foreach (Video video in playListCollection)
            {
                PlayListBox.Items.Add(video.Name);
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying) VideoElement.Pause();
            else VideoElement.Play();

            isPlaying = !isPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VideoElement.Stop();
            isPlaying = false;
        }

        private void PreviousTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (videoIdx > 0)
            {
                videoIdx--;
                PlayVideo(playListCollection[videoIdx].Path);
                playHistoryListCollection.Add(playListCollection[videoIdx]);
            }
        }

        private void NextTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (videoIdx < playListCollection.Count - 1)
            {
                videoIdx++;
                PlayVideo(playListCollection[videoIdx].Path);
                playHistoryListCollection.Add(playListCollection[videoIdx]);
            }
        }

        private void MuteVolume_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VideoElement.IsMuted = !VideoElement.IsMuted;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoElement.Volume = VolumeSlider.Value;
        }

        private void VideoElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            PlayPauseButton.IsEnabled = true;

            ProgressSlider.Maximum = VideoElement.NaturalDuration.TimeSpan.TotalSeconds;
            ProgressSlider.Value = 0;
            VideoElement.Position = new TimeSpan(0, 0, 0, 0);

            VolumeSlider.Value = VideoElement.Volume;
        }

        private void VideoElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            //VideoElement.Stop();
            if (videoIdx < playListCollection.Count - 1)
            {
                videoIdx++;
                PlayVideo(playListCollection[videoIdx].Path);
                playHistoryListCollection.Add(playListCollection[videoIdx]);
            }
        }

        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\Users\Elvin\source\repos\Übung_3PA\WPF VideoPlayer\WPF VideoPlayer\videos";

            if (fileDialog.ShowDialog() == true)
            {
                Video video = new Video(fileDialog.SafeFileName, fileDialog.FileName);
                if (!Contains(video.Name))
                {
                    playListCollection.Add(video);
                    videoIdx = playListCollection.Count - 1;
                    playHistoryListCollection.Add(video);
                    PlayVideo(fileDialog.FileName);
                }
            }
        }

        private void PlayListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            videoIdx = PlayListBox.SelectedIndex;
            PlayVideo(playListCollection[PlayListBox.SelectedIndex].Path);
            playHistoryListCollection.Add(playListCollection[PlayListBox.SelectedIndex]);

        }

        private void ProgressSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, (int)ProgressSlider.Value);
            VideoElement.Position = ts;
        }

        private void PlayHistoryListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            PlayVideo(playHistoryListCollection[PlayHistoryListBox.SelectedIndex].Path);
            playHistoryListCollection.Add(playHistoryListCollection[PlayHistoryListBox.SelectedIndex]);
        
        }        

        private void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = @"C:\Users\Elvin\source\repos\Übung_3PA\WPF VideoPlayer\WPF VideoPlayer";
            dialog.ShowDialog();

            try
            {
                var mp4Files = Directory.EnumerateFiles(dialog.SelectedPath, "*.mp4");

                foreach (string currentFile in mp4Files)
                {
                    string fileName = currentFile.Substring(dialog.SelectedPath.Length + 1);
                    Video video = new Video(fileName, dialog.SelectedPath + @"\" + fileName);
                    if (!Contains(video.Name)) playListCollection.Add(video);
                }

                if (playListCollection.Count > 0)
                {
                    PlayVideo(playListCollection[0].Path);
                    playHistoryListCollection.Add(playListCollection[0]);
                    videoIdx = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool Contains(string name)
        {

            foreach (var item in playListCollection)
            {
                if (item.Name.Equals(name)) return true;
            }
            return false;
        }

        private void PlayListButton_Click(object sender, RoutedEventArgs e)
        {
            PlayListBox.Visibility = PlayListBox.IsVisible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void PlayHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            PlayHistoryListBox.Visibility = PlayHistoryListBox.IsVisible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void PlayVideo(string src)
        {
            VideoElement.Source = new Uri(src);
            ProgressSlider.Value = 0;
            VideoElement.Position = new TimeSpan(0, 0, 0, 0);
            VideoElement.Play();
        }
    }
}
