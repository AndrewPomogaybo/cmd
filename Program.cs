using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = Console.ReadLine();

            switch (query) 
            {
                case "ls":
                    string dirName = "C:\\";
                    if (Directory.Exists(dirName))
                    {
                        string[] dirs = Directory.GetDirectories(dirName);
                        foreach (string s in dirs)
                        {
                            Console.WriteLine(s);
                        }
                        Console.WriteLine();
                        string[] files = Directory.GetFiles(dirName);
                        foreach (string s in files)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    break;
                case "--help":
                    Process process = new Process();
                    process.StartInfo.FileName = "git";
                    process.StartInfo.Arguments = "help";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();
                    query = process.StandardOutput.ReadToEnd();
                    Console.WriteLine(query);
                    break;
            }
            Console.ReadKey();
        }
    }
}
