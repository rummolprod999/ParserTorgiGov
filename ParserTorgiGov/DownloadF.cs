﻿using System;
using System.Net;
using System.Threading;

namespace ParserTorgiGov
{
    public class DownloadF
    {
        public static string DownL(string url)
        {
            string tmp = "";
            int count = 0;
            while (true)
            {
                try
                {
                    tmp = new TimedWebClientUaEis().DownloadString(url);
                    break;
                }
                catch (Exception e)
                {
                    if (count >= 20)
                    {
                        Log.Logger($"Не удалось скачать xml за {count} попыток");
                        break;
                    }
                    Log.Logger("Не удалось получить строку xml", e , url);
                    count++;
                }
            }
            return tmp;
        }
    }
}