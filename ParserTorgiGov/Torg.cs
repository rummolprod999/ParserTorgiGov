using System;
using MySql.Data.MySqlClient;

namespace ParserTorgiGov
{
    public class Torg
    {
        protected readonly string bidNumber;
        protected readonly string publishDate;
        protected readonly string lastChanged;
        protected readonly int isArchived;
        protected readonly int b;
        protected readonly string odDetailedHref;
        
        public Torg(string bidNumber, string publishDate, string lastChanged, int isArchived, int b, string odDetailedHref)
        {
            this.bidNumber = bidNumber;
            this.publishDate = publishDate;
            this.lastChanged = lastChanged;
            this.isArchived = isArchived;
            this.b = b;
            this.odDetailedHref = odDetailedHref;
        }

        public virtual void Parse()
        {
            
        }

        public bool TestIdent()
        {
            string selectT = $"SELECT id FROM {Program.Prefix}torgi_bid_kind_id_{b} WHERE bidNumber = @bidNumber AND lastChanged = @lastChanged";
            DateTime dateNow = DateTime.Parse(lastChanged);
            string d = $"{dateNow:yyyy-MM-dd HH:mm:ss}";
            using (MySqlConnection connect = ConnectToDb.GetDbConnection())
            {
                MySqlCommand cmd = new MySqlCommand(selectT, connect);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@bidNumber", bidNumber);
                cmd.Parameters.AddWithValue("@lastChanged", d);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }
    }
}