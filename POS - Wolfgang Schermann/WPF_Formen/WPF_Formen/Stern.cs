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
    class Stern : Basis
    {
        #region Dependency Properties
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(Double), typeof(Stern), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(Double), typeof(Stern), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Ecken = DependencyProperty.Register("Eck", typeof(Int32), typeof(Stern), new FrameworkPropertyMetadata(3, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double X2
        {
            get { return (double)base.GetValue(X2Property); }
            set { base.SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y2
        {
            get { return (double)base.GetValue(Y2Property); }
            set { base.SetValue(Y2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Eck
        {
            get { return (double)base.GetValue(Ecken); }
            set { base.SetValue(Ecken, value); }
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
            myPathFigure.Segments.Add(new LineSegment(new Point(X1, Y2), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X2, Y2), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X2, Y1), true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }
        #endregion
    }
}
