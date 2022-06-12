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

        public List<EllipseInfo> ellipseList = new List<EllipseInfo>();

        public MainWindow()
        {
            InitializeComponent();


            startGame();
        }

        private void startGame()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (((1 < i && i < 5) || (((-1 < i && i < 2) || (4 < i && i < 7)) && 1 < j && j < 5)) && !(i == 3 && j == 3))
                    {
                        Ellipse ellipse = new Ellipse();
                        ellipse.Fill = Brushes.Red;
                        ellipse.Stroke = Brushes.Red;
                        ellipse.StrokeThickness = 2;
                        ellipse.PreviewMouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                        Grid.SetColumn(ellipse, i);
                        Grid.SetRow(ellipse, j);
                        grid.Children.Add(ellipse);

                        ellipseList.Add(new EllipseInfo(ellipse, i, j));
                    }
                }
            }
        }

        Ellipse moving = null;
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

                EllipseInfo ellipseToRemove = findEllipse(((col + Grid.GetColumn(moving)) / 2), ((row + Grid.GetRow(moving)) / 2));

                if(((col == Grid.GetColumn(moving) && difference(row, Grid.GetRow(moving)) == 2) || 
                    (row == Grid.GetRow(moving) && difference(col, Grid.GetColumn(moving)) == 2)) && 
                    ellipseToRemove != null)
                {

                    removeEllipse(ellipseToRemove);
                    updateEllipse(findEllipse(Grid.GetColumn(moving), Grid.GetRow(moving)) ,moving, col, row);

                    Grid.SetColumn(moving, col);
                    Grid.SetRow(moving, row);
                }

                moving.RenderTransform = null;
                moving.IsHitTestVisible = true;
                moving = null;

                if (ellipseList.Count == 1)
                {
                    MessageBox.Show("You have Won!");
                    ellipseList.ForEach(ellipse => grid.Children.Remove(ellipse.ellipse));
                    ellipseList.Clear();
                    startGame();
                }

                if (lostGame())
                {
                    MessageBox.Show("You have Lost!");
                    ellipseList.ForEach(ellipse => grid.Children.Remove(ellipse.ellipse));
                    ellipseList.Clear();
                    startGame();
                }

            }
        }


        private void updateEllipse(EllipseInfo oldEllipse ,Ellipse ellipse, int newCol, int newRow)
        {
            EllipseInfo newEllipse = new EllipseInfo(ellipse, newCol, newRow);

            ellipseList.Remove(oldEllipse);
            ellipseList.Add(newEllipse);
        }


        private void removeEllipse(EllipseInfo ellipse)
        {
            grid.Children.Remove(ellipse.ellipse);
            ellipseList.Remove(ellipse);
        }


        private EllipseInfo findEllipse(int col, int row)
        {

            IEnumerable <EllipseInfo> result = ellipseList.Where(ellipseInfo => ellipseInfo.x == col && ellipseInfo.y == row);

            foreach(EllipseInfo ellipseInfo in result)
            {
                return ellipseInfo;
            }

            return null;
        }


        private int difference(int numberOne, int numberTwo)
        {

            if(numberOne < numberTwo)
            {
                return numberTwo - numberOne;
            }

            return numberOne - numberTwo;
        }


        private Boolean lostGame()
        {
            foreach (EllipseInfo ellipse in ellipseList)
            {
                if ((findEllipse(ellipse.x, ellipse.y + 1) != null &&
                     findEllipse(ellipse.x, ellipse.y + 2) == null &&
                     validField(ellipse.x, ellipse.y + 2)) ||

                    (findEllipse(ellipse.x, ellipse.y - 1) != null &&
                     findEllipse(ellipse.x, ellipse.y - 2) == null &&
                     validField(ellipse.x, ellipse.y - 2)) ||

                    (findEllipse(ellipse.x + 1, ellipse.y) != null &&
                     findEllipse(ellipse.x + 2, ellipse.y) == null &&
                     validField(ellipse.x + 2, ellipse.y)) ||

                    (findEllipse(ellipse.x - 1, ellipse.y) != null &&
                     findEllipse(ellipse.x - 2, ellipse.y) == null &&
                     validField(ellipse.x - 2, ellipse.y)))
                {
                    return false;
                }
            }
            return true;
        }


        private Boolean validField(int x, int y)
        {
            if ((1 < x && x < 5) || (((-1 < x && x < 2) || (4 < x && x < 7)) && 1 < y && y < 5))
            {
                return true;
            }
            return false;
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
    }
}
