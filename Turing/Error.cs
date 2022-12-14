using System;
using System.IO;

namespace TURING
{
    public class Error : Exception
    {
        public Error (string message, StreamWriter log) : base(message)
        {
            log.WriteLine(message);
        }
    }
}