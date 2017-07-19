using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ParserTorgiGov
{
    public class Log
    {
        public static void Logger(params object[] parametrs)
        {
            string s = "";
            s += DateTime.Now.ToString(CultureInfo.InvariantCulture);
            s = parametrs.Aggregate(s, (current, t) => $"{current} {t}");

            using (StreamWriter sw = new StreamWriter(Program.FileLog, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(s);
            }
        }
    }
}