using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        //Suan true yada false yazmak zorunda degiliz cunku bu zaten error oto false donderir. İster mesaj gir ister girme dedik.
        public ErrorResult(string message) : base(false, message)
        {

        }
        //Base result a false gonderiyor ama mesajsız.
        public ErrorResult() : base(false)
        {

        }
    }
}
