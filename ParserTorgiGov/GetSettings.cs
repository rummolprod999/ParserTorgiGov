using System;
using System.Xml;
using System.IO;

namespace ParserTorgiGov
{
    public class GetSettings
    {
        public readonly string Database;
        public readonly string LogPath;
        public readonly string Prefix;
        public readonly string UserDb;
        public readonly string PassDb;
        public readonly string Server;
        public readonly int Port;

        public GetSettings()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Program.PathProgram + Path.DirectorySeparatorChar + "setting_torgi_gov.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlNode xnode in xRoot)
                {
                    switch (xnode.Name)
                    {
                        case "database":
                            Database = xnode.InnerText;
                            break;
                        case "logdir_torgi":
                            LogPath = $"{Program.PathProgram}{Path.DirectorySeparatorChar}{xnode.InnerText}";
                            break;
                        case "prefix":
                            Prefix = xnode.InnerText;
                            break;
                        case "userdb":
                            UserDb = xnode.InnerText;
                            break;
                        case "passdb":
                            PassDb = xnode.InnerText;
                            break;
                        case "server":
                            Server = xnode.InnerText;
                            break;
                        case "port":
                            Port = Int32.TryParse(xnode.InnerText, out Port) ? Int32.Parse(xnode.InnerText) : 3306;
                            break;
                    }
                }
            }
            if (String.IsNullOrEmpty(LogPath) || String.IsNullOrEmpty(Database) ||
                String.IsNullOrEmpty(UserDb) || String.IsNullOrEmpty(Server))
            {
                Console.WriteLine("Некоторые поля в файле настроек пустые");
                Environment.Exit(0);
            }
        }
    }
}