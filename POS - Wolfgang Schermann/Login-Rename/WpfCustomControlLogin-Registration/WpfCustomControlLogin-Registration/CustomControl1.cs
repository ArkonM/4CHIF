﻿using System;
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

namespace WpfCustomControlLogin_Registration
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLogin_Registration"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLogin_Registration;assembly=WpfCustomControlLogin_Registration"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var Login = Template.FindName("Login", this) as Grid;
            var Regis = Template.FindName("Registration", this) as Grid;
            var regisBtn = Template.FindName("PART_REGISTRATION", this) as Button;
            var loginBtn = Template.FindName("PART_LOGIN", this) as Button;
            if (regisBtn != null)
            {
                regisBtn.Click += (s, a) =>
                {
                    Login.Visibility = Visibility.Collapsed;
                    Regis.Visibility = Visibility.Visible;
                };
            }
            if (loginBtn != null)
            {
                loginBtn.Click += (s, a) =>
                {

                    Regis.Visibility = Visibility.Collapsed;
                    Login.Visibility = Visibility.Visible;
                };
            }
        }

    }
}
