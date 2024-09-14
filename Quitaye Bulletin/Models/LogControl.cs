using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class LogControl
    {
        private static string _Path = string.Empty;
        private static bool DEBUG = true;

        public static void Write(string msg)
        {
            LogControl._Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(LogControl._Path, "log.txt")))
                    LogControl.Log(msg, (TextWriter)w);
                if (!LogControl.DEBUG)
                    return;
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
            }
        }

        private static void Log(string msg, TextWriter w)
        {
            try
            {
                w.Write(Environment.NewLine);
                TextWriter textWriter = w;
                DateTime now = DateTime.Now;
                string longTimeString = now.ToLongTimeString();
                now = DateTime.Now;
                string longDateString = now.ToLongDateString();
                textWriter.Write("[{0} {1}]", (object)longTimeString, (object)longDateString);
                w.Write("\t");
                w.WriteLine(" {0}", (object)msg);
                w.WriteLine("-----------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
