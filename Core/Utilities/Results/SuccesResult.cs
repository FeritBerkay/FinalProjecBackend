using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccesResult:Result
    {
        //Suan true yada false yazmak zorunda degiliz cunku bu zaten succes oto true donderir. İster mesaj gir ister girme dedik biz.
        public SuccesResult(string message) : base(true, message)
        {

        }
        //Base result a true gonderiyor ama mesajsız.
        public SuccesResult() : base(true)
        {

        }
    }
}
