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
            catch (Exception ex)
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

            var imPars = new ImageParser();
            ILogger logger;

            switch (runParams.LoggerType)
            {
                case LoggerType.Console:
                    logger = new ConsoleLogger();
                    break;
                case LoggerType.File:
                    logger = new FileLogger(runParams.FilePath);
                    break;
                case LoggerType.Memory:
                    logger = new MemoryLogger();
                    break;
                default:
                    logger = new ConsoleLogger();
                    break;
            }


            imPars.CreateImageFileStatistic(runParams.FolderPath, logger);

            

            Console.ReadLine();
        }
    }
}