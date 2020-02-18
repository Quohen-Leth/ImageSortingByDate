using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace ImageDateRead
{
    public class ImageParser
    {
        public ImageParser()
        {
        }

        /// <summary>
        /// Shold do the same as we now do in program  but with this method interface
        /// </summary>
        /// <param name="inputFolder">image input folder</param>
        /// <param name="output">Output object where to save info (console, file memory)</param>
        public void CreateImageFileStatistic(string inputFolder, ILogger output)
        {
            var curFolderScanner = new FolderScanner();

            List<CurrFileInfo> files = curFolderScanner.GetFiles(inputFolder, "*.jp*");
            Console.WriteLine($"{files.Count} JPEG files total.");
            
            foreach (var fl in files)
            {
                var f = SetDateFromFile(fl);
                output.Log($"{f.Path} - {f.DateCreated} - {f.DateModified} - {f.DateEXIF}");
            }
        }

        
        public CurrFileInfo SetDateFromFile(CurrFileInfo filePath)
        {
            var fl = new FileInfo(filePath.Path);
            // Reading file creation date.
            DateTime crDate = fl.CreationTime;
            // Reading file last modification date.
            DateTime mdDate = fl.LastWriteTime;
            // In case, when EXIF date isn't available setting its value to minimal.
            DateTime exifDate = DateTime.MinValue;
            try
            {
                // Opening file to steram to avoid writing whole file to memory (seems it doesn't work, hm...).
                FileStream imgfile = File.Open(fl.FullName, FileMode.Open);
                Image photo3 = Image.FromStream(imgfile, false, false);
                // Reading EXIF field with Id = 306 (DateTime) (all possible Ids - https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.8).
                byte[] exif306 = photo3.GetPropertyItem(306).Value;
                // All propertyitem are arrays of byte with different length. DateTime field is ASCII string with null in the end https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.imaging.propertyitem.type?view=netframework-4.8 .
                var enc = new ASCIIEncoding();
                // Converting array of bytes to string and cutting null from end.
                string strng306 = enc.GetString(exif306, 0, exif306.Length - 1);
                exifDate = DateTime.ParseExact(strng306, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture,
                    DateTimeStyles.None);
            }
            catch
            {
            }

            // Adding image Dates to struct CurrFileInfo
            filePath.DateCreated = crDate;
            filePath.DateModified = mdDate;
            filePath.DateEXIF = exifDate;
            return filePath;
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
    }
}