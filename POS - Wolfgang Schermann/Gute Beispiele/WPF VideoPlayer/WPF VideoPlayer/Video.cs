using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VideoPlayer
{
    class Video
    {

        private string name;
        private string path;
        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }

        public Video (string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}
