using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDateRead
{
    public struct FileInfo
    {
        string path;
    }
    
    public class FolderScanner
    {
        public List<FileInfo> GetFiles(string searchDir)
        {
            // TODO recursively scan folder for image files and return list with file paths
            return new List<FileInfo>();
        }
    }
}
