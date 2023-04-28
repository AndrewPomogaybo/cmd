using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class ReadLiner
    {
        public string ReadLinesAll(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                string file = String.Join(Environment.NewLine, lines);
                return file;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public void Read(int count, string path2)
        {
            try
            {
                using (StreamReader file = new StreamReader(path2))
                {
                    for (int i = 0; i < count; i++)
                    {
                        string line = file.ReadLine();
                        if (line == null)
                            break;
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
