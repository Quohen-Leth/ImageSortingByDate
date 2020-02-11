using System.Collections.Generic;
using System.IO;
using System;

namespace ImageDateRead
{
    // Yurko's remark: FileInfo is a standard class for file hadling in C#, so I changed the name FileInfo into CurrFileInfo
    public struct CurrFileInfo
    {
        public string path;
        public CurrFileInfo(string p1)
        {
            path = p1;
        }
    }
    
    public class FolderScanner
    {
        // TODO reminder C#  code convention for naming variables and methods
        public List<CurrFileInfo> GetFiles(string SearchDir, List<CurrFileInfo> jfiles, string searchPattern = "*.jp*")
        {
            foreach (string f in Directory.GetFiles(SearchDir, searchPattern))
            {
                jfiles.Add(new CurrFileInfo(f));
            }
            foreach(string d in Directory.GetDirectories(SearchDir))
            {
                GetFiles(d, jfiles);
            }

            return jfiles;
        }
    }
}
