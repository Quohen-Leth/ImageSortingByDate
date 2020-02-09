using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace ImageDateRead
{
    class Program
    {
        static void GetDateFromFile(CurrFileInfo FilePath)
        {
            FileInfo fl = new FileInfo(FilePath.path);
            // Reading file creation date.
            DateTime CrDate = fl.CreationTime;
            // Reading file last modification date.
            DateTime MdDate = fl.LastWriteTime;
            // In case, when EXIF date isn't available setting its value to minimal.
            DateTime ExifDate = DateTime.MinValue;
            // Opening file to steram to avoid writing whole file to memory (seems it doesn't work, hm...).
            FileStream imgfile = File.Open(fl.FullName, FileMode.Open);
            Image photo3 = Image.FromStream(imgfile, false, false);
            try
            {
                // Reading EXIF field with Id = 306 (DateTime) (all possible Ids - https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.8).
                byte[] exif306 = photo3.GetPropertyItem(306).Value;
                // All propertyitem are arrays of byte with different length. DateTime field is ASCII string with null in the end https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.imaging.propertyitem.type?view=netframework-4.8 .
                ASCIIEncoding enc = new ASCIIEncoding();
                // Converting array of bytes to string and cutting null from end.
                string strng306 = enc.GetString(exif306, 0, exif306.Length - 1);
                ExifDate = DateTime.ParseExact(strng306, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
            }
            catch
            {
            }
            Console.WriteLine($"{fl.Name} - { CrDate } - { MdDate } - { ExifDate }");
        }
        // Method that reads all available EXIF fields in selected file and returns their Id, Type and Length.
        /*static void OneImgFileDate()
        {
            FileStream imgfile = File.Open(@"e:\VSprojects\img_date_read\ImageDateRead\ImageDateRead\bin\Debug\Images\IMG_20200125_153243931.jpg", FileMode.Open);
            Image photo3 = Image.FromStream(imgfile,false,false);
            //Image photo3 = new Bitmap(@"e:\VSprojects\img_date_read\ImageDateRead\ImageDateRead\bin\Debug\Images\IMG_20200125_153243931.jpg");
            PropertyItem[] propItems = photo3.PropertyItems;
            foreach (PropertyItem propItem in propItems)
            {
                Console.WriteLine($"{propItem.Id} {propItem.Type.ToString()} {propItem.Len.ToString()}");
            }
            // Retreiving EXIF field with Id = 306 (DateTime) from selected file.
            byte[] exif306 = photo3.GetPropertyItem(306).Value;
            ASCIIEncoding enc = new ASCIIEncoding();
            string strng306 = enc.GetString(exif306, 0, exif306.Length - 1);
            DateTime ExifDate = DateTime.ParseExact(strng306, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
            Console.WriteLine(ExifDate);
        }*/

        // (kuzya review ) TODO never commit bin, obj or other generated folders and files
        // move Images folder with some test data to the root of project, delete bin and obj folders and add these folders to the .gitignore file
        // https://help.github.com/en/github/using-git/ignoring-files

        static void Main(string[] args)
        {
            
            // pass folder path with the program argument like  your-program.exe --folder={path to the folder with images}
            // example usage in commandline: your-program.exe --folder="c:\test\images"
            // during programming and debug you can put arguments in project properties->Debug->Command Line Arguments
            // all passed arguments will be present in string[] args input array
            // parse argument and get folder path value than use it in FolderScanner
            /*
            var inpuFolder = { get from arguments}
            var files = new FolderScanner().GetFiles(inputFolder);
            foreach(var f in files)
            {
                getDateFromFile(f);
            }
            */

            // Verifying that launch argument was set.
            if (args.Length == 0)
            {
                Console.WriteLine("Run program with parameter --folder=<path>");
                Console.ReadLine();
                // If not, then close application.
                return;
            }
            var conarg = args[0];
            // Verifying that quotations was set properly.
            if (!conarg.Contains("'"))
            {
                Console.WriteLine("Directory must be selected with ' '");
                Console.ReadLine();
                // If not, then close application.
                return;
            }
            // Parsing folder path from launch argument
            int first = conarg.IndexOf("'");
            int last = conarg.LastIndexOf("'");
            string ImgDirPath = conarg.Substring(first + 1, last - first - 1);
            // Verifying that folder exists.
            if (Directory.Exists(ImgDirPath))
            {
                Console.WriteLine($"In Directory {ImgDirPath}:");
                FolderScanner CurFolderScanner = new FolderScanner();
                var ImgFiles = CurFolderScanner.GetFiles(ImgDirPath);
                Console.WriteLine($"{ImgFiles.Count} JPEG files total.");
                foreach (var fl in ImgFiles)
                {
                    GetDateFromFile(fl);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("'{0}' is not a valid directory.",ImgDirPath);
                Console.ReadLine();
            }
        }
    }
}
