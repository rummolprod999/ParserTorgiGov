using System.Data;
using MySql.Data.MySqlClient;

namespace ParserTorgiGov
{
    public class Parser : IParser<Torg>
    {
        protected readonly TypeArguments Period;
        protected readonly string ParseUrl;
        protected DataTable DtBidKind;

        public Parser(TypeArguments p)
        {
            Period = p;
            DtBidKind = GetBidKind();
            string currDate = $"{Program.LocalDate:yyyyMMdd}T0000";
            switch (Period)
            {
                case TypeArguments.Last:
                    string startDate = "20170101T0000";
                    ParseUrl = Program.UrlPub.Replace("{publishDateFrom}", startDate);
                    ParseUrl = ParseUrl.Replace("{publishDateTo}", currDate);
                    break;
                case TypeArguments.Curr:
                    string Date3Days = $"{Program.LocalDate.AddDays(-3):yyyyMMdd}T0000";
                    ParseUrl = Program.UrlPub.Replace("{publishDateFrom}", Date3Days);
                    ParseUrl = ParseUrl.Replace("{lastChangeFrom}", Date3Days);
                    ParseUrl = ParseUrl.Replace("{publishDateTo}", currDate);
                    ParseUrl = ParseUrl.Replace("{lastChangeTo}", currDate);
                    break;
            }
        }

        public DataTable GetBidKind()
        {
            string sel = "SELECT * FROM torgi_bid_kind";
            DataTable dt;
            using (MySqlConnection connect = ConnectToDb.GetDbConnection())
            {
                connect.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sel, connect);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            return dt;
        }

        public virtual void Parsing()
        {
        }
    }
}