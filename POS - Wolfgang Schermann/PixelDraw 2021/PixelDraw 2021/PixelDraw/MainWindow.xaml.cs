using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace PixelDraw
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly int imageSize = 300;
        private static WriteableBitmap _wb;
        private static int _bytesPerPixel;
        private static int _stride;
        private static byte[] _colorArray;

        public MainWindow()
        {
            InitializeComponent();
            _wb = new WriteableBitmap(imageSize, imageSize, 96, 96, PixelFormats.Bgra32, null);
            _bytesPerPixel = (_wb.Format.BitsPerPixel + 7) / 8;
            _stride = _wb.PixelWidth * _bytesPerPixel;
            _colorArray = ConvertColor(Colors.Black);
            drawing.Source = _wb;

            List<Color> colors = new List<Color>();
            colors.Add(Colors.Aqua);
            colors.Add(Colors.Salmon);
            colors.Add(Colors.Red);
            colors.Add(Colors.Salmon);
            colors.Add(Colors.Orange);
            colors.Add(Colors.Green);
            colorSelect.ItemsSource = colors;
        }

        #region Hilfsfunktionen

        private static byte[] ConvertColor(Color color)
        {
            byte[] c = new byte[4];
            c[0] = color.B;
            c[1] = color.G;
            c[2] = color.R;
            c[3] = color.A;
            return c;
        }

        private static Color ConvertColor(byte[] color)
        {
            Color c = new Color();
            c.B = color[0];
            c.G = color[1];
            c.R = color[2];
            c.A = color[3];
            return c;
        }

        private void setPixel(Color c, int x, int y)
        {
            if (x < _wb.PixelWidth && x > 0 && y < _wb.PixelHeight && y > 0)
            {
                _wb.WritePixels(new Int32Rect(x, y, 1, 1), ConvertColor(c), _stride, 0);
            }
        }

        private void setPixel(int x, int y)
        {
            if (x < _wb.PixelWidth && x > 0 && y < _wb.PixelHeight && y > 0)
            {
                _wb.WritePixels(new Int32Rect(x, y, 1, 1), _colorArray, _stride, 0);
            }
        }

        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 10; i <= 290; i++)
            {
                setPixel(i, 10);
                setPixel(i, 290);
                setPixel(10, i);
                setPixel(290, i);
            }
            for (int i = 10; i <= 290; i+=20)
            {
                drawLine(150, 150, 10, i);
                drawLine(150, 150, 290, i);
                drawLine(150, 150, i, 10);
                drawLine(150, 150, i, 290);
            }
        }


        private static byte[] _readArray = ConvertColor(Colors.Black);

        private void setPixelThreaded(Color c, int x, int y)
        {
            try
            {
                _wb.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      if (x < _wb.PixelWidth && x > 0 && y < _wb.PixelHeight && y > 0)
                      {
                          _wb.WritePixels(new Int32Rect(x, y, 1, 1), ConvertColor(c), _stride, 0);
                      }
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }

        private Color getPixelThreaded(int x, int y)
        {
            Color res = Colors.Transparent;
            try
            {
                _wb.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      if (x < _wb.PixelWidth && x > 0 && y < _wb.PixelHeight && y > 0)
                      {
                          _wb.CopyPixels(new Int32Rect(x, y, 1, 1), _readArray, _stride, 0);
                          res = ConvertColor(_readArray);
                      }
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return res;
        }

        private Color getPixel(int x, int y)
        {
            Color res = Colors.Transparent;
            if (x < _wb.PixelWidth && x > 0 && y < _wb.PixelHeight && y > 0)
            {
                _wb.CopyPixels(new Int32Rect(x, y, 1, 1), _readArray, _stride, 0);
                res = ConvertColor(_readArray);
            }

            return res;
        }


        private void drawLine(int x1, int y1, int x2, int y2)
        {
            //hier euren Algorithmus implementieren
            //setPixel(x, y); zeichnet einen Punkt
            int p1 = x1 - x2;
            int p2 = y1 - y2;
            int xc = x1;
            int yc = y1;
            float test = p1 / p2;
            if (p1 < p2)
            {
                int i = 1;
                while (yc != y2)
                {
                    if (y2 < yc)
                    {
                        yc--;
                        if (i == test)
                        {
                            if(xc < x2)
                            {
                                xc++;
                            }
                            if(xc > x2)
                            {
                                xc--;
                            }
                        }
                    } else
                    {
                        yc--;
                        if (i == test)
                        {
                            if (xc < x2)
                            {
                                xc++;
                            }
                            if (xc > x2)
                            {
                                xc--;
                            }
                        }
                    }
                }
            } else
            {
                int i = 1;
                while (xc != x2)
                {
                    if (x2 < xc)
                    {
                        xc--;
                        if (i == test)
                        {
                            if (yc < y2)
                            {
                                yc++;
                            }
                            if (yc > y2)
                            {
                                yc--;
                            }
                            i = 0;
                        }
                    }
                    else
                    {
                        xc--;
                        if (i == test)
                        {
                            if (yc < y2)
                            {
                                yc++;
                            }
                            if (yc > y2)
                            {
                                yc--;
                            }
                            i = 0;
                        }
                    }
                }
            }
        }
    }
}
