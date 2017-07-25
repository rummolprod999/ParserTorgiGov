using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace ParserTorgiGov
{
    internal class Program
    {
        public static string PathProgram;
        private static string _database;
        private static string _logPath;
        private static string _prefix;
        private static string _user;
        private static string _pass;
        private static string _server;
        private static int _port;
        public static string StrArg;
        public static string Database => _database;
        public static string Prefix => _prefix;
        public static string User => _user;
        public static string Pass => _pass;
        public static string Server => _server;
        public static int Port => _port;
        public static string FileLog;
        public static TypeArguments Periodparsing;
        public static string LogPath => _logPath;
        public static readonly DateTime LocalDate = DateTime.Now;
        public static string UrlPub =
                "http://torgi.gov.ru/opendata/7710349494-torgi/data-{bidKind}-{publishDateFrom}-{publishDateTo}-structure-20130401T0000.xml"
            ;

        public static string UrlChange =
                "http://torgi.gov.ru/opendata/7710349494-torgi/data.xml?bidKind={bidKind}&publishDateFrom={publishDateFrom}&publishDateTo={publishDateTo}&lastChangeFrom={lastChangeFrom}&lastChangeTo={lastChangeTo}"
            ;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "Недостаточно аргументов для запуска, используйте last или curr");
                return;
            }
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName()
                .CodeBase);
            if (path != null) PathProgram = path.Substring(5);
            StrArg = args[0];
            switch (args[0])
            {
                case "last":
                    Periodparsing = TypeArguments.Last;
                    Init(Periodparsing);
                    ParserTorg(Periodparsing);
                    break;
                case "curr":
                    Periodparsing = TypeArguments.Curr;
                    Init(Periodparsing);
                    ParserTorg(Periodparsing);
                    break;
            }
        }

        private static void Init(TypeArguments arg)
        {
            GetSettings set = new GetSettings();
            _database = set.Database;
            _logPath = set.LogPath;
            _prefix = set.Prefix;
            _user = set.UserDb;
            _pass = set.PassDb;
            _server = set.Server;
            _port = set.Port;
            _logPath = set.LogPath;
            if (String.IsNullOrEmpty(LogPath))
            {
                Console.WriteLine("Не получится создать папки для парсинга");
                Environment.Exit(0);
            }
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (arg == TypeArguments.Curr || arg == TypeArguments.Last)
                FileLog = $"{LogPath}{Path.DirectorySeparatorChar}Torgi_{LocalDate:dd_MM_yyyy}.log";
        }

        private static void ParserTorg(TypeArguments arg)
        {
            Log.Logger("Время начала парсинга Torgi");
            /*ParserTorg p = new ParserTorg(Periodparsing);
            p.Parsing();*/
            DateTime date_now = DateTime.Parse("2017-07-17T11:50:52");
            Console.WriteLine($"{date_now:yyyy-MM-dd HH:mm:ss}");
            Log.Logger("Время окончания парсинга Torgi");

        }
    }
}