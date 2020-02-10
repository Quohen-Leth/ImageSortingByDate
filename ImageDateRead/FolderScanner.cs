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
        string[] jf1;

        string[] DirSearch(string SearchPath)
        {
            foreach (string d in Directory.GetDirectories(SearchPath))
            {
                //Console.WriteLine(d);
                foreach (string f in Directory.GetFiles(d))
                {
                    jf1.
                    Console.WriteLine(f);
                }
                DirSearch(d);
            }
        }

        public List<CurrFileInfo> GetFiles(string SearchDir)
        {
            // TODO recursively scan folder for image files and return list with file paths
            
            // Yurko's remark: Haven't found any methods for searching files, like FindFirst/FindNext for recursive search.
            // The only way to read filenames in directory is EnumerateFiles (or GetFiles) which returns a collection of filenames.
            List<CurrFileInfo> CurList = new List<CurrFileInfo>();

            string[] jf = DirSearch(SearchDir);
            var jpgfiles = Directory.EnumerateFiles(SearchDir, "*.jp*");
            foreach (var fff in jpgfiles)
            {
                CurList.Add(new CurrFileInfo(fff));
            }
            return CurList;
        }
    }
}
