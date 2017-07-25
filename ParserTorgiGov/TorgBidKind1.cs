namespace ParserTorgiGov
{
    public class TorgBidKind1:Torg
    {
        public TorgBidKind1(string bidNumber, string publishDate, string lastChanged, int isArchived, int b, string odDetailedHref):base(bidNumber, publishDate, lastChanged, isArchived, b, odDetailedHref)
        {
            
        }

        public override void Parse()
        {
            if (TestIdent())
            {
                Log.Logger("Такой торг уже есть в базе", bidNumber);
                return;
            }
            int Cancel = GetCancel();
            
            
        }
    }
}