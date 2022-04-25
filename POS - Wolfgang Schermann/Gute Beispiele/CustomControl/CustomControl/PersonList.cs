using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControl
{
    public class PersonList
    {
        public ObservableCollection<Person> PersonListCollection { get; set; }
        public PersonList()
        {
            PersonListCollection = new ObservableCollection<Person>();
        }

    }
}
