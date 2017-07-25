using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParserTorgiGov
{
    public class ParserTorg
    {
        protected readonly TypeArguments Period;
        protected readonly string ParseUrl;
        protected DataTable DtBidKind;

        public ParserTorg(TypeArguments p)
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

        public void Parsing()
        {
            foreach (DataRow row in DtBidKind.Rows)
            {
                int b = (int) row["bid_kind_id"];
                string PathParse = ParseUrl.Replace("{bidKind}", b.ToString());
                string xml = DownloadF.DownL(PathParse);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string jsons = JsonConvert.SerializeXmlNode(doc);
                /*using (StreamWriter sw = new StreamWriter($"{b}.json", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(jsons);
                }*/
                JObject json = JObject.Parse(jsons);
                JObject root = (JObject) json.SelectToken("openData");
                /*JProperty firstOrDefault = root.Properties().FirstOrDefault(pr => pr.Name.Contains("notification"));
                if (firstOrDefault != null)
                {
                    JToken notif = firstOrDefault.Value;
                    Console.WriteLine(notif);
                }*/
                List<JToken> notifications = Tools.GetElements(root, "notification");
                foreach (var n in notifications)
                {
                    string bidNumber = ((string) n.SelectToken("bidNumber") ?? "").Trim();
                    string publishDate = (JsonConvert.SerializeObject(n.SelectToken("publishDate") ?? "") ??
                                          "").Trim('"');
                    string lastChanged = (JsonConvert.SerializeObject(n.SelectToken("lastChanged") ?? "") ??
                                          "").Trim('"');
                    int isArchived = (int?) n.SelectToken("isArchived") ?? 0;

                    switch (b)
                    {
                       case 1:
                           TorgBidKind1 t1 = new TorgBidKind1(bidNumber, publishDate, lastChanged, isArchived);
                           t1.Parse();
                           break;
                    }
                    
                }
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
    }
}