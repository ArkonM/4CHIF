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
    class Kreis : Basis 
    {

        #region Dependency Properties
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(Double), typeof(Kreis), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double Radius
        {
            get { return (double)base.GetValue(RadiusProperty); }
            set { base.SetValue(RadiusProperty, value); }
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
            myPathFigure.Segments.Add(new ArcSegment(new Point(X1 + 1, Y1), new Size(Radius, Radius), 360, true, SweepDirection.Clockwise, true));
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
