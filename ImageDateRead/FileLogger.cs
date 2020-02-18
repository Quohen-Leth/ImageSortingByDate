using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDateRead
{
    public class FileLogger : ILogger
    {
        private readonly string _file;

        public FileLogger(string file)
        {
            _file = file;
        }

        public void Log(string msg)
        {
            System.IO.File.WriteAllText(_file, msg);
        }
    }
}