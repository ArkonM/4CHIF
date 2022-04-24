using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
using System.Xml.Serialization;

namespace WPF_Einkaufslistengenerator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<string, List<string>> productDic;
        private ObservableCollection<Product> productList;
        private string fileName = string.Empty;
        public MainWindow()
        {
            InitializeComponent();

            productDic = new Dictionary<string, List<string>>();
            productList = new ObservableCollection<Product>();

            ProductGroupCB.ItemsSource = productDic.Keys;
            ProductLB.ItemsSource = productList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo currenCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            string fn = currenCulture.Equals(new CultureInfo("de")) ? @"..\..\Produkte.csv" : @"..\..\Products.csv";


            using (var reader = new StreamReader(fn))
            {

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    //values[0] = Gruppe, values[1] = Produkt

                    if(productDic.ContainsKey(values[0]))
                    {
                        productDic[values[0]].Add(values[1]);
                    }
                    else
                    {
                        List<string> items = new List<string>();
                        items.Add(values[1]);
                        productDic.Add(values[0], items);
                    }
                }

               /* foreach (var item in productsDic) //ersetzt oben im Konstruktor
                {
                    ProductGroupCB.Items.Add(item.Key);
                } */

            }
        }

        private void ProductGroupCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           /* foreach (var item in productsDic)
            {
                if (item.Key.Equals(ProductGroupCB.SelectedItem.ToString()))
                {
                    ProductCB.ItemsSource = item.Value;
                    break;
                }
            } */

            ProductCB.ItemsSource = productDic[ProductGroupCB.SelectedItem.ToString()];
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(ProductBox.Text.Trim().Length == 0 && (ProductGroupCB.SelectedIndex == -1 || ProductCB.SelectedIndex == -1))
            {
                MessageBox.Show(WPF_Einkaufslistengenerator.Properties.Resources.ErrorMsg);
                return;
            }

            Product product = ProductBox.Text.Trim().Length == 0 ?
                new Product(ProductGroupCB.SelectedItem.ToString(), ProductCB.SelectedItem.ToString(), (int)AmountNumUpDown.Value)
                :
                new Product(Properties.Resources.spezifischesProdukt, ProductBox.Text, (int)AmountNumUpDown.Value);

            productList.Add(product);

            ProductBox.Clear();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = productList.Count > 0;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            productList.Clear();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Elvin\source\repos\Übung_3PA\WPF Einkaufslistengenerator\WPF Einkaufslistengenerator";
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;

                ReadXml();
            }

        }

        private void ReadXml()
        {
            XmlSerializer reader = new XmlSerializer(typeof(ProductList));
            FileStream file = File.OpenRead(fileName);
            ProductList prodListObj = (ProductList)reader.Deserialize(file);
            file.Close();

            productList = prodListObj.ProductListCollection;
            ProductLB.ItemsSource = productList;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (fileName.Equals(string.Empty))
            {
                GetSavePathAndWriteXML();
                return;
            }
            WriteXML();
        }

        private void WriteXML()
        {
            ProductList prodListObj = new ProductList();
            prodListObj.ProductListCollection = productList;

            XmlSerializer writer = new XmlSerializer(typeof(ProductList));
            FileStream file = File.Create(fileName); //fileName = path
            writer.Serialize(file, prodListObj);
            file.Close();
        }

        private void GetSavePathAndWriteXML()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\Users\Elvin\source\repos\Übung_3PA\WPF Einkaufslistengenerator\WPF Einkaufslistengenerator";
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                fileName = saveFileDialog.FileName;
                WriteXML();
            }

        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSavePathAndWriteXML();
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(ProductLB, "Shoppinglist");
            }
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = ProductLB.SelectedItems;
            int idx = -1;

            for (int i = 0; i < selectedItems.Count; i++)
            {
                idx = productList.IndexOf((Product)selectedItems[i]);
                productList.RemoveAt(idx);
                i--;
            }
        }
    }
}
