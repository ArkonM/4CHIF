using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CustomControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> personList;
        public string test = "Hello";
        public string Test { get { return test; } set { test = value; } }
        

        public MainWindow()
        {
            InitializeComponent();

            personList = new ObservableCollection<Person>();
            PersonLB.ItemsSource = personList;

            
        }

        public void Person_Add(object sender, ExecutedRoutedEventArgs e)
        {
            personList.Add(new Person(inputVname.Text, inputNName.Text, inputGeb.Text));
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person test = new Person("Lukas", "HS", "15.11.2003");
            personList.Add(test);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        //Write XML
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PersonList personListObj = new PersonList();
            personListObj.PersonListCollection = personList;

            XmlSerializer writer = new XmlSerializer(typeof(PersonList));

            string path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                
            }

            FileStream file = File.Create(path);
            writer.Serialize(file, personListObj);
            file.Close();

        }


        //XML Lesen
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;

            }

            XmlSerializer reader = new XmlSerializer(typeof(PersonList));
            FileStream file = File.OpenRead(path);
            PersonList personListObj = new PersonList();
            personListObj = (PersonList)reader.Deserialize(file);

            personList = personListObj.PersonListCollection;
            PersonLB.ItemsSource = personList;



        }



        private void CommandBinding_Test(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Puppe was geht!");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(test);
        }
    }
}
