using System;
using System.IO;

namespace ImageDateRead
{
    public class QuotationException : Exception
    {
        const string message = "Directory must be selected with ' '";
        public QuotationException()
            :base(message)
        { }
    }

    public class CommandLineArgumentsParser
    {
        public static string Parse (string CommandLineArgument)
        {
            // Verifying that quotations was set properly.
            if (!CommandLineArgument.Contains("'")){
                throw new QuotationException();
            }
            // Parsing folder path from launch argument
            int first = CommandLineArgument.IndexOf("'");
            int last = CommandLineArgument.LastIndexOf("'");
            // Verifying that both quotations was set properly.
            if (first == last){
                throw new QuotationException();
            }
            string FolderPath = CommandLineArgument.Substring(first + 1, last - first - 1);
            // Verifying that folder exists.
            if (!Directory.Exists(FolderPath)){
                throw new DirectoryNotFoundException($"'{FolderPath}' is not a valid directory.");
            }
            return FolderPath;           
        }
    }
}