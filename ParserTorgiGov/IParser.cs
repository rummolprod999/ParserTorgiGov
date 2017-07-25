using System.Data;

namespace ParserTorgiGov
{
    public interface IParser<out T> where T : Torg
    {
        DataTable GetBidKind();
        void Parsing();
    }
}