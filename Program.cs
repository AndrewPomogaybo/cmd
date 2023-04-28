using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;

namespace pomogaybo
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadLiner rl = new ReadLiner();
            Processes p = new Processes();
            Size size = new Size();

            while (true)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                string query = Console.ReadLine();

                if (query == "exit")
                {
                    break;
                }
                else if (query.StartsWith("cd") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\cd.txt"));
                }
                else if (query.StartsWith("ls") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\ls.txt"));
                }
                else if (query.StartsWith("date") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\date.txt"));
                }
                else if (query.StartsWith("pwd") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\pwd.txt"));
                }
                else if (query.StartsWith("arch") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\arch.txt"));
                }
                else if (query.StartsWith("clear") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\clear.txt"));
                }
                else if (query.StartsWith("mkdir") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\mkdir.txt"));
                }
                else if (query == "rmdir --help")
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\rmdir.txt"));
                }
                else if (query == "touch --help")
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\touch.txt"));
                }
                else if (query == "rm --help")
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\rm.txt"));
                }
                else if (query.StartsWith("cat") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\cat.txt"));
                }
                else if (query.StartsWith("du") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\du.txt"));
                }
                else if (query.StartsWith("cd"))
                {
                    try
                    {
                        string[] path = query.Split('\\');
                        string pathNew = string.Join("\\", path);
                        string rightPath = pathNew.Replace("cd", "");
                        Console.WriteLine("");
                        Directory.SetCurrentDirectory(rightPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("mkdir"))
                {
                    string path = query.Replace("mkdir", "");
                    try
                    {
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("rmdir"))
                {
                    string path = query.Replace("rmdir", "");

                    try
                    {
                        Directory.Delete(path, true);
                        Console.WriteLine("Каталог удален");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }    
                else if (query.StartsWith("cat"))
                {
                    string path = query.Replace("cat", "");
                    string path2 = path.Replace(" ", "");
                    Console.WriteLine(rl.ReadLinesAll(path2));
                }
                else if (query.StartsWith("touch"))
                {
                    string path = query.Replace("touch", "");
                    string path2 = path.Replace(" ", "");

                    try
                    {
                        StreamWriter writer = File.CreateText(path2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                else if (query.StartsWith("rm"))
                {
                    string path = query.Replace("rm", "");
                    string path2 = path.Replace(" ", "");
                    try
                    {
                        File.Delete(path2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("head"))
                {
                    string path = query.Replace("head", "");
                    string path2 = path.Replace(" ", "");
                    rl.Read(10, path2);
                }
                else if (query.StartsWith("ls") && query.EndsWith("-l"))
                {
                    string path = Directory.GetCurrentDirectory();
                    string[] files = Directory.GetFiles(path);

                    foreach (string file in files)
                    {
                        FileInfo info = new FileInfo(file);
                        Console.WriteLine(info.Name);
                        Console.Write(info.CreationTime + " ");
                        Console.Write(info.Length + " ");
                    }
                    Console.WriteLine(Directory.GetCurrentDirectory());
                }
                else if (query.StartsWith("ls") && query.EndsWith("-a"))
                {
                    try
                    {
                        string folder = Directory.GetCurrentDirectory();
                        DirectorySecurity sec = new DirectorySecurity();
                        sec.AddAccessRule(new FileSystemAccessRule(" ", FileSystemRights.FullControl, AccessControlType.Allow));
                        Directory.SetAccessControl(folder, sec);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("du") && query.EndsWith("-h"))
                {
                    string folder = Directory.GetCurrentDirectory();
                    int catalogSize = 0;
                    try
                    {
                        Console.WriteLine(size.GetSize(folder, catalogSize) / 1024 + " M");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    switch (query)
                    {
                        case "ls":
                            string dirName = Directory.GetCurrentDirectory();
                            
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

                        case "man":
                            Process process = new Process();
                            process.StartInfo.FileName = "cmd.exe";
                            process.StartInfo.Arguments = "/?";
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.Start();
                            query = process.StandardOutput.ReadToEnd();
                            Console.WriteLine(query);
                            break;

                        case "clear":
                            Console.Clear();
                            break;

                        case "date":
                            Console.WriteLine(DateTime.Now.ToString());
                            break;

                        case "pwd":
                            Console.WriteLine(Directory.GetCurrentDirectory());
                            break;

                        case "arch":
                            Console.WriteLine(System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
                            break;

                        case "ps":
                            p.GetAllProcesses();
                            break;

                        case "du":
                            string folder = Directory.GetCurrentDirectory();
                            int catalogSize = 0;
                            try
                            {
                                Console.WriteLine(size.GetSize(folder, catalogSize));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;


                    }
                }
            }
            Console.ReadKey();
        }
    }
}
