using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Waldwunderverwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WaldwunderDatabase waldwunderDatabase;

        public MainWindow()
        {
            InitializeComponent();

            waldwunderDatabase = new WaldwunderDatabase();
        }

        private void UpdateCollection()
        {
            waldwunderDatabase = new WaldwunderDatabase();

            WaldLB.Items.Clear();

            var query = from w in waldwunderDatabase.Waldwunder select w;

            foreach (var item in query)
            {
                WaldLB.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddWaldwunder_Click(object sender, RoutedEventArgs e)
        {
            Dialog dialog = new Dialog();

            if (dialog.ShowDialog() == true)
            {
                //  MessageBox.Show("Daten erfolgreich eingegeben!");
                Waldwunder waldwunder = new Waldwunder()
                {
                    Description = dialog.Description
                                                         ,
                    Latitude = dialog.Latitude
                                                         ,
                    Longitude = dialog.Longitude
                                                         ,
                    Name = dialog.ForestwonderName
                                                         ,
                    Province = dialog.Province
                                                         ,
                    Type = dialog.Type
                };
                waldwunderDatabase.Waldwunder.InsertOnSubmit(waldwunder);
                waldwunderDatabase.SubmitChanges();

                foreach (var item in dialog.safeFileNames)
                {
                    waldwunderDatabase = new WaldwunderDatabase();

                    var query = from w in waldwunderDatabase.Waldwunder
                                where w.Name.Equals(waldwunder.Name)
                                   && w.Description.Equals(waldwunder.Description)
                                select w;

                    Waldwunder waldw = query.ToList().First();

                    Bild bild = new Bild() { Name = item, Waldwunder = waldw };
                    waldwunderDatabase.Bilder.InsertOnSubmit(bild);
                    waldwunderDatabase.SubmitChanges();
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            AnzeigenButton.IsEnabled = true;
            DialogStichwort dialog = new DialogStichwort();

            if (dialog.ShowDialog() == true)
            {
                var query = from w in waldwunderDatabase.Waldwunder
                            where w.Name.Contains(dialog.StichwortName)
                               || w.Description.Contains(dialog.StichwortName)
                            select w;

                WaldLB.Items.Clear();

                foreach (var item in query)
                {
                    WaldLB.Items.Add(item);
                }

                DrawOnMap();
            }
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AnzeigenButton.IsEnabled = true;
            DialogArt dialog = new DialogArt();

            if (dialog.ShowDialog() == true)
            {

                var query = from w in waldwunderDatabase.Waldwunder
                            where w.Type.Equals(dialog.ArtName)
                            select w;

                WaldLB.Items.Clear();
                foreach (var item in query)
                {
                    WaldLB.Items.Add(item);
                }
                DrawOnMap();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AnzeigenButton.IsEnabled = true;
            DialogOrt dialog = new DialogOrt();

            if (dialog.ShowDialog() == true)
            {
                var query = from w in waldwunderDatabase.Waldwunder
                            where dialog.Longitude <= w.Longitude + 0.5 && dialog.Longitude >= w.Longitude - 0.5
                             || dialog.Latitude <= w.Latitude + 0.5 && dialog.Latitude >= w.Latitude - 0.5
                            select w;

                WaldLB.Items.Clear();
                foreach (var item in query)
                {
                    WaldLB.Items.Add(item);
                }
                DrawOnMap();
            }
        }

        private void DrawOnMap()
        {
            double minLAT = 46.308597, maxLAT = 49.063175, minLONG = 9.362383, maxLONG = 17.231941;

            CanvasMap.Children.Clear();
            foreach (Waldwunder item in WaldLB.Items)
            {

                double LatInPixel = CanvasMap.ActualHeight / (maxLAT - minLAT);
                double LongInPixel = CanvasMap.ActualWidth / (maxLONG - minLONG);

                int posX = (int)((item.Longitude - minLONG) * LongInPixel);
                int posY = (int)((maxLAT - item.Latitude) * LatInPixel);

                Ellipse el = new Ellipse();

                el.DataContext = item;

                el.Stroke = Brushes.Red;
                el.Fill = Brushes.Blue;
                el.Width = 15;
                el.Height = 15;

                el.MouseLeftButtonDown += El_MouseLeftButtonDown;

                Canvas.SetLeft(el, posX);
                Canvas.SetTop(el, posY);

                CanvasMap.Children.Add(el);
            }
        }

        private void El_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WaldLB.SelectedItem = ((Waldwunder)((Ellipse)sender).DataContext);
        }

        private void AnzeigenButton_Click(object sender, RoutedEventArgs e)
        {
            if (WaldLB.SelectedItem == null)
            {
                MessageBox.Show("Bitte Waldwunder auswählen!");
                return;
            }

            var query = from b in waldwunderDatabase.Bilder
                        where b.Waldwunder.Equals((Waldwunder)WaldLB.SelectedItem)
                        select b;

            List<string> pictureNames = new List<string>();
            foreach (var item in query)
            {
                pictureNames.Add(item.Name);
            }

            DialogInfo dialog = new DialogInfo();
            dialog.SetDetails((Waldwunder)WaldLB.SelectedItem, pictureNames);
            dialog.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawOnMap();
        }

    }
}
