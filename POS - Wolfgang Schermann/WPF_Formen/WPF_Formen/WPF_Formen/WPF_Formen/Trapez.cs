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
    class Trapez : Basis
    {

        #region Dependency Properties
        public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(Double), typeof(Trapez), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(Double), typeof(Trapez), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty CProperty = DependencyProperty.Register("C", typeof(Double), typeof(Trapez), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty AStartProperty = DependencyProperty.Register("AStart", typeof(Double), typeof(Trapez), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
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

        [TypeConverter(typeof(LengthConverter))]
        public double C
        {
            get { return (double)base.GetValue(CProperty); }
            set { base.SetValue(CProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double AStart
        {
            get { return (double)base.GetValue(AStartProperty); }
            set { base.SetValue(AStartProperty, value); }
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
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + C, Y1), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + A, Y1 + B), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(AStart, Y1 + B), true));    //AStart für den linken Punkt der unteren Linie
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}