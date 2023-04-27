using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;

namespace pomogaybo
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader reader = new FileReader();

            while (true)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                string query = Console.ReadLine();

                if (query == "exit")
                {
                    break;
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

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
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
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                else if (query.StartsWith("cat"))
                {
                    try
                    {
                        string path = query.Replace("cat", "");
                        string path2 = path.Replace(" ", "");
                        Console.WriteLine(reader.ReadFile(path2));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
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
                    int countRows = 10;

                    using (StreamReader file = new StreamReader(path2))
                    {
                        for(int i = 0; i < countRows; i++)
                        {
                            string line = file.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            Console.WriteLine(line);
                        }
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

                        case "help":
                           Process process = new Process();
                           process.StartInfo.FileName = "git";
                           process.StartInfo.Arguments = "help";
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

                    }
                }
            }       
            Console.ReadKey();
        }
    }
}
