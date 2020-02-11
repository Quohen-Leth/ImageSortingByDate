﻿using System;

namespace ImageDateRead
{
    class Program
    {
        // Completed TODO move all image date parsing logic to separate class for example ImageParser.cs
        static void Main(string[] args)
        {
            // Completed TODO  move all logic to CommandLineArgumentsParser
            // checking arguments length and exception is a deal of Parser not a program
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

            // Completed TODO use naming rules  all local and private variable must start from lower case - curFolderScanner  etc
            var curFolderScanner = new FolderScanner();
            System.Collections.Generic.List<CurrFileInfo> imgFiles = curFolderScanner.GetFiles(imgDirPath, new System.Collections.Generic.List<CurrFileInfo>(),"*.jp*");
            Console.WriteLine($"{imgFiles.Count} JPEG files total.");
            foreach (var fl in imgFiles)
            {
                CurrFileInfo fl1 = ImageParser.GetDateFromFile(fl);
                Console.WriteLine($"{fl1.Path} {fl1.DateCreated} {fl1.DateModified} {fl1.DateEXIF}");
            }
            Console.ReadLine();
        }
    }
}
