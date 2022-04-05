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

namespace WPF_8Queens_Problem
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
        

        // Funktion Ausgabe
        static void Print(int[,] field)
        {
            Console.WriteLine("**Output**");
            for (int i1 = 0; i1 < field.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < field.GetLength(1); i2++)
                {
                    Console.Write($"{field[i1, i2]}");
                }
                Console.WriteLine();
            }
        }
        // Funktion Reset
        static void Reset(in int[,] field)
        {
            Console.WriteLine("**Reset**");
            for (int i1 = 0; i1 < field.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < field.GetLength(1); i2++)
                {
                    field[i1, i2] = 0;
                }
            }
        }
        // Ist Feld Sicher
        static bool IsSafeFromQueen(int[,] field, int x, int y)
        {
            if (
                CheckX(field, x) &&
                CheckY(field, y) &&
                CheckDiag(field, x, y)
                )
            {
                return true;
            }
            return false;
        }
        private static bool CheckX(int[,] field, int x)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                if (field[x, i] == 1)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool CheckY(int[,] field, int y)
        {
            for (int i = 0; i < field.GetLength(1); i++)
            {
                if (field[i, y] == 1)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool CheckDiag(int[,] field, int x, int y)
        {
            int dif = x - y;
            int sum = x + y;
            for (int i1 = 0; i1 < field.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < field.GetLength(1); i2++)
                {
                    if (i1 + i2 == sum || i1 - i2 == dif)
                    {
                        if (field[i1, i2] == 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        static void PutPawns(in int[,] field)
        {
            for (int i1 = 0; i1 < field.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < field.GetLength(1); i2++)
                {
                    if (IsSafeFromQueen(field, i1, i2))
                    {
                        field[i1, i2] = 2;
                    }
                }
            }
        }
        static bool Backtrack(int[,] field, int x = 0)
        {
            Console.WriteLine("Backtrack " + x);
            // Terminate
            if (x == field.GetLength(0))
            {
                return true;
            }
            // Try To Place Queen
            for (int i2 = 0; i2 < field.GetLength(1); i2++)
            {
                if (IsSafeFromQueen(field, x, i2))
                {
                    // Place Queen
                    field[x, i2] = 1;
                    Print(field);
                    // Check next row
                    if (Backtrack(field, x + 1) == true)
                    {
                        return true;
                    }
                    else
                    {
                        // Revert Placement
                        field[x, i2] = 0;
                    }
                }
            }
            // Terminate if Q not placed
            Console.WriteLine("Not Placeable");
            return false;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            int size = Convert.ToInt32(NQueens.Text);
            int[,] field = new int[size, size];
            Backtrack(field);
            Console.WriteLine("Ende: ");
            Print(field);
            UniGrid.MaxHeight = 800;
            UniGrid.MaxWidth = 800;

            for (int i = 0; i < size*size; ++i)
            {
                Image l = new Image();
                BitmapImage myImageSource = new BitmapImage();
                myImageSource.BeginInit();
                myImageSource.UriSource = new Uri("C:/Users/armin/OneDrive/Desktop/8queens.png");
                myImageSource.EndInit();
                l.MaxHeight = 100;
                l.MaxWidth = 100;
                l.Source = myImageSource;
                
                UniGrid.Children.Add(l);
            }
        }
    }
}
