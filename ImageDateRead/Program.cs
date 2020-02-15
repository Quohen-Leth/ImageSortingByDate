using System;
using System.IO;
using System.Collections.Generic;

namespace ImageDateRead
{
    class Program
    {
        static void Main(string[] args)
        {
            // Yurko's remark: Is this ok?)
            var runParams = new RunParameters();
            var cLAParser = new CommandLineArgumentsParser();
            try
            {
                runParams = cLAParser.Parse(args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }
            // Verifying that folder exists.
            if (!Directory.Exists(runParams.FolderPath))
            {
                Console.WriteLine($"'{runParams.FolderPath}' is not a valid directory.");
                return;
            }
            Console.WriteLine($"In Directory {runParams.FolderPath}:");
            var curFolderScanner = new FolderScanner();
            // Completed TODO avoid using full namespace import namespace in using. It is hard to read code with full namespaces
            List<CurrFileInfo> imgFiles1 = curFolderScanner.GetFiles(runParams.FolderPath, new List<CurrFileInfo>(),"*.jp*");
            Console.WriteLine($"{imgFiles1.Count} JPEG files total.");
            var imgFiles2 = new List<CurrFileInfo>();
            foreach (var fl1 in imgFiles1)
            {
                var imPars = new ImageParser();
                CurrFileInfo fl2 = imPars.GetDateFromFile(fl1);
                imgFiles2.Add(fl2);
            }
            if (runParams.ConsoleFlag || runParams.FileFlag)
            {
                var results = new Logger();
                bool completed = results.Output(imgFiles2, runParams);
                if (completed)
                {
                    Console.WriteLine("Completed");
                }
                else
                {

                }
            }
            Console.ReadLine();
        }
    }
}
