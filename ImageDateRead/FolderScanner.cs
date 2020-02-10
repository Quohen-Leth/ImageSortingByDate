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
        /*static List<string> DirSearch2(string sDir, List<string> files)
        {
            foreach(string f in Directory.GetFiles(sDir, "*.jp*"))
            {
                files.Add(f);
            }
            foreach(string d in Directory.GetDirectories(sDir))
            {
                DirSearch2(d, files);
            }
            return files;
        }*/

        public List<CurrFileInfo> GetFiles(string SearchDir, List<CurrFileInfo> jfiles)
        {
            // TODO recursively scan folder for image files and return list with file paths

            // Yurko's remark: Haven't found any methods for searching files, like FindFirst/FindNext for recursive search.
            // The only way to read filenames in directory is EnumerateFiles (or GetFiles) which returns a collection of filenames.
            // Also Directory.GetFiles(SearchDir, "*.jp*", SearchOption.AllDirectories) works.
            /*List<CurrFileInfo> CurList = new List<CurrFileInfo>();*/
            foreach (string f in Directory.GetFiles(SearchDir, "*.jp*"))
            {
                jfiles.Add(new CurrFileInfo(f));
            }
            foreach(string d in Directory.GetDirectories(SearchDir))
            {
                GetFiles(d, jfiles);
            }


            /*List<string> jpf = DirSearch2(SearchDir, new List<string>());
            foreach (var f in jpf)
            {
                CurList.Add(new CurrFileInfo(f));
            }*/
            return /*CurList*/ jfiles;
        }
    }
}
