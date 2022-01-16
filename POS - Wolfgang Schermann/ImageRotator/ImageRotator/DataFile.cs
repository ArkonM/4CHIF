using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRotator
{
    class DataFile
    {

        public string FileName { get; set; }

        public string startPath { get; set; }

        public string endPath { get; set; }

        public DataFile(string fileName, string startPath, string endPath)
        {
            this.FileName = fileName;
            this.startPath = startPath;
            this.endPath = endPath;
        }
    }
}
