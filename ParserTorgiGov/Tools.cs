using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ParserTorgiGov
{
    public static class Tools
    {
        public static List<JToken> GetElements(JToken j, string s)
        {
            List<JToken> els = new List<JToken>();
            var els_obj = j.SelectToken(s);
            if (els_obj != null && els_obj.Type != JTokenType.Null)
            {
                switch (els_obj.Type)
                {
                    case JTokenType.Object:
                        els.Add(els_obj);
                        break;
                    case JTokenType.Array:
                        els.AddRange(els_obj);
                        break;
                }
            }

            return els;
        }
    }
}