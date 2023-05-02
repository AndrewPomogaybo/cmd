using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
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
            TailArgs tail = new TailArgs();

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
                    string path = query.Replace("mkdir -v ", "");
                    string[] folders = path.Split(' ');

                    try
                    {
                        foreach (string fNAme in folders)
                        {
                            Directory.CreateDirectory(fNAme);
                            Console.WriteLine("mkdir: created directory: " + "'" + fNAme + "'");
                        }
                        
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
                else if (query.StartsWith("kill") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\kill.txt"));
                }
                else if (query.StartsWith("tail") && query.EndsWith("--help"))
                {
                    Console.WriteLine(rl.ReadLinesAll(@"C:\Users\Student\Desktop\pomogaybo\args\tail.txt"));
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
                else if (query.StartsWith("tail -n"))
                {
                    try
                    {
                        string path = query.Replace($"tail -n ", "");

                        string pattern = @"\d+";

                        Match match = Regex.Match(query, pattern);

                        if (match.Success)
                        {
                            string number = match.Value;
                            string file = path.Replace($"{number} ", "");
                            int num = Convert.ToInt32(number);
                            tail.Tail(file, num);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (query.StartsWith("tail"))
                {
                    tail.Tail(query.Replace("tail ", ""), 10);
                }
                else if (query.StartsWith("cat -n"))
                {
                    string file = query.Replace("cat -n ", "");
                    int lineNumber = 1;
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine("{0}\t{1}", lineNumber++, line);
                        }
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
                    try
                    {
                        string file = query.Replace("wc ", "");
                        string text = File.ReadAllText(file);

                        int lines = text.Split('\n').Length;
                        int words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        int chars = text.Length;
                        Console.WriteLine($"{lines}, {words}, {chars}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }
                else if (query.StartsWith("cp"))
                {
                    string path = query.Replace("cp ", "");
                    int indexForFile = 0;
                    int length = path.IndexOf(' ', indexForFile) - indexForFile;
                    if(length <= 0)
                    {
                        length = path.Length - indexForFile;

                    }
                    string file = path.Substring(indexForFile, length);
                    Console.WriteLine(file);
                    //-------------------------------------------------------------------------------
                    /*int indexForFile2 = 8;
                    int lengthForFile2 = path.IndexOf(' ', indexForFile2) - indexForFile2;
                    if (lengthForFile2 <= 0)
                    {
                        lengthForFile2 = path.Length - indexForFile2;

                    }
                    string f = path.Substring(indexForFile2, lengthForFile2);
                    string FileName = f.Replace(" ", "");*/

                    //-------------------------------------------------------------------------------

                    int indexForDir = 9;
                    int lengthForDir = path.IndexOf(' ', indexForDir) - indexForDir;
                    if (lengthForDir <= 0)
                    {
                        lengthForDir = path.Length - indexForDir;

                    }
                    string dir = path.Substring(indexForDir, lengthForDir);
                    string directory = dir.Replace(" ", "");

                    string files = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(directory, files));
                    //File.Copy(file, directory, true);


                }
                else if (query.StartsWith("kill"))
                {
                    try
                    {
                        string path = query.Replace("kill ", "");
                        ProcessStartInfo psi = new ProcessStartInfo();
                        p.DoProcess("cmd.exe", "/C taskkill /F /IM " + path);
                        psi.Verb = "runas";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

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
