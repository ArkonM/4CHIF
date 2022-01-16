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
    class Quadrat : Rechteck
    {

        #region Dependency Properties
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(Double), typeof(Quadrat), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double Length
        {
            get { return (double)base.GetValue(LengthProperty); }
            set { base.SetValue(LengthProperty, value); }
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
            myPathFigure.Segments.Add(new LineSegment(new Point(X1, Y1 + Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + Length, Y1 + Length), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + Length, Y1), true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }
        #endregion

    }
}
