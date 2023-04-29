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
            Process process = new Process();

            while (true)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                string query = Console.ReadLine();

                if (query == "exit")
                {
                    break;
                }
                else if (query.StartsWith("mkdir -v"))
                {
                    string path = query.Replace("mkdir -v", "");
                    try
                    {
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        Console.WriteLine("mkdir: created directory " + path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
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
                else if (query.StartsWith("df") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\df.txt"));
                }
                else if (query.StartsWith("wc") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\wc.txt"));
                }
                else if(query.StartsWith("rm") && query.EndsWith("-r"))
                {
                    string directory = Directory.GetCurrentDirectory();
                    try
                    {
                        Array.ForEach(Directory.GetFiles(directory), File.Delete);
                        Array.ForEach(Directory.GetDirectories(directory), Directory.Delete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if(query.StartsWith("rm") && query.EndsWith("*"))
                {
                    DirectoryInfo info = new DirectoryInfo(".");
                    foreach (FileInfo file in info.GetFiles())
                    {
                        file.Delete();
                    }
                }
                else if (query.StartsWith("wc"))
                {
                    string path = Directory.GetCurrentDirectory();

                    int lineCount = 0;
                    int wordCount = 0;
                    int byteCount = 0;

                    try
                    {
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lineCount++;
                                byteCount += Encoding.UTF8.GetByteCount(line + Environment.NewLine);
                                wordCount += line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine(lineCount);
                    Console.WriteLine(wordCount);
                    Console.WriteLine(byteCount);
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
                    string path = query.Replace("mkdir ", "");
                    string[] folders = path.Split(' ');

                    try
                    {
                        foreach (string fNAme in folders)
                        {
                            Directory.CreateDirectory(fNAme);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("rmdir"))
                {
                    string folders = query.Replace("rmdir ", "");
                    p.DoProcess("cmd.exe", $"/c rd /s /q {folders}");
                }    
                else if (query.StartsWith("cat"))
                {
                    string path = query.Replace("cat", "");
                    Console.WriteLine(rl.ReadLinesAll(path.Replace(" ", "")));
                }
                else if (query.StartsWith("date -d") && query.EndsWith("'tomorrow'"))
                {
                    DateTime tom = DateTime.Today.AddDays(1);
                    string tomDay = tom.ToString("ddd MMMM dd HH:mm:ss yyyy");
                    Console.WriteLine(tomDay);
                }
                else if (query.StartsWith("date -d") && query.EndsWith("'yesterday'"))
                {
                    DateTime yes = DateTime.Today.AddDays(-1);
                    string yesDay = yes.ToString("ddd MMMM dd HH:mm:ss yyyy");
                    Console.WriteLine(yesDay);
                }
                else if (query.StartsWith("touch"))
                {
                    string path = query.Replace("touch ", "");
                    string[] files = path.Split(' '); 
                    try
                    {
                        foreach (string fileName in files)
                        {
                            File.CreateText(fileName);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                else if (query.StartsWith("rm"))
                {
                    string path = query.Replace("rm ", "");
                    p.DoProcess("cmd.exe", $"/c del /s /q {path}");
                }
                else if (query.StartsWith("head"))
                {
                    rl.Read(10, query.Replace("head ", ""));
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
                    try
                    {
                        Console.WriteLine(size.GetSize(Directory.GetCurrentDirectory(), 0) / 1024 + " M");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("du") && query.EndsWith("-ha"))
                {
                    string path = Directory.GetCurrentDirectory();
                    p.GetDir(Directory.GetFiles(path, "*", SearchOption.AllDirectories));
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
                            p.DoProcess("cmd.exe", "/?");
                            process.WaitForExit();
                            query = process.StandardOutput.ReadToEnd();
                            Console.WriteLine(query);
                            break;

                        case "clear":
                            Console.Clear();
                            break;

                        case "date":
                            DateTime currenDate = DateTime.Now;
                            string date = currenDate.ToString("ddd MMMM dd HH:mm:ss yyyy");
                            Console.WriteLine(date);
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
                            try
                            {
                                Console.WriteLine(size.GetSize(Directory.GetCurrentDirectory(), 0));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case "df":
                            DriveInfo[] allDrives = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in allDrives)
                            {
                                Console.WriteLine($"Name: {0}" + drive.Name);
                                Console.WriteLine($"Type: {0}" + drive.DriveType);

                                if(drive.IsReady == true)
                                {
                                    Console.WriteLine($"Volume Label: {0}" + drive.VolumeLabel);
                                    Console.WriteLine($"File System: {0}" + drive.DriveFormat);
                                    Console.WriteLine($"Total availabe space: {0}" + drive.TotalFreeSpace);
                                    Console.WriteLine($"Available space for current user: {0}" + drive.AvailableFreeSpace);
                                    Console.WriteLine($"Size: {0}" + drive.TotalSize);
                                }
                            }
                            break;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
