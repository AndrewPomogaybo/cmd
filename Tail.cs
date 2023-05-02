using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class TailArgs
    {
        public void Tail(string path, int n)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                List<string> lines = new List<string>();
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                List<string> lastLines = lines.Skip(Math.Max(0, lines.Count - n)).ToList();

                foreach (string lastLine in lastLines)
                {
                    Console.WriteLine(lastLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
