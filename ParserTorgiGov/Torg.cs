using System;

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
    }
}