using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileSelector
{
    public delegate List<String> ScanHandler(List<String> list);

    class Worker
    {

        private ScanHandler Handler;

        private List<String> ScanFile(List<String> pfad)
        {
            List<String> list = new List<string>();
            try
            {
                foreach (string i in pfad)
                {
                    var files = Directory.GetFiles(i);
                    list.AddRange(files);
                }
            }
            catch (Exception)
            {
                throw new Exception("Abgebrochen");
            }
            Thread.Sleep(2000);
            return list;
        }

        public IAsyncResult Start(List<String> pfad, AsyncCallback Call, object state)
        {
            Handler = new ScanHandler(ScanFile);
            return Handler.BeginInvoke(pfad, Call, state);
        }

        public List<String> EndScan(IAsyncResult res)
        {
            return Handler.EndInvoke(res);
        }
    }
}