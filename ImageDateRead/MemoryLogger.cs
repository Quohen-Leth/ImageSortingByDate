using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDateRead
{
    public class MemoryLogger : ILogger
    {
        private string _messageLog = "";

        public void Log(string msg)
        {
            this._messageLog += msg;
        }
    }
}