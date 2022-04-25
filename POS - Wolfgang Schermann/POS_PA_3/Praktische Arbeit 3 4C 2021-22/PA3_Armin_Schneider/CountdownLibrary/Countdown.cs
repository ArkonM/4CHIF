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

namespace CountdownLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CountdownLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CountdownLibrary;assembly=CountdownLibrary"
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
    public class CountdownControl : Control

    {
        static CountdownControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CountdownControl), new FrameworkPropertyMetadata(typeof(CountdownControl)));
        }

        public static readonly DependencyProperty
            Count = DependencyProperty.Register(
                    "Countdown",
                  typeof(int),
                     typeof(CountdownControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        ));

        public int Countdown
        {
            get { return (int)GetValue(Count); }
            set { MaxCount = value; SetValue(Count, value); }
        }


        public static readonly DependencyProperty
            MaxCountdown = DependencyProperty.Register(
                    "MaxCount",
                  typeof(int),
                     typeof(CountdownControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        ));

        public int MaxCount
        {
            get { return (int)GetValue(MaxCountdown); }
            set { if(value > MaxCount) { SetValue(MaxCountdown, value); } }
        }
    }
}
