using System;
using System.Collections.Generic;

namespace ImageDateRead
{
    public class Logger
    {
        public Logger(string path = null, bool consoleOut = false, bool fileOut = false)
        {
            //TODO save these vars in class,  do not pass everytime in log methods
            // Yurko' remark (for himself): how to use non static objects during program run. 
        }

        // TODO support third option to save log in memory  (just save in string variable)
        // Yurko's remark: haven't understand clearly, log is already in memory, it saved in List<CurrFileInfo> imgFiles2
        //Comleted TODO avoid using full namespace like System.Collections.Generic.List  use List instead and import namespace in using
        public bool Output(List<CurrFileInfo> fileInfoList, RunParameters rParams)
        {
            bool res = true;
            var list2 = new List<string>();
            foreach (var fl in fileInfoList)
            {
                list2.Add($"{fl.Path} - {fl.DateCreated} - {fl.DateModified} - {fl.DateEXIF}");
                if (rParams.ConsoleFlag)
                {
                    Console.WriteLine($"{fl.Path} - {fl.DateCreated} - {fl.DateModified} - {fl.DateEXIF}");
                }
            }
            if (rParams.FileFlag)
            {
                try
                {
                    // Yurko's remark: this time I used static system.io.file.WriteAllLines() as recommended here: https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
                    // WriteAllLines creates a file, writes a collection of strings to the file,
                    // and then closes the file.  You do NOT need to call Flush() or Close().
                    System.IO.File.WriteAllLines(rParams.FilePath, list2);
                }
                catch
                {
                    res = false;
                }
            }
            return res;
        }

        public void Log(string message)
        {
            //TODO implement message logging to the Console, File, Memory like in Output method
            // but with this interface,  in ideal case this class should not know anything what he is logging
            // it should NOT know about CurrFileInfo, or any other entity. also flags like (string path, bool consoleOut, bool fileOut)
            // must be set one time in class constructor
        }
    }
}
