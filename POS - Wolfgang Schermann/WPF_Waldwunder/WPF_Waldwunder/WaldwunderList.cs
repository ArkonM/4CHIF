using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Waldwunder
{
    public class WaldwunderList
    {
        public ObservableCollection<Waldwunder> waldwunderList;

        public WaldwunderList()
        {
            waldwunderList = new ObservableCollection<Waldwunder>();
        }
    }
}
