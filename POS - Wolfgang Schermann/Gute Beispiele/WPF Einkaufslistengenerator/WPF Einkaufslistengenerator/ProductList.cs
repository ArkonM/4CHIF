using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Einkaufslistengenerator
{
    public class ProductList
    {

        public ObservableCollection<Product> ProductListCollection { get; set; }

        public ProductList() { ProductListCollection = new ObservableCollection<Product>(); }

    }
}
