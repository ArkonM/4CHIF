using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace WPF_WordPad
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RichTB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateItemCheckedState(_btnBold, TextElement.FontWeightProperty, FontWeights.Bold);
            UpdateItemCheckedState(_btnItalic, TextElement.FontStyleProperty, FontStyles.Italic);
            UpdateItemCheckedState(_btnUnderline, Inline.TextDecorationsProperty, TextDecorations.Underline);

            UpdateItemCheckedState(_btnAlignLeft, Paragraph.TextAlignmentProperty, TextAlignment.Left);
            UpdateItemCheckedState(_btnAlignCenter, Paragraph.TextAlignmentProperty, TextAlignment.Center);
            UpdateItemCheckedState(_btnAlignRight, Paragraph.TextAlignmentProperty, TextAlignment.Right);
            UpdateItemCheckedState(_btnAlignJustify, Paragraph.TextAlignmentProperty, TextAlignment.Justify);

            UpdateSelectionListType();

            UpdateSelectedFontFamily();
            UpdateSelectedFontSize();

        }

        private void UpdateItemCheckedState(ToggleButton button, DependencyProperty formattingProperty, object expectedValue)
        {
            object currentValue = RichTB.Selection.GetPropertyValue(formattingProperty);
            button.IsChecked = (currentValue == DependencyProperty.UnsetValue) ? false : currentValue != null && currentValue.Equals(expectedValue);
        }

        private void UpdateSelectionListType()
        {
            Paragraph startParagraph = RichTB.Selection.Start.Paragraph;
            Paragraph endParagraph = RichTB.Selection.End.Paragraph;
            if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                if (markerStyle == TextMarkerStyle.Disc) //bullets
                {
                    _btnBullets.IsChecked = true;
                }
                else if (markerStyle == TextMarkerStyle.Decimal) //numbers
                {
                    _btnNumbers.IsChecked = true;
                }
            }
            else
            {
                _btnBullets.IsChecked = false;
                _btnNumbers.IsChecked = false;
            }
        }

        private void Window_Loaded(object sender, System.EventArgs e)
        {
            _fontFamily.ItemsSource = System.Windows.Media.Fonts.SystemFontFamilies;
            _fontSize.ItemsSource = FontSizes;
        }

        public double[] FontSizes
        {
            get
            {
                return new double[] {
                3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5,
                10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
                16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
                32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
                80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
                };
            }
        }

        private void _fontFamily_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FontFamily editValue = (FontFamily)e.AddedItems[0];
            ApplyPropertyValueToSelectedText(TextElement.FontFamilyProperty, editValue);
        }

        private void _fontSize_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ApplyPropertyValueToSelectedText(TextElement.FontSizeProperty, e.AddedItems[0]);
        }

        void ApplyPropertyValueToSelectedText(DependencyProperty formattingProperty, object value)
        {
            if (value == null)
            {
                return;
            }

            RichTB.Selection.ApplyPropertyValue(formattingProperty, value);
        }

        private void UpdateSelectedFontFamily()
        {
            object value = RichTB.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamily currentFontFamily = (FontFamily)((value == DependencyProperty.UnsetValue) ? null : value);
            if (currentFontFamily != null)
            {
                _fontFamily.SelectedItem = currentFontFamily;
            }
        }
        private void UpdateSelectedFontSize()
        {
            object value = RichTB.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            _fontSize.SelectedValue = (value == DependencyProperty.UnsetValue) ? null : value;
        }

        private void RibbonApplicationMenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XAML Format (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(RichTB.Document.ContentStart, RichTB.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Xaml);
                fileStream.Close();
            }
        }

        private void RibbonApplicationMenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XAML Format (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream xamlFile = new FileStream(dlg.FileName, FileMode.Open);
                FlowDocument cds = new FlowDocument((Block)XamlReader.Load(xamlFile));
                RichTB.Document = cds;
                xamlFile.Close();
            }
        }

        private void RibbonButton_Click_Help(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funktioniert wie WordPad!");
        }
    }
}
