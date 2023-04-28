using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogaybo
{
    public class Processes
    {
        public void GetAllProcesses()
        {
            Console.WriteLine("ID NAME MEMORY");
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
    }
}
