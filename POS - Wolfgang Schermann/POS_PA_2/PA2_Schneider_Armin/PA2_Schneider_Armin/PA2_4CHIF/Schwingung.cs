using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PA2_4CHIF
{
    public delegate double ValueHandler(double value);
    class Schwingung : Basis
    {

        private ValueHandler Handler;

        public static readonly DependencyProperty AmpProperty = DependencyProperty.Register("Amp", typeof(Double), typeof(Schwingung), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty FreProperty = DependencyProperty.Register("Fre", typeof(Double), typeof(Schwingung), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty BreProperty = DependencyProperty.Register("Bre", typeof(Double), typeof(Schwingung), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));


        public double Amp
        {
            get { return (double)base.GetValue(AmpProperty); }
            set { base.SetValue(AmpProperty, value); }
        }


        public double Fre
        {
            get { return (double)base.GetValue(FreProperty); }
            set { base.SetValue(FreProperty, value); }
        }


        public double Bre
        {
            get { return (double)base.GetValue(BreProperty); }
            set { base.SetValue(BreProperty, value); }
        }

        protected override PathFigure CreatePathFigure()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(X1 - 10, Y1);
            myPathFigure.Segments.Add(new LineSegment(new Point(X1 + 10, Y1), true));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1, Y1 - 10), false));
            myPathFigure.Segments.Add(new LineSegment(new Point(X1, Y1 + 10), true));
            myPathFigure.IsClosed = false;
            return myPathFigure;
        }


        /*public IAsyncResult Start(double res, AsyncCallback Call, object state)
        {
            Handler = new ValueHandler(CreatePathFigure);
            return Handler.BeginInvoke(res, Call, state);
        }

        public double EndPaint(IAsyncResult res)
        {
            return Handler.EndInvoke(res);
        }*/
    }
}
