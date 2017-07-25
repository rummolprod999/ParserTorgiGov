using System;
using System.Data;
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

        public Torg(string bidNumber, string publishDate, string lastChanged, int isArchived, int b,
            string odDetailedHref)
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
            string selectT =
                $"SELECT id FROM {Program.Prefix}torgi_bid_kind_id_{b} WHERE bidNumber = @bidNumber AND lastChanged = @lastChanged";
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

        public int GetCancel()
        {
            int c = 0;
            if (!String.IsNullOrEmpty(lastChanged))
            {
                string selectT = $"SELECT id FROM {Program.Prefix}torgi_bid_kind_id_{b} WHERE bidNumber = @bidNumber";
                using (MySqlConnection connect = ConnectToDb.GetDbConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(selectT, connect);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@bidNumber", bidNumber);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter {SelectCommand = cmd};
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime dateNow = DateTime.Parse(lastChanged);
                            DateTime dateOld = (DateTime) row["lastChanged"];
                            if (dateNow > dateOld)
                            {
                                string updateT =
                                    $"UPDATE {Program.Prefix}torgi_bid_kind_id_{b} SET cancel = 1 WHERE id = @id";
                                MySqlCommand cmd2 = new MySqlCommand(updateT, connect);
                                cmd2.Prepare();
                                cmd2.Parameters.AddWithValue("id", (int) row["id"]);
                                cmd2.ExecuteNonQuery();
                            }
                            else
                            {
                                c = 1;
                            }
                        }
                    }
                }
            }
            return c;
        }
    }
}