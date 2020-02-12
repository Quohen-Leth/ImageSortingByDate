using System.Collections.Generic;
using System.IO;
using System;

namespace ImageDateRead
{
    public struct CurrFileInfo
    {
        public string Path;
        public DateTime DateCreated;
        public DateTime DateModified;
        public DateTime DateEXIF;
        public CurrFileInfo(string path, DateTime dateCreated, DateTime dateModified, DateTime dateEXIF)
        {
            Path = path;
            DateCreated = dateCreated;
            DateModified = dateModified;
            DateEXIF = dateEXIF;
        }
    }
    
    public class FolderScanner
    {
        // Completed TODO reminder C#  code convention for naming variables and methods
        public List<CurrFileInfo> GetFiles(string searchDir, List<CurrFileInfo> jfiles, string searchPattern)
        {
            foreach (string f in Directory.GetFiles(searchDir, searchPattern))
            {
                jfiles.Add(new CurrFileInfo() {Path = f});
            }
            foreach(string d in Directory.GetDirectories(searchDir))
            {
                GetFiles(d, jfiles, searchPattern);
            }

            return jfiles;
        }
    }
}
