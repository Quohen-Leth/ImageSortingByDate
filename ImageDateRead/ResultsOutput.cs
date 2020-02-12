using System;
using System.Collections.Generic;

namespace ImageDateRead
{
    class ResultsOutput
    {
        public bool Output(System.Collections.Generic.List<CurrFileInfo> fileInfoList, string path, bool consoleOut, bool fileOut)
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
    }
}
