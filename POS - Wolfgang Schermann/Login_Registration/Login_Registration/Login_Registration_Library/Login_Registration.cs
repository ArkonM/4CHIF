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

namespace Login_Registration_Library
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Login_Registration_Library"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Login_Registration_Library;assembly=Login_Registration_Library"
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
    public class Login_Registration : Control
    {
        

        static Login_Registration()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Login_Registration), new FrameworkPropertyMetadata(typeof(Login_Registration)));


        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var login = Template.FindName("loginGrid", this) as Grid;
            var registration = Template.FindName("registrationGrid", this) as Grid;
            var btnToRegister = Template.FindName("btnToRegister", this) as Button;
            var btnToLogin = Template.FindName("btnToLogin", this) as Button;
            //Butten, der zum Registration Screen wechselt bekommt ein Event...
            if (btnToRegister != null)
            {
                btnToRegister.Click += (s, a) =>
                {
                    login.Visibility = Visibility.Collapsed;
                    registration.Visibility = Visibility.Visible;
                };
            }
            //Button, der zum Login Screen wechselt bekommt ein Event....
            if (btnToLogin != null)
            {
                btnToLogin.Click += (s, a) =>
                {
                    
                    registration.Visibility = Visibility.Collapsed;
                    login.Visibility = Visibility.Visible;
                };
            }
        }
    }
}
