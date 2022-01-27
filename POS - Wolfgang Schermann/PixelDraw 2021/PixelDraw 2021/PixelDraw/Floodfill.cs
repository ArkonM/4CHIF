using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PixelDraw
{
    class Floodfill
	{
		// Diese Methode gibt true zurück, wenn die beiden Farben gleich sind, sonst false
		private static bool ColorMatch(Color replacementColor, Color targetColor)
		{
			return replacementColor.ToArgb() == targetColor.ToArgb();
		}

		// Diese Methode füllt alle zusammenhängenden Pixel, die dieselbe Farbe wie das Startpixel haben, mit der neuen Farbe
		private static void FloodFill(Bitmap bitmap, Point point, Color replacementColor)
		{
			Color targetColor = bitmap.GetPixel(point.X, point.Y); // Speichert die Farbe des Startpixels
			Queue<Point> queue = new Queue<Point>(); // Warteschlange der Pixel für die Breitensuche
			queue.Enqueue(point); // Fügt das Startpixel der Warteschlange hinzu
			while (queue.Count != 0) // So lange die Warteschlange nicht leer ist
			{
				Point point1 = queue.Dequeue(); // Entfernt das erste Pixel aus der Warteschlange
				if (!ColorMatch(bitmap.GetPixel(point1.X, point1.Y), targetColor)) // Wenn die Farbe des aktuellen Pixels gleich dem Startpixel ist, wird die nächste Iteration der äußeren while-Schleife ausgeführt
				{
					continue;
				}
				Point point2 = new Point(point1.X + 1, point1.Y); // Speichert das Pixel rechts vom aktuellen Pixel
				while (point1.X >= 0 && ColorMatch(bitmap.GetPixel(point1.X, point1.Y), targetColor)) // So lange das aktuelle Pixel nicht links vom Rand ist und die Farbe des Startpixels hat
				{
					bitmap.SetPixel(point1.X, point1.Y, replacementColor); // Setzt das aktuelle Pixel auf die neue Farbe
					if (point1.Y > 0 && ColorMatch(bitmap.GetPixel(point1.X, point1.Y - 1), targetColor)) // Wenn das aktuelle Pixel nicht am linken Rand ist und die Farbe des Startpixels hat
					{
						queue.Enqueue(new Point(point1.X, point1.Y - 1)); // Fügt das Pixel über dem aktuellen Pixel der Warteschlange hinzu
					}
					if (point1.Y < bitmap.Height - 1 && ColorMatch(bitmap.GetPixel(point1.X, point1.Y + 1), targetColor)) // Wenn das aktuelle Pixel nicht am rechten Rand ist und die Farbe des Startpixels hat
					{
						queue.Enqueue(new Point(point1.X, point1.Y + 1)); // Fügt das Pixel unter dem aktuellen Pixel der Warteschlange hinzu
					}
					point1.X--; // Verschiebt das aktuelle Pixel um 1 nach links
				}
				// Die folgende while-Schleife wiederholt den Ablauf mit dem Pixel rechts vom aktuellen Pixel
				while (point2.X <= bitmap.Width - 1 && ColorMatch(bitmap.GetPixel(point2.X, point2.Y), targetColor))
				{
					bitmap.SetPixel(point2.X, point2.Y, replacementColor);
					if (point2.Y > 0 && ColorMatch(bitmap.GetPixel(point2.X, point2.Y - 1), targetColor))
					{
						queue.Enqueue(new Point(point2.X, point2.Y - 1));
					}
					if (point2.Y < bitmap.Height - 1 && ColorMatch(bitmap.GetPixel(point2.X, point2.Y + 1), targetColor))
					{
						queue.Enqueue(new Point(point2.X, point2.Y + 1));
					}
					point2.X++; // Verschiebt das aktuelle Pixel um 1 nach rechts
				}
			}
		}

		// Hauptmethode, die das Programm ausführt
		public static void Main(string[] args)
		{
			Bitmap bitmap = new Bitmap("UnfilledCircle.png"); // Weist den Inhalt der Bilddatei mit dem angegebenen Dateinamen der Variablen der Klasse Bitmap hinzu
			FloodFill(bitmap, new Point(200, 200), Color.Red); // Aufruf der Methode mit den Parametern für Startpixel und Farbe
			bitmap.Save("FilledCircle.png"); // Speichert das geänderte Bitmap als Bilddatei mit dem angegebenen Dateinamen
		}
	}
}
