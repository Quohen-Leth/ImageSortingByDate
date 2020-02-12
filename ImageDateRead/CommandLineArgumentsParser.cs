using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ImageDateRead
{
    public struct LogFlags
    {
        public static bool FileFlag;
        public static bool ConsoleFlag;
        public LogFlags(bool fileFlag, bool consoleFlag)
        {
            FileFlag = fileFlag;
            ConsoleFlag = consoleFlag;
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
        // TODO Our programm will support logging to Console or saving file information to file
        // there will be two possible usages from commandline
        // your-program.exe --folder="c:\test\images"  --log=console
        // will scan files and output file info to the console
        // 
        // your-program.exe --folder="c:\test\images" --log=file --report-file="report.txt"
        // will scan files and output file info to the text file defined in --report-file

        // 
        // TODO support parsing few arguments  for example
        // your-program.exe --folder="c:\test\images" --log=console --report-file="report.txt"
        public static string Parse (string[] commandLineArgument)
        {
            // Verifying that launch argument was set.
            if (commandLineArgument.Length == 0)
            {
                throw new Exception("Run program with parameters: --folder=<path> [--log=console] [--report-file='report.txt']");
            }
            // Verifying that quotations was set properly.
            if (!commandLineArgument[0].Contains("'")){
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
            if (!Regex.IsMatch(commandLineArgument[0], pattern1))
            {
                throw new Exception("Run program with parameters: --folder=<path> [--log=console] [--report-file='report.txt']");
            }
            var matchFolderPath = Regex.Match(commandLineArgument[0], pattern1);
            string FolderPath = Regex.Replace(matchFolderPath.Value, pattern2, String.Empty);
            // Verifying that folder exists.
            if (!Directory.Exists(FolderPath))
            {
                throw new DirectoryNotFoundException($"'{FolderPath}' is not a valid directory.");
            }
            LogFlags.ConsoleFlag = false;
            LogFlags.FileFlag = false;
            foreach (string cLA in commandLineArgument)
            {
                Console.WriteLine(cLA);
                if (Regex.IsMatch(cLA, pattern3))
                {
                    LogFlags.ConsoleFlag = true;
                }
                if (Regex.IsMatch(cLA, pattern4))
                {
                    LogFlags.FileFlag = true;
                }
            }
            Console.WriteLine(LogFlags.ConsoleFlag);
            Console.WriteLine(LogFlags.FileFlag);
            return FolderPath;

            /*
            // Parsing folder path from launch argument
            int first = commandLineArgument[0].IndexOf("'");
            int last = commandLineArgument[0].LastIndexOf("'");
            // Verifying that both quotations was set properly.
            if (first == last){
                throw new quotationException();
            }
            //string FolderPath = commandLineArgument[0].Substring(first + 1, last - first - 1);
            */

            /*
            string pattern1 = @"[+-]?[+-]?folder=['][^']*[']";
            string pattern2 = @"([+-]?[+-]?folder=|['])";
            string name = @"New --folder='c:/test folder/gfdfg/435432/x≥‚‡‚Ù≥‡Ô' is hfdghsfhgf";
            var name2 = Regex.Match(name, pattern1);
            var name3 = Regex.Replace(name2.Value, pattern2,String.Empty);
            Console.WriteLine(name2.Value);
            Console.WriteLine(name3);
            */
        }
    }
}
 