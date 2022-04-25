using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControl
{
    public class Person
    {
        private string vName;
        private string nName;
        private string geb;

        public string VName { get => vName; set => vName = value; }
        public string NName { get => nName; set => nName = value; }
        public string Geb { get => geb; set => geb = value; }

        public Person ()
        {

        }

        public Person (string vn, string nn, string geb)
        {
            vName = vn;
            nName = nn;
            this.geb = geb;
        }

    }
}
