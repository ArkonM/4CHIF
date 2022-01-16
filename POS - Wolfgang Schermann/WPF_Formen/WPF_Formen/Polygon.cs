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
    class Polygon : Rechteck
    {
        #region Dependency Properties
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(Double), typeof(Polygon), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty EdgesProperty = DependencyProperty.Register("Edges", typeof(Double), typeof(Polygon), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double Length
        {
            get { return (double)base.GetValue(LengthProperty); }
            set { base.SetValue(LengthProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Edges
        {
            get { return (double)base.GetValue(EdgesProperty); }
            set { base.SetValue(EdgesProperty, value); }
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Zeichnet eine Linie
        /// </summary>
        protected override PathFigure CreatePathFigure()
        {
            double angle = 360 / Edges;
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point((int)(X1 + Length * Math.Cos(0 * 2 * Math.PI / Edges)), (int)(Y1 + Length * Math.Sin(0 * 2 * Math.PI / Edges)));
            for (int i = 1; i < Edges; i++)
            {
                myPathFigure.Segments.Add(new LineSegment(new Point((int)(X1 + Length * Math.Cos(i * 2 * Math.PI / Edges)), (int)(Y1 + Length * Math.Sin(i * 2 * Math.PI / Edges))), true));
            }
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
