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
        public List<CurrFileInfo> GetFiles(string searchDir, string searchPattern)
        {
            var list = new List<CurrFileInfo>();

            if (!Directory.Exists(searchDir))
                throw new Exception("Folder not found");

            GetFiles(searchDir, list, searchPattern);

            return list;
        }

        // Yurko's remark: There is no need to return the List of structs, it can't be modified further by adding some other params (as I understand).
        // It's enought to return a list of strings with filenames.
        private List<CurrFileInfo> GetFiles(string searchDir, List<CurrFileInfo> jfiles, string searchPattern)
        {
            foreach (string f in Directory.GetFiles(searchDir, searchPattern))
            {
                jfiles.Add(new CurrFileInfo() {Path = f});
            }

            foreach (string d in Directory.GetDirectories(searchDir))
            {
                GetFiles(d, jfiles, searchPattern);
            }

            return jfiles;
        }
    }
}