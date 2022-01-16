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
    class Parallelogramm : Basis
    {

        #region Dependency Properties
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(Double), typeof(Parallelogramm), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(Double), typeof(Parallelogramm), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(Double), typeof(Parallelogramm), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double X2
        {
            get { return (double)base.GetValue(X2Property); }
            set { base.SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double A
        {
            get { return (double)base.GetValue(AProperty); }
            set { base.SetValue(AProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double B
        {
            get { return (double)base.GetValue(BProperty); }
            set { base.SetValue(BProperty, value); }
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
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + A, Y1), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X2 + A, Y1 + B), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X2, Y1 + B), true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
