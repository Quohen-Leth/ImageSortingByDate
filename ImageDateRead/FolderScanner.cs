using System.Collections.Generic;
using System.IO;

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
        public List<CurrFileInfo> GetFiles(string searchDir)
        {
            // TODO recursively scan folder for image files and return list with file paths
            
            // Yurko's remark: Haven't found any methods for searching files, like FindFirst/FindNext for recursive search.
            // The only way to read filenames in directory is EnumerateFiles (or GetFiles) which returns a collection of filenames.
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
