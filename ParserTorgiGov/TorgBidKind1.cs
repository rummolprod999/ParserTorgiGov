using System;
using System.Xml;

namespace ParserTorgiGov
{
    public class TorgBidKind1:Torg
    {
        public TorgBidKind1(string bidNumber, string publishDate, string lastChanged, int isArchived, int b, string odDetailedHref):base(bidNumber, publishDate, lastChanged, isArchived, b, odDetailedHref)
        {
            
        }

        public override void Parse()
        {
            string xml = DownloadF.DownL(odDetailedHref);
            if (String.IsNullOrEmpty(xml))
            {
                Log.Logger("Получили пустую строку торга", odDetailedHref);
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (TestIdent())
            {
                Log.Logger("Такой торг уже есть в базе", bidNumber);
                return;
            }
            int Cancel = GetCancel();
            string bidOrgKind = "";


        }
    }
}