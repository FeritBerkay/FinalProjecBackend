using System;

namespace Business.CCS
{
    public class DatabesLogger: ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veri Tabanına Loglandı loglandı");
        }
    }
}
