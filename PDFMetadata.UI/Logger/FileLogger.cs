using System;
using System.IO;
using System.Text;

namespace PDFMetadata.UI.Logger
{
    /// <summary>
    /// Logging to root file 
    /// </summary>
    public class FileLogger
    {
        public static void Log(Exception exception, string path)
        {
            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Append("Exception Type" + Environment.NewLine);
                sb.Append(exception.GetType().Name);
                sb.Append(Environment.NewLine + Environment.NewLine);
                sb.Append("Message" + Environment.NewLine);
                sb.Append(exception.Message + Environment.NewLine + Environment.NewLine);
                sb.Append("Stack Trace" + Environment.NewLine);
                sb.Append(exception.StackTrace + Environment.NewLine + Environment.NewLine);
                exception = exception.InnerException;
            } while (exception != null);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(path + $"{DateTime.Now.ToString("yyyyMMddTHHmmss")}.txt", sb.ToString());
        }
    }
}