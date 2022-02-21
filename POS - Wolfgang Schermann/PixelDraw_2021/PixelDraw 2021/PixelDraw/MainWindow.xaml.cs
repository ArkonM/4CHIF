using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace PixelDraw
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 
    // Sabani Elvin
    public partial class MainWindow : Window
    {
        private static readonly int imageSize = 300;
        private static WriteableBitmap _wb;
        private static int _bytesPerPixel;
        private static int _stride;
        private static byte[] _colorArray;
        private static byte[] _readArray = ConvertColor(Colors.Black);
        private Point startPoint, endPoint;
        private Color currColor;
        private DataFill obj;
        private bool isSwitch;

        public MainWindow()
        {
            InitializeComponent();
            _wb = new WriteableBitmap(imageSize, imageSize, 96, 96, PixelFormats.Bgra32, null);
            _bytesPerPixel = (_wb.Format.BitsPerPixel + 7) / 8;
            _stride = _wb.PixelWidth * _bytesPerPixel;
            _colorArray = ConvertColor(Colors.Black);
            drawing.Source = _wb;

            List<Color> colorList = new List<Color>();
            colorList.Add(Colors.Aqua);
            colorList.Add(Colors.Coral);
            colorList.Add(Colors.Salmon);

            ColorsComboBox.ItemsSource = colorList;
            currColor = Colors.Black;
            obj = new DataFill();
            isSwitch = false;
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

        private int getBitmapWidthThreaded()
        {
            int width = 0;
            try
            {
                _wb.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      width = _wb.PixelWidth;
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return width;
        }

        private int getBitmapHeightThreaded()
        {
            int height = 0;
            try
            {
                _wb.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal
                  , new System.Windows.Threading.DispatcherOperationCallback(delegate
                  {
                      height = _wb.PixelHeight;
                      return null;
                  }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return height;
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
            for (int i = 10; i <= 290; i += 20)
            {
                drawLine(currColor, 150, 150, 10, i);
                drawLine(currColor, 150, 150, 290, i);
                drawLine(currColor, 150, 150, i, 10);
                drawLine(currColor, 150, 150, i, 290);
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _wb = new WriteableBitmap(imageSize, imageSize, 96, 96, PixelFormats.Bgra32, null);
            _bytesPerPixel = (_wb.Format.BitsPerPixel + 7) / 8;
            _stride = _wb.PixelWidth * _bytesPerPixel;
            _colorArray = ConvertColor(Colors.Black);
            drawing.Source = _wb;
        }

        #region Algorithmen
        private void drawLine(Color c, int x1, int y1, int x2, int y2)
        {
            //hier euren Algorithmus implementieren
            //setPixel(x, y); zeichnet einen Punkt
            //Bresenham Algorithm

            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                setPixel(c, x1, y1);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }

        private void drawCircle(Color currColor, int x1, int y1, int x2, int y2)
        {
            double radius = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double lenX, lenY;
            const int DEGREES = 360;

            for (double i = 0; i < DEGREES; i += 0.1)
            {
                lenX = radius * Math.Cos(i * Math.PI / 180);
                lenY = radius * Math.Sin(i * Math.PI / 180);
                setPixel(currColor, (int)(x1 + lenX), (int)(y1 + lenY));
            }
        }

        private void _fill4RecStart(object a)
        {
            fill4Rec(obj.X, obj.Y, obj.oldColor, obj.newColor);
        }

        private void fill4Rec(int x, int y, Color oldColor, Color newColor)
        {
            if (getPixelThreaded(x, y).Equals(oldColor))
            {
                setPixelThreaded(newColor, x, y);
                fill4Rec(x, y + 1, oldColor, newColor);  // unten
                fill4Rec(x, y - 1, oldColor, newColor);  // oben
                fill4Rec(x + 1, y, oldColor, newColor);  // rechts
                fill4Rec(x - 1, y, oldColor, newColor);  // links
            }
        }

        private void _fill4ItStart(object a)
        {
            fill4It(startPoint, currColor);
        }

        // Diese Methode füllt alle zusammenhängenden Pixel, die dieselbe Farbe wie das Startpixel haben, mit der neuen Farbe
        private void fill4It(Point point, Color replacementColor)
        {
            Color targetColor = getPixelThreaded((int)point.X, (int)point.Y); // Speichert die Farbe des Startpixels
            Queue<Point> queue = new Queue<Point>(); // Warteschlange der Pixel für die Breitensuche
            queue.Enqueue(point); // Fügt das Startpixel der Warteschlange hinzu
            while (queue.Count != 0) // So lange die Warteschlange nicht leer ist
            {
                Point point1 = queue.Dequeue(); // Entfernt das erste Pixel aus der Warteschlange
                if (!getPixelThreaded((int)point1.X, (int)point1.Y).Equals(targetColor)) // Wenn die Farbe des aktuellen Pixels gleich dem Startpixel ist, wird die nächste Iteration der äußeren while-Schleife ausgeführt
                {
                    continue;
                }
                Point point2 = new Point(point1.X + 1, point1.Y); // Speichert das Pixel rechts vom aktuellen Pixel
                while (point1.X >= 0 && getPixelThreaded((int)point1.X, (int)point1.Y).Equals(targetColor)) // So lange das aktuelle Pixel nicht links vom Rand ist und die Farbe des Startpixels hat
                {
                    setPixelThreaded(replacementColor, (int)point1.X, (int)point1.Y); // Setzt das aktuelle Pixel auf die neue Farbe
                    if ((int)point1.Y > 0 && getPixelThreaded((int)point1.X, (int)point1.Y - 1).Equals(targetColor)) // Wenn das aktuelle Pixel nicht am linken Rand ist und die Farbe des Startpixels hat
                    {
                        queue.Enqueue(new Point(point1.X, point1.Y - 1)); // Fügt das Pixel über dem aktuellen Pixel der Warteschlange hinzu
                    }
                    if ((int)point1.Y < getBitmapHeightThreaded() - 1 && getPixelThreaded((int) point1.X, (int)point1.Y + 1).Equals(targetColor))
                    {
                        queue.Enqueue(new Point(point1.X, point1.Y + 1)); // Fügt das Pixel unter dem aktuellen Pixel der Warteschlange hinzu
                    }
                    point1.X--; // Verschiebt das aktuelle Pixel um 1 nach links
                }
                // Die folgende while-Schleife wiederholt den Ablauf mit dem Pixel rechts vom aktuellen Pixel
                while (point2.X <= getBitmapWidthThreaded() - 1 && getPixelThreaded((int)point2.X, (int)point2.Y).Equals(targetColor))
                {
                    setPixelThreaded(replacementColor, (int)point2.X, (int)point2.Y);
                    if (point2.Y > 0 && getPixelThreaded((int)point2.X, (int)point2.Y - 1).Equals(targetColor))
                    {
                        queue.Enqueue(new Point(point2.X, point2.Y - 1));
                    }
                    if (point2.Y < getBitmapHeightThreaded() - 1 && getPixelThreaded((int)point2.X, (int)point2.Y + 1).Equals(targetColor))
                    {
                        queue.Enqueue(new Point(point2.X, point2.Y + 1));
                    }
                    point2.X++; // Verschiebt das aktuelle Pixel um 1 nach rechts
                }
            }

        }

        #endregion

        #region Events
        private void drawing_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(drawing);
            endPoint.X = endPoint.X * imageSize / drawing.ActualWidth;
            endPoint.Y = endPoint.Y * imageSize / drawing.ActualHeight;

            switch (DrawBox.SelectionBoxItem.ToString())
            {
                case "Linie": drawLine(currColor, (int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y); break;
                case "Kreis": drawCircle(currColor, (int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y); break;
                case "Füllen":

                    obj.X = (int)startPoint.X;
                    obj.Y = (int)startPoint.Y;
                    obj.oldColor = getPixel((int)startPoint.X, (int)startPoint.Y);
                    obj.newColor = currColor;
                    if (isSwitch)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(_fill4ItStart));
                    } 
                    else
                    {
                        // ThreadPool.QueueUserWorkItem(new WaitCallback(_fill4RecStart)); //Problem: bei sehr großen Flächen (zu viele rekursiv-Aufrufe): Stackoverflowexception
                        Thread t = new Thread(_fill4RecStart, 50000000);
                        t.Start();
                    }
                    isSwitch = !isSwitch;
                    break;
                case "Dijkstra":

                    Color oldColor = getPixel((int)startPoint.X, (int)startPoint.Y);
                    Dijkstra d = new Dijkstra(_wb.PixelWidth, _wb.PixelHeight);
                    //Color vom Startpunkt und Endpunkt müssen gleich sein
                    if (oldColor.Equals(getPixel((int)endPoint.X, (int)endPoint.Y))){
                        for(int i = 0; i < _wb.PixelHeight; i++)
                        {
                            for(int j = 0; j < _wb.PixelWidth; j++)
                            {
                                
                                if(getPixel(i, j).Equals(oldColor))
                                {
                                    d.AddNode(i, j);
                                }
                                
                            }
                        }

                        d.setStart((int)startPoint.X, (int)startPoint.Y);
                        d.setEnd((int)endPoint.X, (int)endPoint.Y);

                        d.SearchRoute();

                        List<Point> pointList = d.getRoute();

                        for (int i = 0; i < pointList.Count - 1; i++)
                        {
                            drawLine(currColor, (int)pointList[i].X, (int)pointList[i].Y, (int)pointList[i + 1].X, (int)pointList[i + 1].Y);
                        }

                    }
                    
                    break;

                case "A*":

                    oldColor = getPixel((int)startPoint.X, (int)startPoint.Y);
                    AStern st = new AStern(_wb.PixelWidth, _wb.PixelHeight);
                    //Color vom Startpunkt und Endpunkt müssen gleich sein
                    if (oldColor.Equals(getPixel((int)endPoint.X, (int)endPoint.Y)))
                    {
                        for (int i = 0; i < _wb.PixelHeight; i++)
                        {
                            for (int j = 0; j < _wb.PixelWidth; j++)
                            {

                                if (getPixel(i, j).Equals(oldColor))
                                {
                                    st.AddNode(i, j);
                                }

                            }
                        }

                        st.setStart((int)startPoint.X, (int)startPoint.Y);
                        st.setEnd((int)endPoint.X, (int)endPoint.Y);

                        st.SearchRoute();

                        List<Point> pointList = st.getRoute();

                        for (int i = 0; i < pointList.Count - 1; i++)
                        {
                            drawLine(currColor, (int)pointList[i].X, (int)pointList[i].Y, (int)pointList[i + 1].X, (int)pointList[i + 1].Y);
                        }

                    }

                    break;

            }
        }

        private void ColorsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (ColorsComboBox.SelectedIndex)
            {
                case 0: currColor = Colors.Aqua; break;
                case 1: currColor = Colors.Coral; break;
                case 2: currColor = Colors.Salmon; break;

            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void drawing_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Beispiel LeftButtonDown auf Image mit Umrechnung in Pixelkoordinaten
            startPoint = e.GetPosition(drawing);
            startPoint.X = startPoint.X * imageSize / drawing.ActualWidth;
            startPoint.Y = startPoint.Y * imageSize / drawing.ActualHeight;
            //drawLine((int)p.X, (int)p.Y, (int)p.X + 100, (int)p.Y + 50);
        }
        #endregion
    }
}
