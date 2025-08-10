using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_Hortifruti.Utils.Log
{
    public class Logger : ILogger
    {
        readonly string logPath;
        public Logger(string logPath)
        {
            this.logPath = logPath;
        }
        public async Task Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nData: ");
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("\nMensagem:");
            sb.Append(ex.Message);
            sb.Append("\nStackTrace:");
            sb.Append(ex.StackTrace);
            sb.Append("\n------------------------------------------------------------");

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                await sw.WriteAsync(sb.ToString());
            }
        }
    }
}