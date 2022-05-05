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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solitaire
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Grid_Creation(9, "Test");

        }

        Ellipse moving = null;

        List<Border> borderList = new List<Border>();

        private Point clickPosition;

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            moving = (Ellipse)sender;
            clickPosition = e.GetPosition(this);
            moving.IsHitTestVisible = false;
            DragDrop.DoDragDrop(moving, moving, DragDropEffects.All);
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (moving != null)
            {
                int col = Grid.GetColumn((UIElement)sender);
                int row = Grid.GetRow((UIElement)sender);
                //check if move is allowed
                Grid.SetColumn(moving, col);
                Grid.SetRow(moving, row);

                moving.RenderTransform = null;
                moving.IsHitTestVisible = true;
                moving = null;
            }
        }

        private void Grid_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (moving != null)
            {
                Point currentPosition = e.GetPosition(this);

                var transform = moving.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    moving.RenderTransform = transform;
                }

                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
            }
        }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (moving != null)
            {
                moving.RenderTransform = null;
                moving.IsHitTestVisible = true;
                moving = null;
            }
        }

        private void Grid_Creation(int size, String method)
        {
            Spiel.ShowGridLines = true;
            for (int i = 0; i < size; i++)
            {
                Spiel.RowDefinitions.Add(new RowDefinition());
                Spiel.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(2, 2, 2, 2);

                    border.Background = Brushes.Gray;

                    if (((x >= 3 && x <= 5) || (y >= 3 && y <= 5)))
                    {
                        border.Background = Brushes.White;
                        border.AllowDrop = true;
                        border.Drop += Border_Drop;
                    }
                    Grid.SetRow(border, x);
                    Grid.SetColumn(border, y);

                    Spiel.Children.Add(border);
                    borderList.Add(border);
                }
            }

            foreach(var border in borderList)
            {
                if(border.AllowDrop == true)
                {
                    int x = Grid.GetColumn(border);
                    int y = Grid.GetRow(border);


                }
            }
        }
    }
}
