using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndikatorCustom
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:IndikatorCustom"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:IndikatorCustom;assembly=IndikatorCustom"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Dieses Projekt auswählen]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class Indikator : Control
    {
        static Indikator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Indikator), new FrameworkPropertyMetadata(typeof(Indikator)));
        }

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Image Basis = (Image)Template.FindName("basis", this);
            Basis.Source = loadBitmap(Images.body);

            Image Zeiger = (Image)Template.FindName("zeiger", this);
            Zeiger.Source = loadBitmap(Images.indikator);

        }

        public void changeAngle(double wert, double min, double max)
        {
            double sAngle = 0;
            wert -= min;
            if (max < min)
            {
                max = min + 10;
            }
            sAngle = (wert / (max-min)) * 287;
            TextBox currVal = (TextBox)Template.FindName("CurrAnz", this);
            RotateTransform rotate = (RotateTransform)Template.FindName("rotate", this);
            rotate.Angle = sAngle;
        }
    }
}
