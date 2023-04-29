using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class Size
    {
        public double GetSize(string folder, double catalogSize)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            DirectoryInfo[] diA = di.GetDirectories();
            FileInfo[] fi = di.GetFiles();

            foreach (FileInfo f in fi)
            {
                catalogSize = catalogSize + f.Length;
            }

            foreach (DirectoryInfo df in diA)
            {
                GetSize(df.FullName, catalogSize);
            }

            return Math.Round((double)(catalogSize), 1);
        }
    }
}
