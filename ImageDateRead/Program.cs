using System;
using System.Collections.Generic;

namespace ImageDateRead
{
    class Program
    {
        static void Main(string[] args)
        {
            string imgDirPath;
            try
            {
                imgDirPath = CommandLineArgumentsParser.Parse(args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"In Directory {imgDirPath}:");
            
            var curFolderScanner = new FolderScanner();
            // TODO avoid using full namespace import namespace in using. It is hard to read code with full namespaces
            //System.Collections.Generic.List<CurrFileInfo> imgFiles = curFolderScanner.GetFiles(imgDirPath, new System.Collections.Generic.List<CurrFileInfo>(),"*.jp*");
            List<CurrFileInfo> imgFiles = curFolderScanner.GetFiles(imgDirPath, new System.Collections.Generic.List<CurrFileInfo>(),"*.jp*");
            Console.WriteLine($"{imgFiles.Count} JPEG files total.");
            List<CurrFileInfo> imgFiles2 = new System.Collections.Generic.List<CurrFileInfo>();
            foreach (var fl in imgFiles)
            {
                CurrFileInfo fl1 = ImageParser.GetDateFromFile(fl);
                imgFiles2.Add(fl1);
            }
            if (LogFlags.ConsoleFlag || LogFlags.FileFlag)
            {
                var results = new Logger();
                bool completed = results.Output(imgFiles2, imgDirPath, LogFlags.ConsoleFlag, LogFlags.FileFlag);
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
