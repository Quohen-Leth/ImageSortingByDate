using System;
using System.Text.RegularExpressions;

namespace ImageDateRead
{
    // Completed TODO avoid using static variables
    // remove static vars and use public
    // put here parsed input folder path

    public struct RunParameters
    {
        
        public bool FileFlag;
        public bool ConsoleFlag;
        public string FolderPath;
        public string FilePath;
        public RunParameters(bool fileFlag, bool consoleFlag, string folderPath, string filePath)
        {
            FileFlag = fileFlag;
            ConsoleFlag = consoleFlag;
            FolderPath = folderPath;
            FilePath = filePath;
        }
    }

    class quotationException : Exception
    {
        const string message = "Folder path must be selected with ' '";
        public quotationException()
            :base(message)
        { }
    }

    public class CommandLineArgumentsParser
    {
        //TODO return RunConfig (LogFlags)  object instead of string and setting static variables
        // Yurko's remark: Haven't understand why to use RunConfig wrap over LogFlags.
        public RunParameters Parse (string[] commandLineArguments)
        {
            var lf = new RunParameters();
            // Verifying that launch argument was set.
            if (commandLineArguments.Length == 0)
            {
                throw new Exception("Run program with parameters: --folder=<path to folder> [--log=console] [--log=file --report-file=<file name>]");
            }
            // Verifying that quotations was set properly.
            if (!commandLineArguments[0].Contains("'")){
                throw new quotationException();
            }
            // Regular expression patterns for command line argument parsing.
            // Pattern for finding working folder path:
            string pattern1 = @"--folder='[^']*'";
            // Pattern for removing superfluous chars from folder path pattern:
            string pattern2 = @"(--folder=|')";
            // Pattern to check if console log is selected:
            string pattern3 = @"--log=console";
            // Pattern to check if file log is selected:
            string pattern4 = @"--log=file";
            // Pattern to check if log file name is set:
            string pattern5 = @"--report-file='[^']*'";
            // Pattern for removing superfluous chars from file name pattern:
            string pattern6 = @"(--report-file=|')";
            if (!Regex.IsMatch(commandLineArguments[0], pattern1))
            {
                throw new Exception("Run program with parameters: --folder=<path to path> [--log=console] [--log=file --report-file=<file name>]");
            }
            var matchFolderPath = Regex.Match(commandLineArguments[0], pattern1);
            lf.FolderPath = Regex.Replace(matchFolderPath.Value, pattern2, String.Empty);

            // Completed TODO do not check here if folder exist in this class.
            // this class only does parsing and returns config (Single responsibility)
            // cheking folders path is a deal of Program not a parser
            // Completed TODO read about SOLID (https://uk.wikipedia.org/wiki/SOLID_(%D0%BE%D0%B1%27%D1%94%D0%BA%D1%82%D0%BD%D0%BE-%D0%BE%D1%80%D1%96%D1%94%D0%BD%D1%82%D0%BE%D0%B2%D0%B0%D0%BD%D0%B5_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F))
            // You should not understand everything in SOLID at this stage but you should read

            lf.ConsoleFlag = false;
            lf.FileFlag = false;
            foreach (string cLA in commandLineArguments)
            {
                if (Regex.IsMatch(cLA, pattern3))
                {
                    lf.ConsoleFlag = true;
                }
                lf.FilePath = lf.FolderPath + @"\result.txt";
                if (Regex.IsMatch(cLA, pattern4))
                {
                    lf.FileFlag = true;
                }
                else
                {
                    lf.FilePath = null;
                }
                if (Regex.IsMatch(cLA, pattern5))
                {
                    var matchFilePath = Regex.Match(cLA, pattern5);
                    lf.FilePath = Regex.Replace(matchFilePath.Value, pattern6, String.Empty);
                }
            }
            return lf;
        }
    }
}
 