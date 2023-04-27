using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class FileReader
    {
        public string ReadFile(string path2)
        {
            using (StreamReader reader = new StreamReader(path2))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    return line;
                }
            }
            return "";
        }
    }
}
