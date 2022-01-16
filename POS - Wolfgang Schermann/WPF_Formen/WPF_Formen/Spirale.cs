using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace WPF_Formen
{
    class Spirale : Basis
    {
        #region Dependency Properties
        public static readonly DependencyProperty UmdrehungProperty = DependencyProperty.Register("Umdrehung", typeof(Double), typeof(Spirale), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty SteigungProperty = DependencyProperty.Register("Steigung", typeof(Double), typeof(Spirale), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double Umdrehung
        {
            get { return (double)base.GetValue(UmdrehungProperty); }
            set { base.SetValue(UmdrehungProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Steigung
        {
            get { return (double)base.GetValue(SteigungProperty); }
            set { base.SetValue(SteigungProperty, value); }
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Zeichnet eine Linie
        /// </summary>
        protected override PathFigure CreatePathFigure()
        {
            PathFigure spiral = new PathFigure();
            PointCollection points = new PointCollection();

            double winkel = 0;
            double radius;
            double x;
            double y;

            spiral.StartPoint = new Point(X1, Y1);

            for (; winkel < 360 * Umdrehung; winkel += 1)
            {
                radius = winkel * (Steigung * 0.01 / (2 * Math.PI));

                double spinRad = (winkel * Math.PI) / 180;
                x = (double)(Math.Cos(spinRad) * radius + X1);
                y = (double)(Math.Sin(spinRad) * radius + Y1);

                points.Add(new Point(x, y));
            }

            Console.WriteLine(points);

            foreach (Point point in points)
            {
                if (point.X != 0 && point.Y != 0)
                    spiral.Segments.Add(new LineSegment(point, true));
            }

            spiral.IsClosed = false;
            return spiral;
        }
        #endregion
    }
}
