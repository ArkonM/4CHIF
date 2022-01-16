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
    class Ellipse : Basis
    {

        #region Dependency Properties
        public static readonly DependencyProperty RadiusHeightProperty = DependencyProperty.Register("RadiusHeight", typeof(Double), typeof(Ellipse), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty RadiusWidthProperty = DependencyProperty.Register("RadiusWidth", typeof(Double), typeof(Ellipse), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double RadiusHeight
        {
            get { return (double)base.GetValue(RadiusHeightProperty); }
            set { base.SetValue(RadiusHeightProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double RadiusWidth
        {
            get { return (double)base.GetValue(RadiusWidthProperty); }
            set { base.SetValue(RadiusHeightProperty, value); }
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
            myPathFigure.Segments.Add(new ArcSegment(new Point(X1 + 1, Y1), new Size(RadiusHeight, RadiusWidth), 360, true, SweepDirection.Clockwise, true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
