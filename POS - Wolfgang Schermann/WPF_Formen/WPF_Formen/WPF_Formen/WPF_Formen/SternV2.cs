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
    class SternV2 : Rechteck
    {

        #region Dependency Properties
        public static readonly DependencyProperty ZackenProperty = DependencyProperty.Register("Zackenanzahl", typeof(Double), typeof(SternV2), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region CLR Properties
        [TypeConverter(typeof(LengthConverter))]
        public double Zackenanzahl
        {
            get { return (double)base.GetValue(ZackenProperty); }
            set { base.SetValue(ZackenProperty, value); }
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Zeichnet eine Linie
        /// </summary>
        protected override PathFigure CreatePathFigure()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point((int)(X1 + X2 * Math.Cos(0 * 2 * Math.PI / Zackenanzahl)), (int)(Y1 + X2 * Math.Sin(0 * 2 * Math.PI / Zackenanzahl)));
            for (int i = 1; i < Zackenanzahl; i++)
            {
                myPathFigure.Segments.Add(new LineSegment(new Point((int)(X1 + X2 * Math.Cos(i * 2 * Math.PI / Zackenanzahl * 2)), (int)(Y1 + X2 * Math.Sin(i * 2 * Math.PI / Zackenanzahl * 2))), true));
            }
            myPathFigure.IsClosed = true;
            return myPathFigure;
        }

        #endregion

    }
}
