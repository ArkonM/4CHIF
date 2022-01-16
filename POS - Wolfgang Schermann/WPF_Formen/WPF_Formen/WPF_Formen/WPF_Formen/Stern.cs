using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_Formen
{
    class Stern : Basis
    {

        #region Dependency Properties
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(Double), typeof(Stern), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(Double), typeof(Stern), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties

        [TypeConverter(typeof(LengthConverter))]
        public double Length
        {
            get { return (double)base.GetValue(LengthProperty); }
            set { base.SetValue(LengthProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Angle
        {
            get { return (double)base.GetValue(AngleProperty); }
            set { base.SetValue(AngleProperty, value); }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Zeichnet eine Linie
        /// </summary>
        protected override PathFigure CreatePathFigure()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(X1, Y1);
            double angleRadians = Math.PI / 180 * Angle;
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + (Length * Math.Cos(angleRadians)), Y1 + Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + (Length * Math.Cos(angleRadians)) + Length, Y1 + Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + (Length * Math.Cos(angleRadians)), Y1 + Length + (Length * Math.Sin(angleRadians))), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + (Length * Math.Cos(angleRadians)) + Length, Y1 + 3 * Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1, Y1 + 2 * Length + Length / 2), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 - (Length * Math.Cos(angleRadians)) - Length, Y1 + 3 * Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 - (Length * Math.Cos(angleRadians)), Y1 + Length + (Length * Math.Sin(angleRadians))), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 - (Length * Math.Cos(angleRadians)) - Length, Y1 + Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 - (Length * Math.Cos(angleRadians)), Y1 + Length), true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
