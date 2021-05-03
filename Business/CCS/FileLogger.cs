using System;
using System.Collections.Generic;
using System.Text;

namespace Business.CCS
{
    //Loggları bir dosyaya alıyoruz.
    public class FileLogger:ILogger
    {
        public void Log()
        {
            Console.WriteLine("Dosyaya loglandı");
        }
    }
}
