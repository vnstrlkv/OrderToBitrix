using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderToBitrix.Bitrix;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.IO;

namespace OrderToBitrix
{
    class Program
    {
        static DateTime lastRead = DateTime.MinValue;
        static void Main(string[] args)
        {







            // string path = @"D:\IT\БЭКАПЫ\000005220.xml";

           //     XMLtoOrder XML = new XMLtoOrder();
           //
           //    Order test = XML.GetOrder(path);
           //
            //    ToBitrix.Start(test);



            INIManager PATH = new INIManager("c:\\config.ini");
            string path = PATH.GetPrivateString("PATH", "path");
            MonitorDirectory(path);
            Console.Read();

        }


        private static void MonitorDirectory(string path)

        {

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
          //  fileSystemWatcher.Filter = "duty.FPT";
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.EnableRaisingEvents = true;
           
        }
        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)

        {

           
            
                DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
                if (lastWriteTime != lastRead)
                {
                    XMLtoOrder XML = new XMLtoOrder();
                    string path = e.FullPath;
                    System.Threading.Thread.Sleep(3000);
                    Order test = XML.GetOrder(path);

                    ToBitrix.Start(test);
                    lastRead = lastWriteTime;
                }
                // else discard the (duplicated) OnChanged event
            

        
           
        }



    }
}
