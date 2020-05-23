using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public class LoggingToFile : ILoggingService
    {
        private const String file = "requestsLog.txt";
        private const String timestampFormat = "dd/MM/yyyy hh:mm:ss";
        public void Log(string method, string path, string query, string bodyString)
        {
            using (var writer = new StreamWriter(file, true))
            {
                String timestamp = DateTime.Now.ToString(timestampFormat) + ": ";
                writer.WriteLine("===================Received Request!===================");
                writer.WriteLine(timestamp + "Method: " + method);
                writer.WriteLine(timestamp + "Path: " + path);
                writer.WriteLine(timestamp + "Query: " + query);
                writer.WriteLine(timestamp + "BodyString: " + bodyString);
            }
        }
    }
}
