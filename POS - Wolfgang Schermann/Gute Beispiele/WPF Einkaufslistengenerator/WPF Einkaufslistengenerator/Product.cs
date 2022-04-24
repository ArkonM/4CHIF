using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Einkaufslistengenerator
{
    public class Product
    {

        private string productGroup;
        private string productName;
        private int amount;

        public string ProductGroup { get => productGroup; set => productGroup = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int Amount { get => amount; set => amount = value; }

        public Product ()
        {

        }

        public Product (string productGroup, string productName)
        {
            this.productGroup = productGroup;
            this.productName = productName;
        }

        public Product (string productGroup, string productName, int amount)
        {
            this.productGroup = productGroup;
            this.productName = productName;
            this.amount = amount;
        }
    }
}
