using System;
using System.Net;

namespace ParserTorgiGov
{
    public class TimedWebClient: WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest wr = base.GetWebRequest(address);
            wr.Timeout = 300000;
            return wr;
        }
    }
    
    public class TimedWebClientUa: WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest wr = (HttpWebRequest)base.GetWebRequest(address);
            wr.Timeout = 60000;
            wr.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:55.0) Gecko/20100101 Firefox/55.0";
            return wr;
        }
    }
    public class TimedWebClientUaEis: WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest wr = (HttpWebRequest)base.GetWebRequest(address);
            wr.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            wr.Timeout = 90000;
            wr.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None;
            return wr;
        }
    }
    }
