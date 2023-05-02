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
            DirectoryInfo directoryinfo = new DirectoryInfo(folder);
            DirectoryInfo[] diA = directoryinfo.GetDirectories();
            FileInfo[] fileinfo = directoryinfo.GetFiles();

            foreach (FileInfo f in fileinfo)
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
