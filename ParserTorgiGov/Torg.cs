using System;

namespace ParserTorgiGov
{
    public class Torg
    {
        protected readonly string bidNumber;
        protected readonly string publishDate;
        protected readonly string lastChanged;
        protected readonly int isArchived;
        public Torg(string bidNumber, string publishDate, string lastChanged, int isArchived)
        {
            this.bidNumber = bidNumber;
            this.publishDate = publishDate;
            this.lastChanged = lastChanged;
            this.isArchived = isArchived;
        }

        public virtual void Parse()
        {
            
        }
    }
}