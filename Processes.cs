using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class Processes
    {
        Process process = new Process();
        public void GetAllProcesses()
        {
            Console.WriteLine("ID   NAME    MEMORY");
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    Console.WriteLine($"{process.Id} {process.ProcessName} {process.PagedMemorySize64}");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetProcess(string file, string argument)
        {
            try
            {
                process.StartInfo.FileName = file;
                process.StartInfo.Arguments = argument;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetDir(string[] files)
        {
            foreach (string file in files)
            {
                Console.Write(Path.GetFileName(file) + "  ");
                Console.Write(Path.GetExtension(file) + "  ");
                Console.Write(File.GetCreationTime(file) + "  ");
                Console.Write(new FileInfo(file).Length + "  ");
            }
        }
    }
}
