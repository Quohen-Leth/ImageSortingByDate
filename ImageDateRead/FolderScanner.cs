using System.Collections.Generic;
using System.IO;
using System;

namespace ImageDateRead
{
    // Yurko's remark: FileInfo is a standard class for file hadling in C#, so I changed the name FileInfo into CurrFileInfo
    // Also GetFiles is the method of Directory class, so I renamed it into CurrGetFiles
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
        public List<CurrFileInfo> CurrGetFiles(string searchDir)
        {
            // TODO recursively scan folder for image files and return list with file paths

            //
            List<CurrFileInfo> CurList = new List<CurrFileInfo>();
            var jpgfiles = Directory.EnumerateFiles(searchDir, "*.jp*");
            foreach (var fff in jpgfiles)
            {
                CurList.Add(new CurrFileInfo(fff));
            }
            return CurList;
        }
    }
}
