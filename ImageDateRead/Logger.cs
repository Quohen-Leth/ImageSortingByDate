using System;
using System.Collections.Generic;

namespace ImageDateRead
{
    public class Logger
    {
        public Logger(string path = null, bool consoleOut = false, bool fileOut = false)
        {
            //TODO save these vars in class,  do not pass everytime in log methods
        }

        // TODO support third option to save log in memory  (just save in string variable)
        //Comleted TODO avoid using full namespace like System.Collections.Generic.List  use List instead and import namespace in using
        public bool Output(List<CurrFileInfo> fileInfoList, string path, bool consoleOut, bool fileOut)
        {
            bool res = true;
            var list2 = new List<string>();
            foreach (var fl in fileInfoList)
            {
                list2.Add($"{fl.Path} - {fl.DateCreated} - {fl.DateModified} - {fl.DateEXIF}");
                if (consoleOut)
                {
                    Console.WriteLine($"{fl.Path} - {fl.DateCreated} - {fl.DateModified} - {fl.DateEXIF}");
                }
            }
            if (fileOut)
            {
                try
                {
                    System.IO.File.WriteAllLines($"{path}\\Result.txt", list2);
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
