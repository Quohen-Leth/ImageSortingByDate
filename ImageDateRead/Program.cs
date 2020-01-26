using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace ImageDateRead
{
    static class ImgDir
    {
        public static string ImgDirPath;
    }


    class Program
    {
        static void DirPathFind()
        {
            ImgDir.ImgDirPath = Directory.GetCurrentDirectory() + "\\Images";
            Console.WriteLine(ImgDir.ImgDirPath);
        }


        static int FileCounter(string DirPath)
        {
            DirectoryInfo di = new DirectoryInfo(DirPath);
            int FileCount = 0;
            foreach (var fi in di.EnumerateFiles("*.jp*"))
            {
                FileCount = FileCount + 1;
            }
            return FileCount;
        }

        static void OneImgFileDate()
        {
            FileStream imgfile = File.Open(@"e:\VSprojects\img_date_read\ImageDateRead\ImageDateRead\bin\Debug\Images\IMG_20200125_153243931.jpg", FileMode.Open);
            Image photo3 = Image.FromStream(imgfile,false,false);
            //Image photo3 = new Bitmap(@"e:\VSprojects\img_date_read\ImageDateRead\ImageDateRead\bin\Debug\Images\IMG_20200125_153243931.jpg");
            /*PropertyItem[] propItems = photo3.PropertyItems;
            foreach (PropertyItem propItem in propItems)
            {
                Console.WriteLine($"{propItem.Id} {propItem.Type.ToString()} {propItem.Len.ToString()}");
            }*/
            //BitmapData mdata = 
            byte[] exif306 = photo3.GetPropertyItem(306).Value;
            ASCIIEncoding enc = new ASCIIEncoding();
            string strng306 = enc.GetString(exif306, 0, exif306.Length - 1);
            DateTime ExifDate = DateTime.ParseExact(strng306, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
            Console.WriteLine(ExifDate);
        }


        static void ImgFileDate(string DirPath)
        {
            DirectoryInfo di = new DirectoryInfo(DirPath);
            foreach (var fi in di.EnumerateFiles("*.jp*"))
            {
                DateTime CrDate = fi.CreationTime;                                      //Читаєм дату створення
                DateTime MdDate = fi.LastWriteTime;                                     //читаєм дату модифікації
                DateTime ExifDate = MdDate;                                             //на випадок, якщо відсутій exif, прирівнюєм дату exif до дати модифікації
                FileStream imgfile = File.Open(fi.FullName, FileMode.Open);             //відкриваєм файл в потік, щоб не лити весь файл в пам'ять
                Image photo3 = Image.FromStream(imgfile, false, false);
                try
                {
                    byte[] exif306 = photo3.GetPropertyItem(306).Value;                 // exif містить propertyitem з id (всі id даної камери є в текстовому файлі, всі взагалі - https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.8)
                    ASCIIEncoding enc = new ASCIIEncoding();                            // всі propertyitem є масивами байтів різної довжини, propertyitem дати створення з десятковою id 306 є строкою ASCII, з нулем в кінці https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.imaging.propertyitem.type?view=netframework-4.8
                    string strng306 = enc.GetString(exif306, 0, exif306.Length - 1);    // перетворення масиву байтів в стрічку і обрізання null в кінці
                    ExifDate = DateTime.ParseExact(strng306, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
                }
                catch
                { 
                }
                
                Console.WriteLine($"{fi.Name} - { CrDate } - { MdDate } - { ExifDate }");
            }
        }

        // (kuzya review ) TODO never commit bin, obj or other generated folders and files
        // move Images folder with some test data to the root of project, delete bin and obj folders and add these folders to the .gitignore file
        // https://help.github.com/en/github/using-git/ignoring-files

        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to continue:");
            Console.ReadLine();
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
            }*/

            DirPathFind();
            int Countt = FileCounter(ImgDir.ImgDirPath);
            Console.WriteLine($"Total: {Countt} files");
            ImgFileDate(ImgDir.ImgDirPath);
            //OneImgFileDate();
            Console.ReadLine();
        }
        
    }
}
