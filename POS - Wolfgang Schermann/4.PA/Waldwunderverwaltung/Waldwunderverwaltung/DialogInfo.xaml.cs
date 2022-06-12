using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Waldwunderverwaltung
{
    /// <summary>
    /// Interaktionslogik für DialogInfo.xaml
    /// </summary>
    public partial class DialogInfo : Window
    {
        public DialogInfo()
        {
            InitializeComponent();
        }

        public void SetDetails(Waldwunder waldwunder, List<string> pictureNames)
        {
            WaldwundernameLabel.Content = waldwunder.Name;
            BeschreibungBox.Text = waldwunder.Description;
            ArtLabel.Content = waldwunder.Type;
            BundeslandLabel.Content = waldwunder.Province;
            LatitudeLabel.Content = waldwunder.Latitude;
            LongitudeLabel.Content = waldwunder.Longitude;

            foreach (var item in pictureNames)
            {
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("/images/" + item, UriKind.Relative);
                bitmap.EndInit();

                image.Height = 100;
                image.Width = 100;
                image.Margin = new Thickness(0, 0, 10, 0);
                image.Source = bitmap;

                ImgPanel.Children.Add(image);
            }

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
